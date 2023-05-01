using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.ViewModels.Forms;

namespace Pcm.WebUi.Refactor.Views.Forms;

public partial class TrainingForm
{
    [Parameter]
    public EventCallback<int> TrainingAdded { get; set; }
    
    [Parameter] 
    public bool IsEnabled { get; set; }
    
    [Parameter] 
    public int TrainingId { get; set; }
    
    [Inject]
    public TrainingFormViewModel Vm { get; set; }

    private bool _isVisible;

    protected override void OnParametersSet()
    {
        if (TrainingId > 0)
        {
            Vm.InitFromRoutingParameter(TrainingId);
        }
        base.OnParametersSet();
    }

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            Vm.TrainingAdded = TrainingAdded;
        }
    }

    private void CloseOverlay(TrainingResponse responseModel)
    {
        Vm.UpdateRequestModel(responseModel);
        _isVisible = false;
    }
    
    private async Task ChooseOptionOnButtonClick()
    {
        if (Vm.TempTrainingId == 0)
        {
            _isVisible = true;
            StateHasChanged();
        }
        else
        {
            //await Vm.UpdateLoadout();
        }
    }
}