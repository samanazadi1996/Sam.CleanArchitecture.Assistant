using System.Linq;
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
        UseGoogleTranslateBaseStackPanel.Visibility = UseGoogleTranslateCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
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

        string baseCulture = null;
        var useBaseCulture = UseGoogleTranslateCheckBox.IsChecked == true;
        if (useBaseCulture)
        {
            baseCulture = UseGoogleTranslateBaseComboBox.Text;
            if (string.IsNullOrWhiteSpace(baseCulture))
            {
                await VS.MessageBox.ShowAsync("Please select a Base Culture For Clone.");
                return;
            }
        }

        var setAsDefault = SetAsDefaultCheckBox.IsChecked == true;

        if (await LanguageService.CreateLanguage(culture, setAsDefault, baseCulture))
        {
            await VS.MessageBox.ShowAsync("Language added successfully.");
            CultureComboBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to add Language. Please try again.");
        }
    }

    private async void UseGoogleTranslateCheckBox_OnChecked(object sender, RoutedEventArgs e)
    {
        UseGoogleTranslateBaseStackPanel.Visibility = Visibility.Visible;
        var languages =await LanguageService.GetApplicationLanguages();
        
        UseGoogleTranslateBaseComboBox.Items.Clear();

        foreach (var item in languages.OrderBy(p=>p.Length))
        {
            UseGoogleTranslateBaseComboBox.Items.Add(new ComboBoxItem()
            {
                Content = item
            });
        }

        if (languages.Any())
            UseGoogleTranslateBaseComboBox.SelectedIndex = 0;
    }

    private async void UseGoogleTranslateCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
    {
        UseGoogleTranslateBaseStackPanel.Visibility = Visibility.Hidden;
    }
}
