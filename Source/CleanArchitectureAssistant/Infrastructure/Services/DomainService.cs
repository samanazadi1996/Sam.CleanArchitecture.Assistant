using CleanArchitectureAssistant.Infrastructure.Data;
using CleanArchitectureAssistant.Infrastructure.DTOs;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class DomainService
{
    public static async Task<bool> CreateEntity(string domainName, string entityName, string baseType, string keyPropertyTypeComboBox)
    {
        try
        {
            var domainPath = await CommonService.GetDomainPath();
            if (string.IsNullOrEmpty(domainPath))
                return false;


            var dir = Path.Combine(domainPath, domainName, "Entities");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);


            var file = EntityData.GetEntity(await CommonService.GetSolutionName(), domainName, entityName, baseType, keyPropertyTypeComboBox);

            File.WriteAllText(Path.Combine(dir, file.Name), file.Content);

        }
        catch
        {
            return false;
        }

        return true;
    }
    public static async Task<List<string>> GetDonmains()
    {
        var domainPath = await CommonService.GetDomainPath();
        if (string.IsNullOrEmpty(domainPath))
            return [];

        var dir = Path.Combine(domainPath);

        return Directory.GetDirectories(dir)
            .Select(Path.GetFileName)
            .ToList();
    }
    public static async Task<List<EntityDto>> GetEntities()
    {
        var bt = new List<string> { "BaseEntity", "AuditableBaseEntity" };
        var result = new List<EntityDto>();

        try
        {
            var path = await CommonService.GetDomainPath();

            if (path != null)
            {
                var filePaths = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

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
                        // Check if the class inherits from any of the ignored base classes
                        var baseTypes = classDeclaration.BaseList?.Types;

                        if (baseTypes != null && baseTypes.Value.All(b => bt.Contains(b.Type.ToString())))
                        {
                            // Extract the namespace using Roslyn
                            var namespaceDeclaration = classDeclaration.Ancestors()
                                .OfType<NamespaceDeclarationSyntax>()
                                .FirstOrDefault();

                            var namespaceName = namespaceDeclaration?.Name.ToString() ?? "UnknownNamespace";

                            result.Add(new EntityDto
                            {
                                ClassName = classDeclaration.Identifier.Text,
                                Namespace = namespaceName
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception appropriately
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return result;
    }

}