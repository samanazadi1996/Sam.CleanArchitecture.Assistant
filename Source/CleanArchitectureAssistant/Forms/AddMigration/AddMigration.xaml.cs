using CleanArchitectureAssistant.Infrastructure.Services;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;

namespace CleanArchitectureAssistant.Forms.AddMigration;

public partial class AddMigrationWindowControl : UserControl
{
    public AddMigrationWindowControl()
    {
        InitializeComponent();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddMigrationWindow.HideAsync();
    }
    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadData();
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
        var contextName = ContextNameComboBox.Text;
        if (string.IsNullOrWhiteSpace(contextName))
        {
            await VS.MessageBox.ShowAsync("Please provide an context name.");
            return;
        }

        if (await EfService.AddMigration(libraryName, startupProject, migrationName, contextName))
        {
            await VS.MessageBox.ShowAsync("The Migration was successfully created.");
            await LoadData();
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to add Migration. Please try again.");
        }
    }

    async Task LoadData()
    {
        MigrationNameTextBox.Text = string.Empty;
        LibraryNameComboBox.Text = string.Empty;
        StartupProjectComboBox.Text = string.Empty;
        ContextNameComboBox.Text = string.Empty;

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

    private async void LibraryNameComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        try
        {

            var selectedItem = LibraryNameComboBox.SelectedItem as ComboBoxItem;

            if (selectedItem == null)
                return;

            var selectedText = selectedItem.Content.ToString();

            var path = (await CommonService.GetProjectsPath()).FirstOrDefault(p => p.Contains(selectedText));

            ContextNameComboBox.Items.Clear();

            if (path != null)
            {
                var filePaths = System.IO.Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

                foreach (var filePath in filePaths)
                {
                    var fileContent = File.ReadAllText(filePath);

                    // Parse the file content using Roslyn
                    var syntaxTree = CSharpSyntaxTree.ParseText(fileContent);
                    var root = await syntaxTree.GetRootAsync();

                    // Find all class declarations in the file
                    var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

                    foreach (var classDeclaration in classDeclarations)
                    {
                        // Check if the class inherits from DbContext
                        var baseTypes = classDeclaration.BaseList?.Types;
                        if (baseTypes != null && baseTypes.Value.Any(bt => bt.ToString().Contains("DbContext")))
                        {
                            var className = classDeclaration.Identifier.Text;

                            ContextNameComboBox.Items.Add(new ComboBoxItem
                            {
                                Content = className
                            });
                        }
                    }
                }
            }


            if (ContextNameComboBox.Items.Count>0)
            {
                ContextNameComboBox.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            await VS.MessageBox.ShowAsync($"An error occurred: {ex.Message}");
        }
    }


}