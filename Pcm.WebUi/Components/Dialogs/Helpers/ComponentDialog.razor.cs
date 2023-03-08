using Microsoft.AspNetCore.Components;

namespace Pcm.WebUi.Components.Dialogs.Helpers;

public partial class ComponentDialog
{
    [Parameter] public RenderFragment Component { get; set; }
}