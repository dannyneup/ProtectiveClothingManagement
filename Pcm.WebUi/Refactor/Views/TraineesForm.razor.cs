using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Refactor.ViewModels;

namespace Pcm.WebUi.Refactor.Views;

public partial class TraineesForm
{
    [Parameter]
    public EventCallback<int> TraineeAddingDone { get; set; }
    
    [Parameter] 
    public bool IsEnabled { get; set; }
    
    [Parameter]
    public int TrainingId { get; set; }
    
    [Inject]
    public TraineesFormViewModel Vm { get; set; }

    protected override void OnParametersSet()
    {
        Vm.TraineeRequest.TrainingId = TrainingId;
    }
}