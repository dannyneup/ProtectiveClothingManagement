
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels;

public class TraineesFormViewModel : FormViewModel
{
    public TraineeRequest TraineeRequest { get; } = new();
    public int CountAdded { get; private set; }

    private readonly PersonsModel _personModel;

    public TraineesFormViewModel(PersonsModel personModel)
    {
        _personModel = personModel;
        _personModel.TraineeInserRequestFinished = EventCallback.Factory
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

    private async Task UpdateStatus(TraineeResponse response)
    {
        if (response.IsResponseSuccess)
        {
            CountAdded++;
            TraineeRequest.YearStarted = DateTime.Now.Year;
            TraineeRequest.FirstName = "";
            TraineeRequest.LastName = "";
            TraineeRequest.EmailAddress = "";
            StatusText = $"{response.FirstName} {response.LastName} mit ID {response.PersonnelNumber} hinzugefügt.";
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