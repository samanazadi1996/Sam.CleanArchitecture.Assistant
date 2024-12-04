using CleanArchitectureAssistant.Infrastructure.Data;
using CleanArchitectureAssistant.Infrastructure.DTOs;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class RepositoryService
{
    public static async Task<bool> AddRepository(EntityDto entityDto)
    {
        try
        {
            var domainPath = await CommonService.GetDomainPath();
            if (string.IsNullOrEmpty(domainPath))
                return false;

            var @interface = RepositoryData.GetInterface(await CommonService.GetSolutionName(), entityDto);

            var @implementation = RepositoryData.GetImplementation(await CommonService.GetSolutionName(), entityDto);

            var applicationPath = await CommonService.GetApplicationPath();
            if (string.IsNullOrEmpty(applicationPath))
                return false;

            var persistencePath = await CommonService.GetPersistencePath();
            if (string.IsNullOrEmpty(applicationPath))
                return false;

            var interfacesPath = Path.Combine(applicationPath, "Interfaces", "Repositories", @interface.Name);
            var implementationPath = Path.Combine(persistencePath, "Repositories", @implementation.Name);

            File.WriteAllText(interfacesPath, @interface.Content);

            File.WriteAllText(implementationPath, @implementation.Content);

        }

        catch
        {
            return false;
        }

        return true;
    }
}