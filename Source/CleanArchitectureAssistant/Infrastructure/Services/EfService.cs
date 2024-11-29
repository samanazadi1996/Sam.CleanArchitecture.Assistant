using CleanArchitectureAssistant.Infrastructure.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class EfService
{
    public static async Task<bool> AddMigration(string dataLayer, string startUpProject, string migrationName, string context)
    {
        try
        {
            var classLibs =await CommonService.GetProjectsPath();

            var startUp = classLibs.FirstOrDefault(p => p.Contains(startUpProject));
            var data = classLibs.FirstOrDefault(p => p.Contains(dataLayer));

            var cli = $" ef migrations add {migrationName} --context {context} --project \"{data}\" --startup-project \"{startUp}\"";


            CmdService.Shell("dotnet", cli);
        }
        catch
        {
            return false;
        }

        return true;
    }
}