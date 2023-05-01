using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels.Forms;

public class TrainingFormViewModel : FormViewModel
{
    public TrainingRequest TrainingRequest { get; } = new();
    public EventCallback<int> TrainingAdded { get; set; }
    public int TempTrainingId;
    
    private readonly TrainingModel _trainingModel;

    public TrainingFormViewModel(TrainingModel trainingModel)
    {
        _trainingModel = trainingModel;
        _trainingModel.TrainingInsertRequestFinished = EventCallback.Factory
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
        await _trainingModel.AddTraining(TrainingRequest);
    }

    public void InitFromRoutingParameter(int id)
    {
        var response = _trainingModel.Trainings.First(x => x.Id == id);
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