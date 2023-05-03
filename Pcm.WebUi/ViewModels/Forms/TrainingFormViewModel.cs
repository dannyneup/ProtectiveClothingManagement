using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.Models;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.ViewModels.Forms;

public class TrainingFormViewModel : FormViewModel
{
    public TrainingRequest TrainingRequest { get; } = new();
    public EventCallback<int> TrainingAdded { get; set; }
    public int TempTrainingId;
    
    private readonly TrainingsModel _trainingsModel;

    public TrainingFormViewModel(TrainingsModel trainingsModel)
    {
        _trainingsModel = trainingsModel;
        _trainingsModel.TrainingInsertRequestFinished = EventCallback.Factory
            .Create<TrainingResponse>(this, async (x) 
                => await UpdateStatus(x));
        StatusText = "Alle Felder sind erforderlich!";
        TrainingRequest.YearStarted = DateTime.Now.Year;
        TrainingRequest.Type = "ausbildung";
    }

    public async Task AddNewTraining()
    {
        if (TempTrainingId > 0)
        {
            IsLoading = false;
            await TrainingAdded.InvokeAsync(TempTrainingId);
            TempTrainingId = 0;
            return;
        }
        
        IsLoading = true;
        StatusColor = Color.Info;
        StatusText = "Neues Training wird hinzugefügt";
        await _trainingsModel.AddTraining(TrainingRequest);
    }

    public void InitFromRoutingParameter(int id)
    {
        var response = _trainingsModel.Trainings.First(x => x.Id == id);
        UpdateRequestModel(response);
    }

    public void UpdateRequestModel(TrainingResponse response)
    {
        TrainingRequest.Title = response.Title;
        TrainingRequest.YearStarted = response.YearStarted;
        TrainingRequest.Type = response.Type;
        TempTrainingId = response.Id;
    }

    private async Task UpdateStatus(TrainingResponse response)
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