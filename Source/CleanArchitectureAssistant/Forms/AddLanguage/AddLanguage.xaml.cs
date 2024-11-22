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

    private async void Execute(object sender, RoutedEventArgs e)
    {
        var culture = CultureComboBox.Text;
        if (string.IsNullOrWhiteSpace(culture))
        {
            await VS.MessageBox.ShowAsync("Please select a Culture.");
            return;
        }

        if (await LanguageService.CreateLanguage(culture))
        {
            await VS.MessageBox.ShowAsync("Language added successfully.");
            CultureComboBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to add Language. Please try again.");
        }
    }
}
