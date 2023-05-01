using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;
using Pcm.WebUi.Refactor.ViewModels;

namespace Pcm.WebUi.Refactor.Views;

public partial class LoadoutPartsTable
{
    [Inject]
    public LoadOutTableViewModel Vm { get; set; }
    
    [Parameter]
    public int TrainingId { get; set; }
    
    [Parameter]
    public EventCallback<LoadOutPartResponse> ElementSelected { get; set; }
}