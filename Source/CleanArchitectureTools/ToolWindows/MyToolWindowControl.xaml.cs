using CleanArchitectureTools.Infrastructure.Enums;
using CleanArchitectureTools.Infrastructure.Services;
using System.Windows;
using System.Windows.Controls;

namespace CleanArchitectureTools
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            await UseCaseService.CreateUseCase("", "name", UseCaseType.Command);

            await VS.MessageBox.ShowAsync("done");
        }

    }


}