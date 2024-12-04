using CleanArchitectureAssistant.Infrastructure.Services;
using System.Windows;
using System.Windows.Controls;
using static CleanArchitectureAssistant.Infrastructure.Services.CommonService;

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
}
