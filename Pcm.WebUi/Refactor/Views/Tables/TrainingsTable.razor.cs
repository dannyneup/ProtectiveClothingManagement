using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.ViewModels.Tables;

namespace Pcm.WebUi.Refactor.Views.Tables;

public partial class TrainingsTable
{
    [Inject]
    public TrainingsTableViewModel Vm { get; set; }
    
    [Parameter]
    public EventCallback<TrainingResponse> ElementSelected { get; set; }
}