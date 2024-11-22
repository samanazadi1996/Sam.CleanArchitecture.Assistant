using System.IO;
using System.Reflection;

namespace CleanArchitectureAssistant.Infrastructure.Data;

public class EmbeddedResourceDataReader
{
    public static string ReadEmbeddedTextFile(string relativePath)
    {
        // Convert relative path to resource name
        var resourceName = $"CleanArchitectureAssistant.Resources.Appdata.{relativePath.Replace("\\", ".").Replace("-", "_")}";

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            return "";

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
    public static void ListEmbeddedResources()
    {
        var assembly = Assembly.GetExecutingAssembly();
        string[] resources = assembly.GetManifestResourceNames();

        Console.WriteLine("Embedded Resources:");
        foreach (string resource in resources)
        {
            Console.WriteLine(resource);
        }
    }
}