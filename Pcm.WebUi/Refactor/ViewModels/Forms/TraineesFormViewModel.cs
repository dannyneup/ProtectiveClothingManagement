using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels.Forms;

public class TraineesFormViewModel : FormViewModel
{
    public TraineeRequest TraineeRequest { get; } = new();

    private readonly PersonsModel _personModel;
    public int TempTraineeId { get; private set; }


    public TraineesFormViewModel(PersonsModel personModel)
    {
        _personModel = personModel;
        _personModel.TraineeRequestFinished = EventCallback.Factory
            .Create<TraineeResponse>(this, async (x) 
                => await UpdateStatus(x));
        StatusText = "Alle Felder sind erforderlich!";
        TraineeRequest.YearStarted = DateTime.Now.Year;
    }

    public async Task AddNewTrainee()
    {
        IsLoading = true;
        StatusColor = Color.Info;
        StatusText = "Auszubildener wird hinzugefügt";
        await _personModel.AddTrainee(TraineeRequest);
    }
    
    public async Task UpdateLoadout()
    {
        IsLoading = true;
        StatusColor = Color.Info;
        StatusText = "Auszubildener wird aktualisiert";
        await _personModel.UpdateTrainee(TraineeRequest, TempTraineeId);
    }

    public void UpdateRequestModel(TraineeResponse trainee)
    {
        StatusColor = Color.Warning;
        TraineeRequest.EmailAddress = trainee.EmailAddress;
        TraineeRequest.FirstName = trainee.EmailAddress;
        TraineeRequest.LastName = trainee.LastName;
        TraineeRequest.PersonnelNumber = trainee.PersonnelNumber;
        TraineeRequest.TrainingId = trainee.TrainingId;
        TempTraineeId = trainee.PersonnelNumber;
        StatusText = "Daten ändern?";
    }

    private async Task UpdateStatus(TraineeResponse response)
    {
        if (response.IsResponseSuccess)
        {
            TraineeRequest.YearStarted = DateTime.Now.Year;
            TraineeRequest.FirstName = "";
            TraineeRequest.LastName = "";
            TraineeRequest.EmailAddress = "";
            TempTraineeId = 0;
            StatusText = $"{response.FirstName} {response.LastName} {(TempTraineeId > 0 ? "geändert" : "hinzugefügt")}.";
            StatusColor = Color.Success;
        }
        else
        {
            StatusText = $"{response.FirstName} {response.LastName} konnte nicht hinzugefügt werden.";
            StatusColor = Color.Error;
        }
        IsLoading = false;
    }
}