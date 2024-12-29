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
            .Replace("CleanArchitecture.", $"{solutionName}.");

        return result;

    }

}
