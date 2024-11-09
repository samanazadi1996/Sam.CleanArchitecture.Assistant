global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using CleanArchitectureAssistant.AddUseCase;
using System.Runtime.InteropServices;
using System.Threading;

namespace CleanArchitectureAssistant;

[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
[ProvideToolWindow(typeof(MainWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideToolWindow(typeof(AddUseCaseWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideMenuResource("Menus.ctmenu", 1)]
[Guid(PackageGuids.CleanArchitectureAssistantString)]
public sealed class CleanArchitectureAssistantPackage : ToolkitPackage
{
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        await this.RegisterCommandsAsync();

        this.RegisterToolWindows();
    }
}
