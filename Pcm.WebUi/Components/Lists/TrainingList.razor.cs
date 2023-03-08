using System.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Components.Dialogs.ModelEditors;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Lists;

public partial class TrainingList : ComponentBase
{
    private TrainingResponse _trainingBevoreEdit;
    private IEnumerable<TrainingResponse> _trainings;
    [Parameter] public string? SearchString { get; set; }
    [Parameter] public int Elevation { get; set; } = 0;
    [Parameter] public EventCallback<int> RowClicked { get; set; }

    [Inject] public IRepository<TrainingResponse, TrainingRequestModel> TrainingRepository { get; set; }

    //[Inject] public ITrainingRepository TrainingRepository { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
    [Inject] public IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _trainings = await TrainingRepository.GetAll();
    }

    private bool TrainingFilter(TrainingResponse trainingResponse)
    {
        return ListItemFilterController<TrainingResponse>.CheckIfStringMatchesProperties(trainingResponse,
            SearchString);
    }

    private async void OnRowClick(TableRowClickEventArgs<TrainingResponse> rowClickEvent)
    {
        await RowClicked.InvokeAsync(rowClickEvent.Item.Id);
    }

    private async Task OnRowEdit(TrainingResponse training)
    {
        //var training = trainingInfo.ToTrainingResponseModel();
        var dialogParams = new DialogParameters
        {
            {"TrainingToEdit", training}
        };
        await DialogService.ShowAsync<TrainingEditorDialog>(string.Format(Localization.editT, Localization.training), dialogParams);
    }

    private async Task OnRowDelete(TrainingResponse rowObj)
    {
        var deletingConfirmed = await DialogService.ShowMessageBox(
            @Localization.warning,
            @Localization.reallyWantDeleteCannotBeUndone,
            yesText: @Localization.yes, noText: @Localization.no)
            ?? false;
        if (deletingConfirmed)
        {
            var deleted = await TrainingRepository.Delete(rowObj.Id);
        }
    }
}