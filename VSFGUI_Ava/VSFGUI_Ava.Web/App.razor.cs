using Avalonia.Web.Blazor;

namespace VSFGUI_Ava.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<VSFGUI_Ava.App>()
            .SetupWithSingleViewLifetime();
    }
}