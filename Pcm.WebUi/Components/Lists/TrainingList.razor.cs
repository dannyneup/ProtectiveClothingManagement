using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Models;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Lists;

public partial class TrainingList : ComponentBase
{
    private Training? _trainingBeforeEdit;

    [Parameter] public List<Training> Trainings { get; set; }
    [Parameter] public EventCallback<List<Training>> TrainingsChanged { get; set; }
    [Parameter] public string? SearchString { get; set; }
    [Parameter] public int Elevation { get; set; } = 0;
    [Parameter] public EventCallback<Training> TrainingRowClicked { get; set; }
    [Parameter] public EventCallback<Training> TrainingEditClicked { get; set; }
    [Parameter] public EventCallback<Training> TrainingDeleteConfirmed { get; set; }

    [Inject] public IDialogService DialogService { get; set; }


    private bool TrainingFilter(Training training)
    {
        return ListItemFilterController<Training>.CheckIfStringMatchesProperties(training,
            SearchString);
    }

    private async void OnRowClick(TableRowClickEventArgs<Training> rowClickEvent)
    {
        await TrainingRowClicked.InvokeAsync(rowClickEvent.Item);
    }

    private async Task OnRowEdit(Training training)
    {
        await TrainingEditClicked.InvokeAsync(training);
    }

    private async Task OnRowDelete(Training training)
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