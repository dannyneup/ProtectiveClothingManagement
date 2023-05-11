using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.ViewModels.Tables;

namespace Pcm.WebUi.Views.Tables;

public partial class TrainingsTable
{
    [Inject] public TrainingsTableViewModel Vm { get; set; }
    [Parameter] public EventCallback<TrainingResponse> ElementEditSelected { get; set; }
    [Parameter] public EventCallback<TrainingResponse> ElementDeleteSelected { get; set; }
    [Parameter] public bool CanEdit { get; set; } = false;
    [Parameter] public bool Extended { get; set; } = false;
}