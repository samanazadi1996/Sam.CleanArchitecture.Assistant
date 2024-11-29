using CleanArchitectureAssistant.Infrastructure.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CleanArchitectureAssistant.Forms.AddLanguage;

public partial class AddLanguageWindowControl : UserControl
{
    private readonly Dictionary<string, string> _languages = new();

    public AddLanguageWindowControl()
    {
        InitializeComponent();
        UseGoogleTranslateBaseStackPanel.Visibility =
            UseGoogleTranslateCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        LoadCultures();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddLanguageWindow.HideAsync();
    }

    private async void Execute(object sender, RoutedEventArgs e)
    {
        var culture = CultureComboBox.Text;

        if (_languages.TryGetValue(culture, out var cul))
            culture = cul;


        if (string.IsNullOrWhiteSpace(culture))
        {
            await VS.MessageBox.ShowAsync("Please select a valid culture from the dropdown list.");
            return;
        }

        string baseCulture = null;
        var useBaseCulture = UseGoogleTranslateCheckBox.IsChecked == true;
        if (useBaseCulture)
        {
            baseCulture = UseGoogleTranslateBaseComboBox.Text;
            if (string.IsNullOrWhiteSpace(baseCulture))
            {
                await VS.MessageBox.ShowAsync("Please select a base culture to clone.");
                return;
            }
        }

        var setAsDefault = SetAsDefaultCheckBox.IsChecked == true;

        if (await LanguageService.CreateLanguage(culture, setAsDefault, baseCulture))
        {
            await VS.MessageBox.ShowAsync("The language has been added successfully.");
            CultureComboBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("An error occurred while adding the language. Please try again.");
        }
    }

    private async void UseGoogleTranslateCheckBox_OnChecked(object sender, RoutedEventArgs e)
    {
        UseGoogleTranslateBaseStackPanel.Visibility = Visibility.Visible;
        var languages = await LanguageService.GetApplicationLanguages();

        UseGoogleTranslateBaseComboBox.Items.Clear();

        foreach (var item in languages.OrderBy(p => p.Length))
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

    void LoadCultures()
    {
        var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
            .OrderBy(c => c.DisplayName)
            .Where(c => !string.IsNullOrWhiteSpace(c.Name));


        CultureComboBox.Items.Clear();
        _languages.Clear();
        foreach (var culture in cultures)
        {
            var title = $"{culture.DisplayName} ({culture.Name})";
            _languages.Add(title, culture.Name);

            CultureComboBox.Items.Add(new ComboBoxItem
            {
                Content = title,
            });
        }
    }
}