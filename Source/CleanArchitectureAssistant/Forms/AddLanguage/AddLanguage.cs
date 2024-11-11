using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CleanArchitectureAssistant.Forms.AddLanguage;

public class AddLanguageWindow : BaseToolWindow<AddLanguageWindow>
{
    public override string GetTitle(int toolWindowId) => "Clean Architecture Assistant";

    public override Type PaneType => typeof(Pane);

    public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
    {
        return Task.FromResult<FrameworkElement>(new AddLanguageWindowControl());
    }

    [Guid("25c38ced-7d46-948e-f18b-b0656fb792f1")]
    internal class Pane : ToolkitToolWindowPane
    {
        public Pane()
        {
            BitmapImageMoniker = KnownMonikers.ToolWindow;
        }
    }
}
