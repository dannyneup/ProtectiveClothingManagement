using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Refactor.ViewModels.Container;

namespace Pcm.WebUi.Pages;

public partial class TrainingMultistepEditor
{
    [Inject]
    public TrainingMultistepEditorViewModel Vm { get; set; }
    
    [Parameter]
    public string TrainingId { get; set; }

    protected override void OnParametersSet()
    {
        Vm.AddedTrainingId = int.Parse(TrainingId);
        base.OnParametersSet();
    }
}