using CleanArchitectureAssistant.Infrastructure.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class ControllerService
{
    public static async Task<List<string>> GetControllerVersions()
    {
        var endpointPath = await CommonService.GetEndpointPath();
        if (string.IsNullOrEmpty(endpointPath))
            return [];

        var dir = Path.Combine(endpointPath, "Controllers");

        return Directory.GetDirectories(dir)
            .Select(Path.GetFileName)
            .ToList();
    }
    public static async Task<bool> CreateController(string controllerName, string version)
    {
        try
        {
            var solutionName = await CommonService.GetSolutionName();

            var file = ControllerData.GetController(solutionName, controllerName, GetVersion(version));

            var endpointPath = await CommonService.GetEndpointPath();

            var dir = Path.Combine(endpointPath, "Controllers", $"v{GetVersion(version)}");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var newFile = Path.Combine(dir, file.Name);

            if (File.Exists(newFile))
                return false;

            File.WriteAllText(newFile, file.Content);

            int GetVersion(string version)
            {
                var tmp = version.Where(p => "1234567890".Contains(p));

                return tmp.Count() > 0 ? Convert.ToInt32(string.Concat(tmp)) : 1;
            }
        }
        catch
        {
            return false;
        }

        return true;

    }

}