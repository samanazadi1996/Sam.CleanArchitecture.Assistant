using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Infrastructure.Data;

public class ControllerData
{
    public static FileDto GetController(string solutionName, string controllerName, int version)
    {
        var result = new FileDto($"{controllerName}.cs")
        {
            Path = "ca-controller\\ProductController.ca"
        };

        string fileContent = EmbeddedResourceDataReader.ReadEmbeddedTextFile(result.Path);


        result.Content = fileContent
            .Replace("Product", controllerName.Replace("Controller", ""))
            .Replace("CleanArchitecture.", $"{solutionName}.")
            .Replace("Controllers.v1", $"Controllers.v{version}")
            .Replace("ApiVersion(\"1\")", $"ApiVersion(\"{version}\")");

        return result;

    }

}