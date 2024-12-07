using CleanArchitectureAssistant.Infrastructure.Services;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows;
using System.Windows.Controls;

namespace CleanArchitectureAssistant.Forms.Settings;

public partial class SettingsWindowControl : UserControl
{
    public SettingsWindowControl()
    {
        InitializeComponent();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await SettingsWindow.HideAsync();
    }

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadFeatures();
    }


    private async Task LoadFeatures()
    {
    }
    private async void Execute(object sender, RoutedEventArgs e)
    {


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
    private string GetCurrentVersion()
    {
        return "1.0.0"; // مقدار نسخه فعلی را تنظیم کنید
    }


    private async void Back_OnClick(object sender, RoutedEventArgs e)
    {
        await SettingsWindow.HideAsync();
    }
}
