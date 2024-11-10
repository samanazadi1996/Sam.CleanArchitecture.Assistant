using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class CommonService
{
    public static async Task<string> GetSolutionName()
    {
        var slnName =(await VS.Solutions.GetCurrentSolutionAsync())?.Name;

        return slnName?.Replace(".sln", "");
    }
}