using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Splitey.SqlGenerator
{
    [Generator]
    public class SqlQueryGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var provider = context.AdditionalTextsProvider
                .Where(file => file.Path.EndsWith(".sql"))
                .Combine(context.AnalyzerConfigOptionsProvider)
                .Combine(context.CompilationProvider);

            context.RegisterSourceOutput(provider, (spc, source) =>
            {
                var ((file, options), compilation) = source;
                
                try 
                {
                    if (!options.GlobalOptions.TryGetValue("build_property.projectdir", out var projectDir) 
                        || string.IsNullOrWhiteSpace(projectDir))
                    {
                        return;
                    }

                    var rootNamespace = compilation.AssemblyName?.Replace(" ", "_") ?? "Project";

                    var normalizedPath = file.Path.Replace("\\", "/");
                    var normalizedProjectDir = projectDir.Replace("\\", "/");
                    if (!normalizedProjectDir.EndsWith("/")) normalizedProjectDir += "/";

                    var relativePath = normalizedPath.StartsWith(normalizedProjectDir) 
                        ? normalizedPath.Substring(normalizedProjectDir.Length) 
                        : Path.GetFileName(file.Path);

                    var directoryName = Path.GetDirectoryName(relativePath)?.Replace("\\", "/") ?? "";
                    var fileName = Path.GetFileNameWithoutExtension(file.Path);

                    var folderNamespace = directoryName
                        .Replace("/", ".")
                        .Replace(" ", "_")
                        .Replace("-", "_");

                    var fullNamespace = string.IsNullOrWhiteSpace(folderNamespace) 
                        ? rootNamespace 
                        : $"{rootNamespace}.{folderNamespace}";

                    var content = file.GetText()?.ToString() ?? string.Empty;
                    var safeContent = SanitizeSql(content).Replace("\"", "\"\"");
                    var propertyName = fileName.Replace(" ", "_").Replace("-", "_");

                    var code = $@"
namespace {fullNamespace}
{{
    public static partial class SqlQuery
    {{
        public const string {propertyName} = @""{safeContent}"";
    }}
}}";

                    var hintName = relativePath.Replace("/", "_").Replace(".sql", ".g.cs");
                    spc.AddSource(hintName, SourceText.From(code, Encoding.UTF8));}
                catch (Exception ex)
                {
                    var errorName = Path.GetFileName(file.Path) + ".Error.g.cs";
                    var errorContent = $@"/* GENERATOR ERROR:
{ex.Message}
Stack Trace:
{ex.StackTrace}
*/";
                    spc.AddSource(errorName, SourceText.From(errorContent, Encoding.UTF8));
                }
            });
        }
        
        private static string SanitizeSql(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) 
                return string.Empty;

            var noComments = Regex.Replace(sql, @"--.*$", "", RegexOptions.Multiline);

            var lines = noComments.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l));

            return string.Join("\n", lines);
        }
    }
}