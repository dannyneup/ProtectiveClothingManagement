using MudBlazor;

namespace Pcm.WebUi.Refactor.ViewModels.Forms;

public class FormViewModel
{
    public bool IsLoading { get; protected set; }
    public string StatusText { get; protected set; }
    public Color StatusColor { get; protected set; } = Color.Info;
}