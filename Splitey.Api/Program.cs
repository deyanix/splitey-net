using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using Splitey.Authorization;
using Splitey.Authorization.DependencyInjection;
using Splitey.Core.DependencyInjection;
using Splitey.Data;
using Splitey.Data.DependencyInjection;
using Splitey.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services
    .AddSpliteyData()
    .AddSpliteyAuthorization()
    .AddSpliteyApiCore();

builder.Services.AddTransient(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("Default")));
builder.Services
    .AddAuthentication("JWT")
    .AddJwt();
builder.Services
    .AddAuthorization(options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes("JWT")
            .RequireAuthenticatedUser()
            .Build();
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.DefaultModelsExpandDepth(-1); 
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();