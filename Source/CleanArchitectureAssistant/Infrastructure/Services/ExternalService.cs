using System.Diagnostics;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public static class ExternalService
{
    public static void NavigateToUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch
        {
            // ignored
        }
    }

}