using CleanArchitectureAssistant.Infrastructure.DTOs;
using CleanArchitectureAssistant.Infrastructure.Enums;
using CleanArchitectureAssistant.Infrastructure.Services;
using System.Collections.Generic;

namespace CleanArchitectureAssistant.Infrastructure.Data;

public class UseCaseData
{
    public static List<FileDto> GetData(string solutionName,
        string featureName, string useCaseName, UseCaseType type, string returnType)
    {
        List<FileDto> result = [];

        if (type == UseCaseType.Query)
        {
            result.Add(new FileDto($"{useCaseName}Query.cs")
            {
                Path = "ca-use-case\\Features\\FeatureName\\Queries\\UseCaseName\\UseCaseNameQuery.ca"
            });

            result.Add(new FileDto($"{useCaseName}QueryHandler.cs")
            {
                Path = "ca-use-case\\Features\\FeatureName\\Queries\\UseCaseName\\UseCaseNameQueryHandler.ca"
            });
        }
        else if (type == UseCaseType.QueryPagedList)
        {
            result.Add(new FileDto($"{useCaseName}Query.cs")
            {
                Path = "ca-use-case\\Features\\FeatureName\\Queries\\UseCaseNamePagedList\\UseCaseNamePagedListQuery.ca"
            });

            result.Add(new FileDto($"{useCaseName}QueryHandler.cs")
            {
                Path = "ca-use-case\\Features\\FeatureName\\Queries\\UseCaseNamePagedList\\UseCaseNamePagedListQueryHandler.ca"
            });
        }
        else
        {
            result.Add(new FileDto($"{useCaseName}Command.cs")
            {
                Path = "ca-use-case\\Features\\FeatureName\\Commands\\UseCaseName\\UseCaseNameCommand.ca"
            });

            result.Add(new FileDto($"{useCaseName}CommandHandler.cs")
            {
                Path = "ca-use-case\\Features\\FeatureName\\Commands\\UseCaseName\\UseCaseNameCommandHandler.ca"
            });

            result.Add(new FileDto($"{useCaseName}CommandValidator.cs")
            {
                Path = "ca-use-case\\Features\\FeatureName\\Commands\\UseCaseName\\UseCaseNameCommandValidator.ca"
            });
        }
    
        foreach (var item in result)
        {
            string fileContent = ResourceHelper.ReadEmbeddedTextFile(item.Path);

            item.Content = fileContent
                .Replace("FeatureName", featureName)
                .Replace("UseCaseName", useCaseName)
                .Replace("object", returnType)
                .Replace("CleanArchitecture.", solutionName+".");
        }

        if (returnType == "void")
        {
            foreach (var item in result)
            {
                item.Content = item.Content
                    .Replace("<void>", "")
                    .Replace("void MyProperty", "string MyProperty")
                    .Replace("return request.MyProperty;", "return BaseResult.Ok();");
            }
        }
        else if (returnType == "CustomObject")
        {
            var className = $"{useCaseName}Response";
            var featureType = type == UseCaseType.Command ? "Commands" : "Queries";

            if (type == UseCaseType.QueryPagedList)
                useCaseName += "PagedList";

            result.Add(new FileDto($"{className}.cs")
            {
                Content = @$"namespace {solutionName}.Application.Features.{featureName}.{featureType}.{useCaseName};

public class {className}
{{
}}",
            });

            foreach (var item in result)
            {
                item.Content = item.Content
                    .Replace("CustomObject", className)
                    .Replace("return request.MyProperty;", $"return new {className}() {{ }};");
            }

        }
     
        return result;
    }

}
