using System.Collections.Generic;
using System.IO;
using CleanArchitectureAssistant.Infrastructure.Enums;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureAssistant.Infrastructure.Data;

namespace CleanArchitectureAssistant.Infrastructure.Services;

internal class ApplicationService
{
    internal static async Task<bool> CreateUseCase(string featureName, string useCaseName, UseCaseType type,string returnType)
    {
        var useCaseTypes = new Dictionary<UseCaseType, string>()
        {
            {UseCaseType.Command,"Commands"},
            {UseCaseType.Query,"Queries"},
            {UseCaseType.QueryPagedList,"Queries"}
        };
        try
        {
            var applicationPath = await GetApplicationPath();
            if (string.IsNullOrEmpty(applicationPath))
                return false;

            var dir = Path.Combine(applicationPath, "Features",featureName, useCaseTypes[type],useCaseName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (var item in UseCaseData.GetData(await CommonService.GetSolutionName(), featureName, useCaseName, type, returnType))
            {
                File.WriteAllText(Path.Combine(dir,item.Name),item.Content);
            }
        }
        catch
        {
            return false;
        }

        return true;
    }

    internal static async Task<string> GetApplicationPath()
    {
        var projects = await VS.Solutions.GetAllProjectsAsync();

        var applicationCsProj = projects.Select(p => p.FullPath).FirstOrDefault(p => p.EndsWith(".Application.csproj"));

        if (string.IsNullOrEmpty(applicationCsProj))
            return string.Empty;

        return Directory.GetParent(applicationCsProj)?.FullName;
    }

    internal static async Task<List<string>> GetFeatures()
    {
        var applicationPath = await GetApplicationPath();
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