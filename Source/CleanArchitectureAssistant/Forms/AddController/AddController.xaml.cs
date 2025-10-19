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
        var versions = await ControllerService.GetControllerVersions();

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
        var entityName = ControllerNameComboBox.Text;
        if (string.IsNullOrWhiteSpace(entityName))
        {
            await VS.MessageBox.ShowAsync("Please provide an controller name.");
            return;
        }

        if (await ControllerService.CreateController(NormalizeControllerName(ControllerNameComboBox.Text), ControllerVersionComboBox.Text))
        {
            await VS.MessageBox.ShowAsync("The controller was successfully created.");
            ControllerNameComboBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to create the controller. Please try again.");
        }
    }
    private string GetNumber(string str)
    {
        return string.Concat(str.Where(p => "123456890".Contains(p)));
    }
    private string NormalizeControllerName(string controllerName)
    {
        return controllerName.Replace("Controller", "").Replace("controller", "") + "Controller";
    }
}