using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;
public class UpdateService
{
    public static async Task<string> GetLatestVersion()
    {
        using var httpClient = new HttpClient();
        // Set up the request headers
        httpClient.DefaultRequestHeaders.Add("accept", "application/json;api-version=7.2-preview.1;excludeUrls=true");
        httpClient.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=0.9,fa-IR;q=0.8,fa;q=0.7,de;q=0.6");
        httpClient.DefaultRequestHeaders.Add("priority", "u=1, i");
        httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Google Chrome\";v=\"131\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"");
        httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
        httpClient.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
        httpClient.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
        httpClient.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
        httpClient.DefaultRequestHeaders.Add("x-requested-with", "XMLHttpRequest");
        httpClient.DefaultRequestHeaders.Add("x-tfs-session", "031e5dfe-34f4-4547-98ce-1978234d1578");

        // Prepare the request body
        var requestBody = new
        {
            assetTypes = (object)null,
            filters = new[]
            {
                new
                {
                    criteria = new[]
                    {
                        new { filterType = 7, value = "SamanAzadi1996.ASPDotnetCoreCleanArchitecture" }
                    },
                    direction = 2,
                    pageSize = 100,
                    pageNumber = 1,
                    sortBy = 0,
                    sortOrder = 0,
                    pagingToken = (object)null
                }
            },
            flags = 2151
        };

        var jsonBody = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        try
        {
            // Send the POST request
            var response = await httpClient.PostAsync("https://marketplace.visualstudio.com/_apis/public/gallery/extensionquery", content);

            // Check the response
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Rootobject>(responseContent).results[0].extensions[0].versions[0]
                    .version;

            }
        }
        catch (Exception ex)
        {
        }


        return null;
    }


    private class Rootobject
    {
        public Result[] results { get; set; }
    }

    private class Result
    {
        public Extension[] extensions { get; set; }
    }

    private class Extension
    {
        public Version[] versions { get; set; }
    }


    private class Version
    {
        public string version { get; set; }
    }

}