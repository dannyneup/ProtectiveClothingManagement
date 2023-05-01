using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.ViewModels.Tables;

namespace Pcm.WebUi.Refactor.Views.Tables;

public partial class LoadoutPartsTable
{
    [Inject]
    public LoadOutTableViewModel Vm { get; set; }
    
    [Parameter]
    public int TrainingId { get; set; }
    
    [Parameter]
    public EventCallback<LoadOutPartResponse> ElementSelected { get; set; }
}