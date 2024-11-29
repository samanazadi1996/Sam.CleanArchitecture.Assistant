using CleanArchitectureAssistant.Infrastructure.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CleanArchitectureAssistant.Forms.AddMigration;

public partial class AddMigrationWindowControl : UserControl
{
    public AddMigrationWindowControl()
    {
        InitializeComponent();
        LoadData();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddMigrationWindow.HideAsync();
    }

    private async void Execute(object sender, RoutedEventArgs e)
    {
        var libraryName = LibraryNameComboBox.Text;
        if (string.IsNullOrWhiteSpace(libraryName))
        {
            await VS.MessageBox.ShowAsync("Please provide a Layer name.");
            return;
        }

        var migrationName = MigrationNameTextBox.Text;
        if (string.IsNullOrWhiteSpace(migrationName))
        {
            await VS.MessageBox.ShowAsync("Please provide an Migration name.");
            return;
        }

        var startupProject = StartupProjectComboBox.Text;
        if (string.IsNullOrWhiteSpace(startupProject))
        {
            await VS.MessageBox.ShowAsync("Please provide an startup project.");
            return;
        }

        if (await EfService.AddMigration(libraryName, migrationName, startupProject))
        {
            await VS.MessageBox.ShowAsync("The Migration was successfully created.");
            MigrationNameTextBox.Text = string.Empty;
            LibraryNameComboBox.Text = string.Empty;
            StartupProjectComboBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to add Migration. Please try again.");
        }
    }

    async Task LoadData()
    {

        LibraryNameComboBox.Items.Clear();
        StartupProjectComboBox.Items.Clear();

        foreach (var item in await CommonService.GetProjectsPath())
        {
            var text = item.Substring(item.LastIndexOf("\\", StringComparison.Ordinal) + 1);

            LibraryNameComboBox.Items.Add(new ComboBoxItem
            {
                Content = text
            });
            StartupProjectComboBox.Items.Add(new ComboBoxItem
            {
                Content = text
            });
        }
    }

    private async void Refresh_OnClick(object sender, RoutedEventArgs e)
    {
        await LoadData();
    }
}