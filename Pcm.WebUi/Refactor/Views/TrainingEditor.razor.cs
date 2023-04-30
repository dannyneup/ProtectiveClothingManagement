using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Refactor.ViewModels;

namespace Pcm.WebUi.Refactor.Views;

public partial class TrainingEditor
{
    [Inject]
    public TrainingEditorViewModel Vm { get; set; }
}