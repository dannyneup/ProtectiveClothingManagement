using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.ViewModels;

namespace Pcm.WebUi.Refactor.Views;

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

    private bool _isVisible;
    
    protected override void OnParametersSet()
    {
        Vm.LoadoutRequest.TrainingId = TrainingId;
    }

    private async Task ChooseOptionOnButtonClick()
    {
        if (Vm.TempLoadoutId == 0)
        {
            OpenOverlay();
        }
        else
        {
            await Vm.UpdateLoadout();
        }
    }
    

    private void OpenOverlay()
    {
        _isVisible = true;
        StateHasChanged();
    }
    
}