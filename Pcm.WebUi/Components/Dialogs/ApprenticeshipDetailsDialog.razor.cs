using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components.Dialogs;

public partial class ApprenticeshipDetailsDialog : ComponentBase
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    [Parameter] public Apprenticeship Apprenticeship { get; set; }
}