using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.ViewModels.Forms;

namespace Pcm.WebUi.Refactor.Views.Forms;

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
    
    private bool _isVisible;

    protected override void OnParametersSet()
    {
        Vm.TraineeRequest.TrainingId = TrainingId;
    }
    
    private async Task ChooseOptionOnButtonClick()
    {
        if (Vm.TempTraineeId == 0)
        {
            _isVisible = true;
            StateHasChanged();
        }
        else
        {
            await Vm.UpdateLoadout();
        }
    }
    
    private void CloseOverlay(TraineeResponse responseModel)
    {
        Vm.UpdateRequestModel(responseModel);
        _isVisible = false;
    }
}