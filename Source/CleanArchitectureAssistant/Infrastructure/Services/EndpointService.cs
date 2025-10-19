using CleanArchitectureAssistant.Infrastructure.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class EndpointService
{
    public static async Task<bool> CreateEndpoint(string EndpointName)
    {
        try
        {
            var solutionName = await CommonService.GetSolutionName();

            var file = EndpointData.GetEndpoint(solutionName, EndpointName);

            var endpointPath = await CommonService.GetEndpointPath();

            var dir = Path.Combine(endpointPath, "Endpoints");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var newFile = Path.Combine(dir, file.Name);

            if (File.Exists(newFile))
                return false;

            File.WriteAllText(newFile, file.Content);

        }
        catch
        {
            return false;
        }

        return true;

    }

}