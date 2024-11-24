using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CleanArchitectureAssistant.Forms.AddEntity;

public class AddEntityWindow : BaseToolWindow<AddEntityWindow>
{
    public override string GetTitle(int toolWindowId) => "Clean Architecture Assistant";

    public override Type PaneType => typeof(Pane);

    public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
    {
        return Task.FromResult<FrameworkElement>(new AddEntityWindowControl());
    }

    [Guid("64109dfc-fdef-365e-5bfa-9b040647152e")]
    internal class Pane : ToolkitToolWindowPane
    {
        public Pane()
        {
            BitmapImageMoniker = KnownMonikers.ToolWindow;
        }
    }
}
