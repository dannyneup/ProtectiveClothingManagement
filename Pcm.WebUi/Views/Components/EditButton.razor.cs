using Microsoft.AspNetCore.Components;

namespace Pcm.WebUi.Views.Components;

public partial class EditButton
{
    [Parameter] public EventCallback OnEditClicked { get; set; }
    [Parameter] public EventCallback OnDeleteClicked { get; set; }
}