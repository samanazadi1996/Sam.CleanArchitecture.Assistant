using CleanArchitectureAssistant.Infrastructure.Services;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.Reflection;
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
    }


    private void AddIssues_OnClick(object sender, RoutedEventArgs e)
    {
        ExternalService.NavigateToUrl("https://github.com/samanazadi1996/Sam.CleanArchitecture.Assistant/issues");
    }

    private async void CheckForUpdate_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var latestVersion = await UpdateService.GetLatestVersion();

            if (latestVersion is null)
            {
                await VS.MessageBox.ShowAsync("Network error occurred. Please try again later.");
                return;
            }

            if (HasNewVersion(latestVersion.LastUpdated))
            {
                var result =
                    await VS.MessageBox.ShowAsync(
                        $"A new version ({latestVersion.Version}) is available.", "Do you want to download it?"
                        , OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_YESNO);

                if (result == VSConstants.MessageBoxResult.IDYES)
                {
                    ExternalService.NavigateToUrl("https://marketplace.visualstudio.com/items?itemName=SamanAzadi1996.CleanArchitectureAssistant");
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
    private bool HasNewVersion(string lastUpdated)
    {
        try
        {
            var lastBuild = System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString();

            var dtLastUpdated = DateTime.Parse(lastUpdated);
            var dtLastBuild = DateTime.Parse(lastBuild);

            return dtLastUpdated > dtLastBuild;
        }
        catch (Exception)
        {
            // ignored
        }

        return false;
    }


    private async void Back_OnClick(object sender, RoutedEventArgs e)
    {
        await SettingsWindow.HideAsync();
    }
}
