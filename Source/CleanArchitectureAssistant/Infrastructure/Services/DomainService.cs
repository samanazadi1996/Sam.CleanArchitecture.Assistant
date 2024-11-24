using CleanArchitectureAssistant.Infrastructure.Data;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class DomainService
{
    public static async Task<bool> CreateEntity(string domainname, string entityName, string baseType, string keyProperyTypeComboBox)
    {
        try
        {
            var domainPath = await CommonService.GetDomainPath();
            if (string.IsNullOrEmpty(domainPath))
                return false;


            var dir = Path.Combine(domainPath, domainname, "Entities");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);


            var file = EntityData.GetEntity(await CommonService.GetSolutionName(), domainname, entityName, baseType, keyProperyTypeComboBox);

            File.WriteAllText(Path.Combine(dir, file.Name), file.Content);

        }
        catch
        {
            return false;
        }

        return true;
    }
}