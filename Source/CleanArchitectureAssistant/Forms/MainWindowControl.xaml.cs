using CleanArchitectureAssistant.Infrastructure.Services;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows;
using System.Windows.Controls;

namespace CleanArchitectureAssistant.Forms;

public partial class MainWindowControl : UserControl
{
    public MainWindowControl()
    {
        InitializeComponent();
    }

    private async void ShowAddUseCaseWindow(object sender, RoutedEventArgs e)
    {
        await Forms.AddUseCase.AddUseCaseWindow.ShowAsync();
    }
    private async void ShowAddLanguageWindow(object sender, RoutedEventArgs e)
    {
        await Forms.AddLanguage.AddLanguageWindow.ShowAsync();
    }
    private async void ShowAddEntityWindow(object sender, RoutedEventArgs e)
    {
        await Forms.AddEntity.AddEntityWindow.ShowAsync();
    }

    private async void ShowAddMigrationWindow(object sender, RoutedEventArgs e)
    {
        await Forms.AddMigration.AddMigrationWindow.ShowAsync();
    }

    private async void ShowAddRepositoryWindow(object sender, RoutedEventArgs e)
    {
        await Forms.AddRepository.AddRepositoryWindow.ShowAsync();
    }

    private void AddIssues_OnClick(object sender, RoutedEventArgs e)
    {
        ExternalService.NavigateToUrl("https://github.com/samanazadi1996/Sam.CleanArchitecture.Assistant/issues");
    }

    private async void CheckForUpdate_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var version = await UpdateService.GetLatestVersion();

            if (version is null)
            {
                await VS.MessageBox.ShowAsync("Network error occurred. Please try again later.");
                return;
            }

            var currentVersion = GetCurrentVersion();
            if (version != currentVersion)
            {
                var result =
                    await VS.MessageBox.ShowAsync(
                        $"A new version ({version}) is available.", "Do you want to download it?"
                        , OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_YESNO);

                if (result == VSConstants.MessageBoxResult.IDYES)
                {
                    ExternalService.NavigateToUrl("https://marketplace.visualstudio.com/items?itemName=SamanAzadi1996.ASPDotnetCoreCleanArchitecture");
                }
            }
            else
            {
                await VS.MessageBox.ShowAsync("You are using the latest version.");
            }
        }
        catch (Exception ex)
        {
            await VS.MessageBox.ShowAsync($"An error occurred: {ex.Message}");
        }
    }

    // متدی برای دریافت نسخه فعلی افزونه
    private string GetCurrentVersion()
    {
        return "1.0.0"; // مقدار نسخه فعلی را تنظیم کنید
    }
}
