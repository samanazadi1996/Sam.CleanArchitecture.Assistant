using CleanArchitectureAssistant.Infrastructure.Data;
using CleanArchitectureAssistant.Infrastructure.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                    item.Content = await Clone(item, baseCulture);

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

    private static async Task<string> Clone(FileDto item, string baseCulture)
    {
        try
        {
            var filename = baseCulture
                .Split('-')
                .FirstOrDefault(p => p.Trim().Split('.')[0] == item.Name.Split('.')[0]) ?? "";

            var from = filename.Split('.').Length > 2 ? filename.Split('.')[1] : "en";
            var to = item.Name.Split('.')[1];

            var rp = Path.Combine(await CommonService.GetResourcesPath(), "ProjectResources", filename.Trim());
            if (File.Exists(rp))
            {

                var content = File.ReadAllText(rp);

                var xdoc = XDocument.Parse(content);



                foreach (var dataElement in xdoc.Descendants("data"))
                {
                    string value = dataElement.Element("value")?.Value ?? string.Empty;

                    var translated = await GoogleApiService.Translate(value, from, to);

                    content = content.Replace(value, translated);
                }

                return content;
            }
        }
        catch (Exception e)
        {
        }

        return item.Content;

    }
}