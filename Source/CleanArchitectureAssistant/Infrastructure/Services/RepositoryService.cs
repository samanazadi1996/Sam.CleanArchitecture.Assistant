using CleanArchitectureAssistant.Infrastructure.Data;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class RepositoryService
{
    public static async Task<bool> AddRepository(string entityName)
    {
        try
        {
            var domainPath = await CommonService.GetDomainPath();
            if (string.IsNullOrEmpty(domainPath))
                return false;

            var @interface = RepositoryData.GetInterface(await CommonService.GetSolutionName(), entityName);

            var @implementation = RepositoryData.GetImplementation(await CommonService.GetSolutionName(), entityName);

            //File.WriteAllText(Path.Combine(dir, file.Name), file.Content);

        }
        catch
        {
            return false;
        }

        return true;
    }

}