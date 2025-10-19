using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Infrastructure.Data;

public class EndpointData
{
    public static FileDto GetEndpoint(string solutionName, string EndpointName)
    {
        var result = new FileDto($"{EndpointName}.cs")
        {
            Path = "ca-endpoint\\ProductEndpoint.ca"
        };

        string fileContent = EmbeddedResourceDataReader.ReadEmbeddedTextFile(result.Path);


        result.Content = fileContent
            .Replace("Product", EndpointName.Replace("Endpoint", ""))
            .Replace("CleanArchitecture.", $"{solutionName}.");

        return result;

    }

}