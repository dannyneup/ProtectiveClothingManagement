using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Refactor.ViewModels;

namespace Pcm.WebUi.Refactor.Views;

public partial class TrainingMultistepEditor
{
    [Inject]
    public TrainingMultistepEditorViewModel Vm { get; set; }
}