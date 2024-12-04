using Microsoft.VisualStudio.Shell.Interop;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class CommonService
{
    public static async Task<string> GetSolutionName()
    {
        var slnName = (await VS.Solutions.GetCurrentSolutionAsync())?.Name;

        return slnName?.Replace(".sln", "");
    }
    public static async Task<List<string>> GetProjectsPath()
    {
        return await GetProjectsPath(".csproj");
    } 
    public static async Task<string> GetResourcesPath()
    {
        return await GetProjectPath("Infrastructure.Resources.csproj");
    }
    public static async Task<string> GetApplicationPath()
    {
        return await GetProjectPath("Application.csproj");
    }
    public static async Task<string> GetEndpointPath()
    {
        return await GetProjectPath("WebApi.csproj");
    }
    public static async Task<string> GetDomainPath()
    {
        return await GetProjectPath("Domain.csproj");
    }
    public static async Task<string> GetProjectPath(string name)
    {
        var projects = await VS.Solutions.GetAllProjectsAsync();

        var applicationCsProj = projects.Select(p => p.FullPath)
            .FirstOrDefault(p => p.Contains(name));

        if (string.IsNullOrEmpty(applicationCsProj))
            return string.Empty;

        return Directory.GetParent(applicationCsProj)?.FullName;
    }
    public static async Task<List<string>> GetProjectsPath(string name)
    {
        var projects = await VS.Solutions.GetAllProjectsAsync();

        var applicationCsProj = projects.Select(p => p.FullPath)
            .Where(p => p.Contains(name)).ToList();

        return !applicationCsProj.Any() ? [] : applicationCsProj.Select(p => Directory.GetParent(p)?.FullName).ToList();
    }

    internal static async Task<string> GetPersistencePath()
    {
        return await GetProjectPath("Infrastructure.Persistence.csproj");
    }
}