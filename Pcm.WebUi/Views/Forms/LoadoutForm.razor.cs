using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Models;
using Pcm.WebUi.ViewModels.Forms;

namespace Pcm.WebUi.Views.Forms;

public partial class LoadoutForm
{
    [Parameter]
    public EventCallback<int> LoadoutAddingDone { get; set; }
    
    [Parameter] 
    public bool IsEnabled { get; set; }
    
    [Parameter]
    public int TrainingId { get; set; }
    
    [Inject]
    public LoadoutFormViewModel Vm { get; set; }
    
    [Inject] 
    private ISnackbar Snackbar { get; set; }

    private bool _isVisible;

    protected override void OnParametersSet()
    {
        Vm.LoadoutRequest.TrainingId = TrainingId;
        Vm.RequestFinished = EventCallback.Factory
            .Create<SnackbarModel>(this, async (x) 
                => await ShowSnackbar(x));
    }

    private async Task ChooseOptionOnButtonClick()
    {
        if (Vm.TempLoadoutId == 0)
        {
            _isVisible = true;
            StateHasChanged();
        }
        else
        {
            await Vm.UpdateLoadout();
        }
    }
    
    private void CloseOverlay(LoadOutPartResponse responseModel)
    {
        Vm.UpdateRequestModel(responseModel);
        _isVisible = false;
    }

    private async Task ShowSnackbar(SnackbarModel snackbarModel)
    {
        Snackbar.Add(snackbarModel.Text, snackbarModel.Severity);
    }
}