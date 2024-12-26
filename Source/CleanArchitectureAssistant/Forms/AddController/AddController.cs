using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CleanArchitectureAssistant.Forms.AddController;

public class AddControllerWindow : BaseToolWindow<AddControllerWindow>
{
    public override string GetTitle(int toolWindowId) => "Clean Architecture Assistant";

    public override Type PaneType => typeof(Pane);

    public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
    {
        return Task.FromResult<FrameworkElement>(new AddControllerWindowControl());
    }

    [Guid("8b6915e3-4d07-b0b0-3a19-778d039864fc")]
    internal class Pane : ToolkitToolWindowPane
    {
        public Pane()
        {
            BitmapImageMoniker = KnownMonikers.ToolWindow;
        }
    }
}
