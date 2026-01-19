using Microsoft.AspNetCore.Authentication.Cookies;
using Splitey.Authorization.DependencyInjection;
using Splitey.Core.DependencyInjection;
using Splitey.Data.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSpliteyData(builder.Configuration.GetConnectionString("Default"))
    .AddSpliteyAuthorization()
    .AddSpliteyApiCore();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllWithCredentials", policy =>
    {
        policy.SetIsOriginAllowed(origin => true) // Trik: akceptuje każdy origin dynamicznie
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Wymagane, aby Cookie autoryzacyjne działało
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Splitey.Session";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
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
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAllWithCredentials");
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();