using System.Windows;
using System.Windows.Controls;

namespace CleanArchitectureTools;

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
}
