using CleanArchitectureAssistant.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Forms.AddEndpoint;

public partial class AddEndpointWindowControl : UserControl
{
    public AddEndpointWindowControl()
    {
        InitializeComponent();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddEndpointWindow.HideAsync();
    }
    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var entities = await DomainService.GetEntities();

        EndpointNameComboBox.Items.Clear();

        foreach (EntityDto entity in entities)
        {
            EndpointNameComboBox.Items.Add(new ComboBoxItem
            {
                Content = entity.ClassName + "Endpoint"
            });
        }
    }

    private async void Execute(object sender, RoutedEventArgs e)
    {
        var entityName = EndpointNameComboBox.Text;
        if (string.IsNullOrWhiteSpace(entityName))
        {
            await VS.MessageBox.ShowAsync("Please provide an Endpoint name.");
            return;
        }

        if (await EndpointService.CreateEndpoint(NormalizeEndpointName(EndpointNameComboBox.Text)))
        {
            await VS.MessageBox.ShowAsync("The Endpoint was successfully created.");
            EndpointNameComboBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to create the Endpoint. Please try again.");
        }
    }
    private string GetNumber(string str)
    {
        return string.Concat(str.Where(p => "123456890".Contains(p)));
    }
    private string NormalizeEndpointName(string EndpointName)
    {
        return EndpointName.Replace("Endpoint", "").Replace("Endpoint", "") + "Endpoint";
    }
}