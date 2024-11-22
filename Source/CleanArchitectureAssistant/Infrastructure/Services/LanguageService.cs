using System.Collections.Generic;
using CleanArchitectureAssistant.Infrastructure.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class LanguageService
{
    public static async Task<bool> CreateLanguage(string culture, bool setAsDefault, string baseCulture)
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
                if (!string.IsNullOrEmpty(baseCulture))
                    item.Content = await Clone(item.Content);

                File.WriteAllText(Path.Combine(dir, item.Name), item.Content);
            }

            var endpointPath = await CommonService.GetEndpointPath();
            if (string.IsNullOrEmpty(endpointPath))
                return false;

            var appsettingsPath = Path.Combine(endpointPath, "appsettings.json");
            if (File.Exists(appsettingsPath))
            {
                var hasChange = false;
                var jsonContent = File.ReadAllText(appsettingsPath);

                var root = JsonNode.Parse(jsonContent);
                var supportedCultures = root?["Localization"]?["SupportedCultures"]?.AsArray();

                if (supportedCultures != null)
                {
                    var cultures = supportedCultures
                        .Select(node => node?.ToString()).Where(c => !string.IsNullOrEmpty(c)).ToList();

                    if (!cultures.Contains(culture))
                    {
                        supportedCultures.Add(culture);
                        hasChange = true;
                    }

                }

                if (setAsDefault)
                {
                    var defaultRequestCulture = root?["Localization"]?["DefaultRequestCulture"];
                    if (defaultRequestCulture != null)
                    {
                        root["Localization"]["DefaultRequestCulture"] = culture;
                        hasChange = true;
                    }
                }

                if (hasChange)
                    File.WriteAllText(appsettingsPath, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

            }


        }
        catch
        {
            return false;
        }

        return true;
    }

    private static async Task<string> Clone(string itemContent)
    {
        return itemContent;
    }
    public static async Task<List<string>> GetApplicationLanguages()
    {
        try
        {
            List<string> result = [];
            var applicationPath = await CommonService.GetResourcesPath();

            var dir = Path.Combine(applicationPath, "ProjectResources");

            var ee = Directory.GetFiles(dir);

            var temp = ee.Select(p => Path.GetFileName(p))
                .Where(p => p.EndsWith(".resx"))
                .Select(p => p.Split('.'))
                .GroupBy(p => p[1]);

            foreach (var item in temp)
            {
                result.Add(string.Join(" - ", item.Select(p => string.Join(".", p))));
            }

            return result;
        }
        catch (Exception e)
        {
            return [];
        }

    }
}