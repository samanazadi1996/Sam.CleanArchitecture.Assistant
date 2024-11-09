using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CleanArchitectureAssistant;

public class MainWindow : BaseToolWindow<MainWindow>
{
    public override string GetTitle(int toolWindowId) => "Clean Architecture Assistant";

    public override Type PaneType => typeof(Pane);

    public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
    {
        return Task.FromResult<FrameworkElement>(new MainWindowControl());
    }

    [Guid("ddbe5255-d656-422f-8789-ca6fb7ff7e8e")]
    internal class Pane : ToolkitToolWindowPane
    {
        public Pane()
        {
            BitmapImageMoniker = KnownMonikers.ToolWindow;
        }
    }
}
