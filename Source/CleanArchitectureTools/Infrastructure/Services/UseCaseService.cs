using CleanArchitectureTools.Infrastructure.Data;
using CleanArchitectureTools.Infrastructure.Enums;
using System.Linq;

namespace CleanArchitectureTools.Infrastructure.Services;

internal class UseCaseService
{

    internal static async Task CreateUseCase(string featureName, string useCaseName, UseCaseType type)
    {
        var projects = await VS.Solutions.GetAllProjectsAsync();

        var applicationCsProj = projects.Select(p => p.FullPath).FirstOrDefault(p => p.EndsWith(".Application.csproj"));

        if (string.IsNullOrEmpty(applicationCsProj))
            return;

        var applicationLayer = System.IO.Directory.GetParent(applicationCsProj);

        var data = UseCaseData.GetData(featureName, useCaseName, type);



    }
}

