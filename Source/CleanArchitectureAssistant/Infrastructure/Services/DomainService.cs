using CleanArchitectureAssistant.Infrastructure.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class DomainService
{
    public static async Task<bool> CreateEntity(string domainName, string entityName, string baseType, string keyPropertyTypeComboBox)
    {
        try
        {
            var domainPath = await CommonService.GetDomainPath();
            if (string.IsNullOrEmpty(domainPath))
                return false;


            var dir = Path.Combine(domainPath, domainName, "Entities");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);


            var file = EntityData.GetEntity(await CommonService.GetSolutionName(), domainName, entityName, baseType, keyPropertyTypeComboBox);

            File.WriteAllText(Path.Combine(dir, file.Name), file.Content);

        }
        catch
        {
            return false;
        }

        return true;
    }
    public static async Task<List<string>> GetDonmains()
    {
        var domainPath = await CommonService.GetDomainPath();
        if (string.IsNullOrEmpty(domainPath))
            return [];

        var dir = Path.Combine(domainPath);

        return Directory.GetDirectories(dir)
            .Select(Path.GetFileName)
            .ToList();
    }
}