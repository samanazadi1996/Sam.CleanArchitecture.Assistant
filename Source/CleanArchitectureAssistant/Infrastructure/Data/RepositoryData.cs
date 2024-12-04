using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Infrastructure.Data;

public class RepositoryData
{
    public static FileDto GetInterface(string solutionName, EntityDto entity)
    {
        var result = new FileDto($"I{entity.ClassName}Repository.cs")
        {
            Path = "ca-repo\\IProductRepository.ca"
        };

        var fileContent = EmbeddedResourceDataReader.ReadEmbeddedTextFile(result.Path);


        result.Content = fileContent
            .Replace("CleanArchitecture.Domain.Products.Entities", entity.Namespace)
            .Replace("Product", entity.ClassName)
            .Replace("CleanArchitecture.", solutionName + ".");

        return result;

    }
    public static FileDto GetImplementation(string solutionName, EntityDto entity)
    {
        var result = new FileDto($"{entity.ClassName}Repository.cs")
        {
            Path = "ca-repo\\ProductRepository.ca"
        };

        var fileContent = EmbeddedResourceDataReader.ReadEmbeddedTextFile(result.Path);


        result.Content = fileContent
            .Replace("Product", entity.ClassName)
            .Replace("CleanArchitecture.Domain.Products.Entities", entity.Namespace)
            .Replace("CleanArchitecture.", solutionName + ".");

        return result;

    }

}