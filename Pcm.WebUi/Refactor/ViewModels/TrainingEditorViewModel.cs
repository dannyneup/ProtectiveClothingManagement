
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels;

public class TrainingEditorViewModel
{
    public TrainingRequest TrainingRequest { get; } = new();
    public int TrainingResponseId { get; private set; }
    public string StatusText { get; private set; }
    private readonly TrainingModel _trainingModel;

    public TrainingEditorViewModel(TrainingModel trainingModel)
    {
        _trainingModel = trainingModel;
        _trainingModel.InsertRequestFinished = EventCallback.Factory
            .Create<TrainingResponse>(this, async (x) 
                => await NewTrainingAdded(x));
        StatusText = "blablabla";
    }

    public async Task AddNewTraining()
    {
        Console.WriteLine(TrainingRequest.Name);
        Console.WriteLine(TrainingRequest.YearStarted);
        Console.WriteLine(TrainingRequest.Type);
        Console.WriteLine("Done");
        StatusText = "Neues Training wird der Datenbank hinzugefügt";
        await _trainingModel.Add(TrainingRequest);
    }
    
    private async Task NewTrainingAdded(TrainingResponse response)
    {
        if (response.IsResponseSuccess)
        {
            StatusText = $"Neues Training mit ID {response.Id} wurde erfolgreich hinzugefügt.";
            TrainingResponseId = response.Id;
        }
        else
        {
            StatusText = $"Training mit Name {TrainingRequest.Name} konnte nicht hinzugefügt werden. Bitt einen anderen Namen wählen";
        }
    }
    
    
    
    
    
}