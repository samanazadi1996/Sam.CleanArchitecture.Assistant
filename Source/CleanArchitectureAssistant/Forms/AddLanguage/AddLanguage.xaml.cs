using CleanArchitectureAssistant.Infrastructure.Services;
using System.Windows.Controls;
using CleanArchitectureAssistant.Infrastructure.Enums;
using System.Windows;

namespace CleanArchitectureAssistant.Forms.AddLanguage;

public partial class AddLanguageWindowControl : UserControl
{
    public AddLanguageWindowControl()
    {
        InitializeComponent();

    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddLanguageWindow.HideAsync();
    }

    private void Execute(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
