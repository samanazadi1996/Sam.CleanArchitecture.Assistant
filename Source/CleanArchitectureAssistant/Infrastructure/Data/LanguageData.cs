using CleanArchitectureAssistant.Infrastructure.DTOs;
using CleanArchitectureAssistant.Infrastructure.Services;
using System.Collections.Generic;

namespace CleanArchitectureAssistant.Infrastructure.Data;

public class LanguageData
{
    public static List<FileDto> GetData(string culture)
    {
        List<FileDto> result = [];

        result.Add(new FileDto($"ResourceGeneral.{culture}.resx")
        {
            Path = "ca-resource\\ProjectResources\\ResourceGeneral.Culture.ca"
        });

        result.Add(new FileDto($"ResourceMessages.{culture}.resx")
        {
            Path = "ca-resource\\ProjectResources\\ResourceMessages.Culture.ca"
        });

        foreach (var item in result)
        {
            item.Content = EmbeddedResourceDataReader.ReadEmbeddedTextFile(item.Path);
        }


        return result;
    }

}