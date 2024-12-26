global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using CleanArchitectureAssistant.Forms;
using CleanArchitectureAssistant.Forms.AddEntity;
using CleanArchitectureAssistant.Forms.AddLanguage;
using CleanArchitectureAssistant.Forms.AddMigration;
using CleanArchitectureAssistant.Forms.AddRepository;
using CleanArchitectureAssistant.Forms.AddUseCase;
using CleanArchitectureAssistant.Forms.Settings;
using System.Runtime.InteropServices;
using System.Threading;
using CleanArchitectureAssistant.Forms.AddController;

namespace CleanArchitectureAssistant;

[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
[ProvideToolWindow(typeof(MainWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideToolWindow(typeof(AddUseCaseWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideToolWindow(typeof(AddLanguageWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideToolWindow(typeof(AddEntityWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideToolWindow(typeof(AddMigrationWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideToolWindow(typeof(AddRepositoryWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideToolWindow(typeof(SettingsWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
[ProvideToolWindow(typeof(AddControllerWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
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
