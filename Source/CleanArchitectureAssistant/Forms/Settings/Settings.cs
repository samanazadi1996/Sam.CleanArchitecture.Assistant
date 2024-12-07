using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CleanArchitectureAssistant.Forms.Settings;

public class SettingsWindow : BaseToolWindow<SettingsWindow>
{
    public override string GetTitle(int toolWindowId) => "Clean Architecture Assistant";

    public override Type PaneType => typeof(Pane);

    public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
    {
        return Task.FromResult<FrameworkElement>(new SettingsWindowControl());
    }

    [Guid("573a5088-703b-7293-fe04-da06f6e4f6e7")]
    internal class Pane : ToolkitToolWindowPane
    {
        public Pane()
        {
            BitmapImageMoniker = KnownMonikers.ToolWindow;
        }
    }
}
