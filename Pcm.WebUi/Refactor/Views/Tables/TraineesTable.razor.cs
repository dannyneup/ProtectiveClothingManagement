using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.ViewModels.Tables;

namespace Pcm.WebUi.Refactor.Views.Tables;

public partial class TraineesTable
{
    [Inject]
    public TraineesTableViewModel Vm { get; set; }
    
    [Parameter]
    public int TrainingId { get; set; }
    
    [Parameter]
    public EventCallback<TraineeResponse> ElementSelected { get; set; }
}