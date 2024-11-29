using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Infrastructure.Data;

public class EntityData
{
    public static FileDto GetEntity(string solutionName, string domainName, string entityName, string baseType,string keyPropertyType)
    {
        var result = new FileDto($"{entityName}.cs")
        {
            Path = "ca-entity\\DomainName\\Entities\\EntityName.ca"
        };

        string fileContent = EmbeddedResourceDataReader.ReadEmbeddedTextFile(result.Path);

        if (!string.IsNullOrWhiteSpace(keyPropertyType) && keyPropertyType != "default")
            baseType = $"{baseType}<{keyPropertyType}>";


        result.Content = fileContent
            .Replace("DomainName", domainName)
            .Replace("EntityName", entityName)
            .Replace("AuditableBaseEntity", baseType)
            .Replace("CleanArchitecture.", solutionName + ".");

        return result;

    }

}
public class RepositoryData
{
    public static FileDto GetInterface(string solutionName, string entityName)
    {
        var result = new FileDto($"I{entityName}Repository.cs")
        {
            Path = "ca-repo\\IProductRepository.ca"
        };

        var fileContent = EmbeddedResourceDataReader.ReadEmbeddedTextFile(result.Path);


        result.Content = fileContent
            .Replace("Product", entityName)
            .Replace("CleanArchitecture.", solutionName + ".");

        return result;

    }
    public static FileDto Getimplimentation(string solutionName, string entityName)
    {
        var result = new FileDto($"{entityName}Repository.cs")
        {
            Path = "ca-repo\\ProductRepository.ca"
        };

        var fileContent = EmbeddedResourceDataReader.ReadEmbeddedTextFile(result.Path);


        result.Content = fileContent
            .Replace("Product", entityName)
            .Replace("CleanArchitecture.", solutionName + ".");

        return result;

    }

}
