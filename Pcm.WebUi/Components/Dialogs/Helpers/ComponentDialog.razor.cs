using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Pcm.WebUi.Components.Dialogs.Helpers;

public partial class ComponentDialog
{
    [Parameter] public RenderFragment Component { get; set; }
}