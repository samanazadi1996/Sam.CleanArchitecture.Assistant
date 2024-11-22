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
        var projects = await VS.Solutions.GetAllProjectsAsync();

        var csProj = projects.Select(p => p.FullPath)
            .FirstOrDefault(p => p.EndsWith(".Infrastructure.Resources.csproj"));

        if (string.IsNullOrEmpty(csProj))
            return string.Empty;

        return Directory.GetParent(csProj)?.FullName;
    }
    public static async Task<string> GetApplicationPath()
    {
        var projects = await VS.Solutions.GetAllProjectsAsync();

        var applicationCsProj = projects.Select(p => p.FullPath).FirstOrDefault(p => p.EndsWith(".Application.csproj"));

        if (string.IsNullOrEmpty(applicationCsProj))
            return string.Empty;

        return Directory.GetParent(applicationCsProj)?.FullName;
    }


}