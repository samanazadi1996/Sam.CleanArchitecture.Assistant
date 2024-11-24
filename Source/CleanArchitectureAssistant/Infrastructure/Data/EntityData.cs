using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Infrastructure.Data;

public class EntityData
{
    public static FileDto GetEntity(string solutionName, string domainname, string entityName, string baseType,string keyProperyType)
    {
        var result = new FileDto($"{entityName}.cs")
        {
            Path = "ca-entity\\DomainName\\Entities\\EntityName.ca"
        };

        string fileContent = EmbeddedResourceDataReader.ReadEmbeddedTextFile(result.Path);

        if (!string.IsNullOrWhiteSpace(keyProperyType) && keyProperyType!= "default")
            baseType = $"{baseType}<{keyProperyType}>";


        result.Content = fileContent
            .Replace("DomainName", domainname)
            .Replace("EntityName", entityName)
            .Replace("AuditableBaseEntity", baseType)
            .Replace("CleanArchitecture.", solutionName + ".");

        return result;

    }

}
