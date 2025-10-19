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
        await AddUseCase.AddUseCaseWindow.ShowAsync();
    }
    private async void ShowAddLanguageWindow(object sender, RoutedEventArgs e)
    {
        await AddLanguage.AddLanguageWindow.ShowAsync();
    }
    private async void ShowAddEntityWindow(object sender, RoutedEventArgs e)
    {
        await AddEntity.AddEntityWindow.ShowAsync();
    }

    private async void ShowAddMigrationWindow(object sender, RoutedEventArgs e)
    {
        await AddMigration.AddMigrationWindow.ShowAsync();
    }

    private async void ShowAddRepositoryWindow(object sender, RoutedEventArgs e)
    {
        await AddRepository.AddRepositoryWindow.ShowAsync();
    }

    private async void ShowSettingsWindow(object sender, RoutedEventArgs e)
    {
        await Settings.SettingsWindow.ShowAsync();
    }

    private async void ShowAddControllerWindow(object sender, RoutedEventArgs e)
    {
        await AddController.AddControllerWindow.ShowAsync();
    }
    private async void ShowAddEndpointWindow(object sender, RoutedEventArgs e)
    {
        await AddEndpoint.AddEndpointWindow.ShowAsync();
    }
}
