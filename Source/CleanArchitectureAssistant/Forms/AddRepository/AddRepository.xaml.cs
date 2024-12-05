using CleanArchitectureAssistant.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CleanArchitectureAssistant.Infrastructure.DTOs;

namespace CleanArchitectureAssistant.Forms.AddRepository;

public partial class AddRepositoryWindowControl : UserControl
{
    List<EntityDto> entities;
    public AddRepositoryWindowControl()
    {
        InitializeComponent();
    }

    private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
    {
        await AddRepositoryWindow.HideAsync();
    }
    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        entities = await DomainService.GetEntities();
        EntityNameComboBox.Items.Clear();

        foreach (EntityDto entity in entities)
        {
            EntityNameComboBox.Items.Add(new ComboBoxItem
            {
                Content = entity.ClassName
            });
        }
    }

    private async void Execute(object sender, RoutedEventArgs e)
    {
        var entityName = EntityNameComboBox.Text;
        if (string.IsNullOrWhiteSpace(entityName))
        {
            await VS.MessageBox.ShowAsync("Please provide an entity name.");
            return;
        }

        if (await RepositoryService.AddRepository(entities.FirstOrDefault(p => p.ClassName == entityName)))
        {
            await VS.MessageBox.ShowAsync("The entity was successfully created.");
            EntityNameComboBox.Text = string.Empty;
        }
        else
        {
            await VS.MessageBox.ShowAsync("Failed to create the entity. Please try again.");
        }
    }
}