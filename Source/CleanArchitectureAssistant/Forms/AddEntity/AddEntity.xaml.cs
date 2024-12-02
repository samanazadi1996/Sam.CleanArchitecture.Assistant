using CleanArchitectureAssistant.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CleanArchitectureAssistant.Forms.AddEntity;

public partial class AddEntityWindowControl : UserControl
{
    public AddEntityWindowControl()
    {
        InitializeComponent();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddEntityWindow.HideAsync();
    }
    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        List<string> ignore = ["bin", "obj", "Common"];
        var dirs = await DomainService.GetDonmains();

        DomainNameComboBox.Items.Clear();
        foreach (var item in dirs.Where(s => !ignore.Contains(s)))
        {
            DomainNameComboBox.Items.Add(new ComboBoxItem
            {
                Content = item
            });

        }

    }

    private async void Execute(object sender, RoutedEventArgs e)
    {
        var domainName = DomainNameComboBox.Text;
        if (string.IsNullOrWhiteSpace(domainName))
        {
            await VS.MessageBox.ShowAsync("Please provide a domain name.");
            return;
        }
        var entityName = EntityNameTextBox.Text;
        if (string.IsNullOrWhiteSpace(entityName))
        {
            await VS.MessageBox.ShowAsync("Please provide an entity name.");
            return;
        }

        if (await DomainService.CreateEntity(domainName, entityName, BaseTypeComboBox.Text, KeyPropertyTypeComboBox.Text))
        {
            await VS.MessageBox.ShowAsync("The entity was successfully created.");
            EntityNameTextBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to create the entity. Please try again.");
        }
    }
}