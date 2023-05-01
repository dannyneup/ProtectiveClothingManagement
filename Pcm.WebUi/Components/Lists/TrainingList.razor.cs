using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Lists;

public partial class TrainingList : ComponentBase
{ 
    [Parameter, EditorRequired] public List<TrainingResponse> Trainings { get; set; } = Enumerable.Empty<TrainingResponse>().ToList();
    [Parameter] public string SearchString { get; set; } = "";
    [Parameter] public int Elevation { get; set; }
    [Parameter] public EventCallback<TrainingResponse> TrainingRowClicked { get; set; }
    [Parameter] public EventCallback<TrainingResponse> TrainingEditClicked { get; set; }
    [Parameter] public EventCallback<TrainingResponse> TrainingDeleteConfirmed { get; set; }

    [Inject] public IDialogService DialogService { get; set; } = default!;
    [Inject] public ListItemFilterService<TrainingResponse> ListItemFilterService { get; set; } = default!;


    private bool TrainingFilter(TrainingResponse training)
    {
        return ListItemFilterService.CheckIfStringMatchesProperties(training,
            SearchString);
    }

    private async void OnRowClick(TableRowClickEventArgs<TrainingResponse> rowClickEvent)
    {
        await TrainingRowClicked.InvokeAsync(rowClickEvent.Item);
    }

    private async Task OnRowEdit(TrainingResponse training)
    {
        await TrainingEditClicked.InvokeAsync(training);
    }

    private async Task OnRowDelete(TrainingResponse training)
    {
        var dialogOptions = new DialogOptions
        {
            MaxWidth = MaxWidth.Small
        };
        var deletingConfirmed = await DialogService.ShowMessageBox(
                                    Localization.warning,
                                    Localization.reallyWantDeleteCannotBeUndone,
                                    Localization.yes, Localization.no,
                                    options: dialogOptions) 
                                ?? false;
        if (deletingConfirmed) await TrainingDeleteConfirmed.InvokeAsync(training);
    }
}