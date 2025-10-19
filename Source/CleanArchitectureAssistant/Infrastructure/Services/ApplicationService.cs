using System.Collections.Generic;
using System.IO;
using CleanArchitectureAssistant.Infrastructure.Enums;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureAssistant.Infrastructure.Data;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class ApplicationService
{
    public static async Task<bool> CreateUseCase(string featureName, string useCaseName, UseCaseType type, string returnType, bool useInternalMediator)
    {
        var useCaseTypes = new Dictionary<UseCaseType, string>()
        {
            {UseCaseType.Command,"Commands"},
            {UseCaseType.Query,"Queries"},
            {UseCaseType.QueryPagedList,"Queries"}
        };
        try
        {
            var applicationPath = await CommonService.GetApplicationPath();
            if (string.IsNullOrEmpty(applicationPath))
                return false;

            var un = useCaseName + (type == UseCaseType.QueryPagedList ? "PagedList" : "");

            var dir = Path.Combine(applicationPath, "Features", featureName, useCaseTypes[type], un);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (var item in UseCaseData.GetData(await CommonService.GetSolutionName(), featureName, un, type, returnType, useInternalMediator))
            {
                File.WriteAllText(Path.Combine(dir, item.Name), item.Content);
            }
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static async Task<List<string>> GetFeatures()
    {
        var applicationPath = await CommonService.GetApplicationPath();
        if (string.IsNullOrEmpty(applicationPath))
            return [];

        var dir = Path.Combine(applicationPath, "Features");

        if (!Directory.Exists(dir))
            return [];

        return Directory.GetDirectories(dir)
            .Select(Path.GetFileName)
            .ToList();
    }
}