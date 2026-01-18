using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Splitey.SqlGenerator
{
    [Generator]
    public class SqlQueryGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // Łączymy dostawcę plików, opcji (dla ProjectDir) i kompilacji (dla nazwy projektu/assembly)
            var provider = context.AdditionalTextsProvider
                .Where(file => file.Path.EndsWith(".sql"))
                .Combine(context.AnalyzerConfigOptionsProvider)
                .Combine(context.CompilationProvider);

            context.RegisterSourceOutput(provider, (spc, source) =>
            {
                var ((file, options), compilation) = source;

                // 1. Pobierz ścieżkę główną projektu
                if (!options.GlobalOptions.TryGetValue("build_property.projectdir", out var projectDir) 
                    || string.IsNullOrWhiteSpace(projectDir))
                {
                    return;
                }

                // 2. Pobierz nazwę Assembly (będzie głównym członem namespace, np. Splitey.Api)
                var rootNamespace = compilation.AssemblyName?.Replace(" ", "_") ?? "Project";

                // 3. Normalizacja ścieżek
                var normalizedPath = file.Path.Replace("\\", "/");
                var normalizedProjectDir = projectDir.Replace("\\", "/");
                if (!normalizedProjectDir.EndsWith("/")) normalizedProjectDir += "/";

                // 4. Oblicz ścieżkę relatywną (np. Resources/User/User/Get.sql)
                var relativePath = normalizedPath.StartsWith(normalizedProjectDir) 
                    ? normalizedPath.Substring(normalizedProjectDir.Length) 
                    : Path.GetFileName(file.Path);

                // 5. Wyciągnij katalog i nazwę pliku
                var directoryName = Path.GetDirectoryName(relativePath)?.Replace("\\", "/") ?? "";
                var fileName = Path.GetFileNameWithoutExtension(file.Path);

                // 6. Buduj namespace z nazwy projektu i folderów
                // Zamień slashe na kropki, spacje/kreski na podkreślniki
                var folderNamespace = directoryName
                    .Replace("/", ".")
                    .Replace(" ", "_")
                    .Replace("-", "_");

                // Jeśli plik jest w root, namespace to tylko nazwa projektu. Jeśli głębiej - doklejamy.
                var fullNamespace = string.IsNullOrWhiteSpace(folderNamespace) 
                    ? rootNamespace 
                    : $"{rootNamespace}.{folderNamespace}";

                // 7. Przygotuj zawartość SQL
                var content = file.GetText()?.ToString() ?? string.Empty;
                var safeContent = content.Replace("\"", "\"\"");
                var propertyName = fileName.Replace(" ", "_").Replace("-", "_");

                // 8. Generuj kod
                // Używamy "partial class", aby wiele plików w tym samym folderze mogło rozszerzać tę samą klasę Query
                var code = $@"
namespace {fullNamespace}
{{
    public static partial class SqlQuery
    {{
        public const string {propertyName} = @""{safeContent}"";
    }}
}}";

                // Unikalna nazwa pliku dla kompilatora
                var hintName = relativePath.Replace("/", "_").Replace(".sql", ".g.cs");
                spc.AddSource(hintName, SourceText.From(code, Encoding.UTF8));
            });
        }
    }
}