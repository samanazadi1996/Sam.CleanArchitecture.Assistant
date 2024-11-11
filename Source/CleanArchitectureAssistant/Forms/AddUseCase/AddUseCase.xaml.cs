using CleanArchitectureAssistant.Infrastructure.Services;
using System.Windows.Controls;
using CleanArchitectureAssistant.Infrastructure.Enums;
using System.Windows;

namespace CleanArchitectureAssistant.Forms.AddUseCase;

public partial class AddUseCaseWindowControl : UserControl
{
    public AddUseCaseWindowControl()
    {
        InitializeComponent();
        LoadFeatures();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddUseCaseWindow.HideAsync();
    }

    private async Task LoadFeatures()
    {
        var data = await ApplicationService.GetFeatures();

        FeatureNameComboBox.Items.Clear();
        foreach (var item in data)
        {
            FeatureNameComboBox.Items.Add(new ComboBoxItem
            {
                Content = item
            });
        }
    }
    private async void Execute(object sender, RoutedEventArgs e)
    {

        var useCaseTypeText = UseCaseTypeComboBox.Text;
        if (string.IsNullOrWhiteSpace(useCaseTypeText))
        {
            await VS.MessageBox.ShowAsync("Please select a use case type.");
            return;
        }

        if (!Enum.TryParse<UseCaseType>(useCaseTypeText.Replace(" ", ""), out var useCaseType))
        {
            await VS.MessageBox.ShowAsync("Invalid use case type.");
            return;
        }
        var returnType = ReturnTypeComboBox.Text;
        if (string.IsNullOrWhiteSpace(returnType))
        {
            await VS.MessageBox.ShowAsync("Please select a return type.");
            return;
        }

        if (useCaseType == UseCaseType.QueryPagedList && returnType == "void")
        {
            await VS.MessageBox.ShowAsync("A method of this type cannot have a return type of 'void'. Please select a valid return type.");
            return;
        }

        var featureName = FeatureNameComboBox.Text;
        if (string.IsNullOrWhiteSpace(featureName))
        {
            await VS.MessageBox.ShowAsync("Please enter a feature name.");
            return;
        }

        var useCaseName = UseCaseNameTextBox.Text;
        if (string.IsNullOrWhiteSpace(useCaseName))
        {
            await VS.MessageBox.ShowAsync("Please enter a use case name.");
            return;
        }


        if (await ApplicationService.CreateUseCase(featureName, useCaseName, useCaseType, returnType))
        {
            await VS.MessageBox.ShowAsync("Use case created successfully.");
            UseCaseNameTextBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to create use case. Please try again.");
        }
    }
}
