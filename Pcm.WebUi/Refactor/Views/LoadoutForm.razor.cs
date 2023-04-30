using Microsoft.AspNetCore.Components;
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

    protected override void OnParametersSet()
    {
        Vm.LoadoutRequest.TrainingId = TrainingId;
    }
}