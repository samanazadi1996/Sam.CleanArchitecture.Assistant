using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class CommonService
{
    public static async Task<string> GetSolutionName()
    {
        var slnName = (await VS.Solutions.GetCurrentSolutionAsync())?.Name;

        return slnName?.Replace(".sln", "");
    }
    public static async Task<string> GetResourcesPath()
    {
        return await GetProjectPath("Infrastructure.Resources");
    }
    public static async Task<string> GetApplicationPath()
    {
        return await GetProjectPath("Application");
    }
    public static async Task<string> GetEndpointPath()
    {
        return await GetProjectPath("WebApi");
    }
    public static async Task<string> GetProjectPath(string name)
    {
        var projects = await VS.Solutions.GetAllProjectsAsync();

        var applicationCsProj = projects.Select(p => p.FullPath)
            .FirstOrDefault(p => p.EndsWith($".{name}.csproj"));

        if (string.IsNullOrEmpty(applicationCsProj))
            return string.Empty;

        return Directory.GetParent(applicationCsProj)?.FullName;
    }


}