using System.Windows.Controls;

namespace CleanArchitectureTools.AddUseCase
{
    public partial class AddUseCaseWindowControl : UserControl
    {
        public AddUseCaseWindowControl()
        {
            InitializeComponent();
        }

        private async void CloseForm(object sender, System.Windows.RoutedEventArgs e)
        {
            await AddUseCaseWindow.HideAsync();
        }
    }
}