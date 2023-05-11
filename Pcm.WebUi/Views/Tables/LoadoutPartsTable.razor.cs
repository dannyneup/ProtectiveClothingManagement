using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.ViewModels.Tables;

namespace Pcm.WebUi.Views.Tables;

public partial class LoadoutPartsTable
{
    [Inject] public LoadOutTableViewModel Vm { get; set; }

    [Parameter] public int TrainingId { get; set; }

    [Parameter] public EventCallback<LoadOutPartResponse> ElementEditSelected { get; set; }

    [Parameter] public bool CanEdit { get; set; }
}