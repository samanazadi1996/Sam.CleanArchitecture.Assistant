using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class EfService
{
    public static async Task<bool> AddMigration(string dataLayer, string startUpProject, string migrationName, string context)
    {
        try
        {
            var classLibs = await CommonService.GetProjectsPath();

            var startUp = classLibs.FirstOrDefault(p => p.Contains(startUpProject));
            var data = classLibs.FirstOrDefault(p => p.Contains(dataLayer));

            var cli = $" ef migrations add {migrationName} --context {context} --project \"{data}\" --startup-project \"{startUp}\"";

            var cmdResult = CmdService.Shell("dotnet", cli);


            if (cmdResult.Contains("Build failed"))
            {
                var nsg = cmdResult.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                    .FirstOrDefault(P => P.Contains("Build failed"));
                if (!string.IsNullOrEmpty(nsg))
                {
                    await VS.MessageBox.ShowAsync(nsg);
                    return false;
                }
            }
        }
        catch
        {
            return false;
        }

        return true;
    }
    public static async Task<List<string>> GetApplicationDbXontext(string dataLayerPath)
    {
        List<string> result = [];
        try
        {


            var filePaths = System.IO.Directory.GetFiles(dataLayerPath, "*.cs", SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                var fileContent = File.ReadAllText(filePath);

                // Parse the file content using Roslyn
                var syntaxTree = CSharpSyntaxTree.ParseText(fileContent);
                var root = await syntaxTree.GetRootAsync();

                // Find all class declarations in the file
                var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

                foreach (var classDeclaration in classDeclarations)
                {
                    // Check if the class inherits from DbContext
                    var baseTypes = classDeclaration.BaseList?.Types;
                    if (baseTypes != null && baseTypes.Value.Any(bt => bt.ToString().Contains("DbContext")))
                    {
                        var className = classDeclaration.Identifier.Text;
                        result.Add(className);
                    }
                }
            }
        }
        catch (Exception e)
        {
            // ignored
        }

        return result;
    }

}