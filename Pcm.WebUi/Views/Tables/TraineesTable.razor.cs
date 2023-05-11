using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.ViewModels.Tables;

namespace Pcm.WebUi.Views.Tables;

public partial class TraineesTable
{
    [Inject] public TraineesTableViewModel Vm { get; set; }

    [Parameter]
    public int TrainingId
    {
        set => Vm.TrainingId = value;
    }

    [Parameter] public EventCallback<TraineeResponse> ElementEditSelected { get; set; }
    [Parameter] public EventCallback<TraineeResponse> ElementDeleteSelected { get; set; }
    [Parameter] public bool CanEdit { get; set; } = false;
    [Parameter] public bool Extended { get; set; } = false;
}