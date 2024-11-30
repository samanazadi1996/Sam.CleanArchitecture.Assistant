using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Infrastructure.Data;

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
    public static FileDto GetImplementation(string solutionName, string entityName)
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