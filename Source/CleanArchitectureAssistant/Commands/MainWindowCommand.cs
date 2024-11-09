namespace CleanArchitectureAssistant;

[Command(PackageIds.ShowMainWindowCommand)]
internal sealed class MainWindowCommand : BaseCommand<MainWindowCommand>
{
    protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        return MainWindow.ShowAsync();
    }
}
