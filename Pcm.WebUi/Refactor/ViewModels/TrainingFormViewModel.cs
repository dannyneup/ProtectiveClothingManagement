
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels;

public class TrainingFormViewModel
{
    public TrainingRequest TrainingRequest { get; } = new();
    public EventCallback<int> TrainingAdded { get; set; }
    public string StatusText { get; private set; }
    public bool IsLoading { get; private set; }
    public Color StatusColor { get; private set; } = Color.Info;
    private readonly DataModel _dataModel;

    public TrainingFormViewModel(DataModel dataModel)
    {
        _dataModel = dataModel;
        _dataModel.TrainingInsertRequestFinished = EventCallback.Factory
            .Create<TrainingResponse>(this, async (x) 
                => await NewTrainingAdded(x));
        StatusText = "Alle Felder sind erforderlich!";
        TrainingRequest.YearStarted = DateTime.Now.Year;
    }

    public async Task AddNewTraining()
    {
        IsLoading = true;
        StatusColor = Color.Info;
        StatusText = "Neues Training wird der Datenbank hinzugefügt";
        await _dataModel.AddTraining(TrainingRequest);
    }

    private async Task NewTrainingAdded(TrainingResponse response)
    {
        if (response.IsResponseSuccess)
        {
            StatusText = $"Training mit ID {response.Id} hinzugefügt.";
            StatusColor = Color.Success;
        }
        else
        {
            StatusText = $"Training {TrainingRequest.Title} konnte nicht hinzugefügt werden.";
            StatusColor = Color.Error;
        }
        IsLoading = false;
        await TrainingAdded.InvokeAsync(response.Id);
    }
}