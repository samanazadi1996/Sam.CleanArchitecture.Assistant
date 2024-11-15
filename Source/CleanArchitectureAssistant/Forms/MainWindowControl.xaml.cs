﻿using System.Windows;
using System.Windows.Controls;

namespace CleanArchitectureAssistant.Forms;

public partial class MainWindowControl : UserControl
{
    public MainWindowControl()
    {
        InitializeComponent();
    }

    private async void ShowAddUseCaseWindow(object sender, RoutedEventArgs e)
    {
        await Forms.AddUseCase.AddUseCaseWindow.ShowAsync();
    }
    private async void ShowAddLanguageWindow(object sender, RoutedEventArgs e)
    {
        await Forms.AddLanguage.AddLanguageWindow.ShowAsync();
    }
}
