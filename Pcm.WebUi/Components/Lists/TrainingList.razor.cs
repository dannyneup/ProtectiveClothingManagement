using System.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Application.Interfaces.RequestModels;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Components.Dialogs.ModelEditors;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Lists;

public partial class TrainingList : ComponentBase
{
    private TrainingInfoResponseModel _trainingInfoBevoreEdit;
    private IEnumerable<TrainingInfoResponseModel> _trainings;
    [Parameter] public string? SearchString { get; set; }
    [Parameter] public int Elevation { get; set; } = 0;
    [Parameter] public EventCallback<int> RowClicked { get; set; }

    [Inject] public IRepository<ITrainingInfoResponseModel, ITrainingInfoRequestModel> TrainingInfoRepository { get; set; }

    [Inject] public ITrainingRepository TrainingRepository { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
    [Inject] public IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _trainings = await TrainingInfoRepository.GetAll() as IEnumerable<TrainingInfoResponseModel>;
    }

    private bool TrainingFilter(ITrainingInfoResponseModel trainingResponse)
    {
        return ListItemFilterController<ITrainingInfoResponseModel>.CheckIfStringMatchesProperties(trainingResponse,
            SearchString);
    }

    private async void OnRowClick(TableRowClickEventArgs<TrainingInfoResponseModel> rowClickEvent)
    {
        await RowClicked.InvokeAsync(rowClickEvent.Item.Id);
    }

    private async Task OnRowEdit(TrainingInfoResponseModel trainingInfo)
    {
        var training = trainingInfo.ToTrainingResponseModel();
        var dialogParams = new DialogParameters
        {
            {"TrainingToEdit", training}
        };
        await DialogService.ShowAsync<TrainingEditorDialog>(string.Format(Localization.editT, Localization.training), dialogParams);
    }

    private async Task OnRowDelete(TrainingInfoResponseModel rowObj)
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