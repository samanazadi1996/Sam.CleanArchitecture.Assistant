
using CleanArchitectureAssistant.Infrastructure.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.OLE.Interop;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class EfService
{
    public static async Task<bool> AddMigration(string dataLayer, string startUpProject, string migrationName, string context)
    {
        try
        {
            var classLibs = await CommonService.GetProjectsPath();

            var startUp = classLibs.FirstOrDefault(p => p.Contains(startUpProject));
            var data = classLibs.FirstOrDefault(p => p.Contains(dataLayer));

            var cli = $" ef migrations add {migrationName} --context {context} --project \"{data}\" --startup-project \"{startUp}\"";

            var cmdResult = CmdService.Shell("dotnet", cli);


            if (cmdResult.Contains("Build failed"))
            {
                var nsg = cmdResult.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                    .FirstOrDefault(P => P.Contains("Build failed"));
                if (!string.IsNullOrEmpty(nsg))
                {
                    await VS.MessageBox.ShowAsync(nsg);
                    return false;
                }
            }
        }
        catch
        {
            return false;
        }

        return true;
    }
}