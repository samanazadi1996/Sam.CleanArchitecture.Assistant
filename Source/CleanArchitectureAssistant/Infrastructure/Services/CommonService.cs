using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

internal class CommonService
{
    internal static async Task<string> GetSolutionName()
    {
        var slnName =(await VS.Solutions.GetCurrentSolutionAsync())?.Name;

        return slnName?.Replace(".sln", "");
    }
}