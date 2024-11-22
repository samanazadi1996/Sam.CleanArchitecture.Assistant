using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureAssistant.Infrastructure.Data;
using CleanArchitectureAssistant.Infrastructure.Enums;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class LanguageService
{
    public static async Task<bool> CreateLanguage(string culture)
    {
        try
        {
            var applicationPath = await CommonService.GetResourcesPath();
            if (string.IsNullOrEmpty(applicationPath))
                return false;


            var dir = Path.Combine(applicationPath, "ProjectResources");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (var item in LanguageData.GetData(culture))
            {
                File.WriteAllText(Path.Combine(dir, item.Name), item.Content);
            }
        }
        catch
        {
            return false;
        }

        return true;
    }

}