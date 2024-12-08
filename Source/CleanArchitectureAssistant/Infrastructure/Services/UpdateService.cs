using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class UpdateService
{
    public static async Task<VersionDto> GetLatestVersion()
    {
        using var httpClient = new HttpClient();
        // Set up the request headers
        httpClient.DefaultRequestHeaders.Add("accept", "application/json;api-version=7.2-preview.1;excludeUrls=true");

        // Prepare the request body
        var requestBody = new
        {
            filters = new[]
            {
                new
                {
                    criteria = new[]
                    {
                        new { filterType = 7, value = "SamanAzadi1996.ASPDotnetCoreCleanArchitecture" }
                    },
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
                var jsop = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GetLatestVersionDto>(responseContent, jsop).Results[0].Extensions[0].Versions[0];

            }
        }
        catch (Exception ex)
        {
            // ignored
        }


        return null;
    }
}