using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using MudBlazor;
using Pcm.Application.Models;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Lists;

public partial class TrainingList : ComponentBase
{ 
    [Parameter, EditorRequired] public List<Training> Trainings { get; set; } = Enumerable.Empty<Training>().ToList();
    [Parameter] public string SearchString { get; set; } = "";
    [Parameter] public int Elevation { get; set; }
    [Parameter] public EventCallback<Training> TrainingRowClicked { get; set; }
    [Parameter] public EventCallback<Training> TrainingEditClicked { get; set; }
    [Parameter] public EventCallback<Training> TrainingDeleteConfirmed { get; set; }

    [Inject] public IDialogService DialogService { get; set; } = default!;


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