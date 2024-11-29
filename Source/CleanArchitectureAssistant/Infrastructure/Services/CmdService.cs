using System.Diagnostics;

namespace CleanArchitectureAssistant.Infrastructure.Services;

internal class CmdService
{
    public static string Shell(string app, string arg)
    {

        var startInfo = new ProcessStartInfo
        {
            FileName = app,
            Arguments = arg,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        var result = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        return result;
    }

}