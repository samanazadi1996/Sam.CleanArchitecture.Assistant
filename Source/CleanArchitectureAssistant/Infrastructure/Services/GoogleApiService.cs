using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class GoogleApiService
{
    static readonly HttpClient HttpClient = new HttpClient();
    public static async Task<string> Translate(string text, string baseCulture, string targetCulture)
    {
        try
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={baseCulture}&tl={targetCulture}&dt=t&dj=1&q={Uri.EscapeDataString(text)}";
            var response = await HttpClient.GetStringAsync(url);
            var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(response,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var translatedText = translationResponse?.Sentences?.FirstOrDefault()?.Trans ?? text;
            return translatedText;
        }
        catch (Exception e)
        {
            return null;
        }

    }
    public class TranslationResponse
    {
        public List<Sentence> Sentences { get; set; }
    }

    public class Sentence
    {
        public string Trans { get; set; }
    }

}