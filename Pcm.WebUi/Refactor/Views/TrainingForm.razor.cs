using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Refactor.ViewModels;

namespace Pcm.WebUi.Refactor.Views;

public partial class TrainingForm
{
    [Parameter]
    public EventCallback<int> TrainingAdded { get; set; }
    
    [Parameter] 
    public bool IsEnabled { get; set; }
    
    [Inject]
    public TrainingFormViewModel Vm { get; set; }

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            Vm.TrainingAdded = TrainingAdded;
        }
    }
}