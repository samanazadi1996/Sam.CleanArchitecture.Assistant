using CleanArchitectureAssistant.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Forms.AddController;

public partial class AddControllerWindowControl : UserControl
{
    public AddControllerWindowControl()
    {
        InitializeComponent();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddControllerWindow.HideAsync();
    }
    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var entities = await DomainService.GetEntities();
        var versions = await EndpointService.GetControllerVersions();

        ControllerVersionComboBox.Items.Clear();
        ControllerNameComboBox.Items.Clear();

        foreach (EntityDto entity in entities)
        {
            ControllerNameComboBox.Items.Add(new ComboBoxItem
            {
                Content = entity.ClassName + "Controller"
            });
        }
        foreach (var item in versions)
        {
            ControllerVersionComboBox.Items.Add(new ComboBoxItem
            {
                Content = "Version " + GetNumber(item)
            });
        }

        if (ControllerVersionComboBox.Items.Count > 0)
        {
            ControllerVersionComboBox.SelectedIndex = ControllerVersionComboBox.Items.Count - 1;
        }
    }

    private async void Execute(object sender, RoutedEventArgs e)
    {
        //var entityName = EntityNameComboBox.Text;
        //if (string.IsNullOrWhiteSpace(entityName))
        //{
        //    await VS.MessageBox.ShowAsync("Please provide an entity name.");
        //    return;
        //}

        //if (await RepositoryService.AddController(entities.FirstOrDefault(p => p.ClassName == entityName)))
        //{
        //    await VS.MessageBox.ShowAsync("The entity was successfully created.");
        //    EntityNameComboBox.Text = string.Empty;
        //}
        //else
        //{
        //    await VS.MessageBox.ShowAsync("Failed to create the entity. Please try again.");
        //}
    }
    private string GetNumber(string str)
    {
        return string.Concat(str.Where(p => "123456890".Contains(p)));
    }
}