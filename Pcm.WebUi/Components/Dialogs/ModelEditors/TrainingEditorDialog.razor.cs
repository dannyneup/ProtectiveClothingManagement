using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Application.Interfaces.RequestModels;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Dialogs.ModelEditors;

public partial class TrainingEditorDialog : ComponentBase
{
    [Parameter] public TrainingResponseModel? TrainingToEdit { get; set; }
    [Inject] public ITrainingRepository TrainingRepository { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
    
    private TrainingResponseModel _trainingBeforeEdit;
    private List<LoadOutPartResponseModel> _loadOut;
    private List<LoadOutPartResponseModel> _loadOutBeforeEdit;
    private bool _editMode;

    protected override async Task OnInitializedAsync()
    {
        if (TrainingToEdit == null)
        {
            _editMode = false;
            return;
        }
        _editMode = true;
    }

    private async Task OnSave()
    {
        bool isSuccess;
        if (_editMode)
        {
            if (TrainingToEdit.Equals(TrainingToEdit))
            {
                return;
            }
            isSuccess = await UpdateTraining();
        }
        else
        {
            isSuccess = await InsertNewTraining();
        }
        ShowSnackbar();
        
        
        async Task<bool> UpdateTraining()
        {
            var trainingResponse = await TrainingRepository.Update(TrainingToEdit) as TrainingResponseModel;
            return trainingResponse.IsResponseSuccess;
        }
        
        async Task<bool> InsertNewTraining()
        {
            var loadOutRequests = _loadOut.ConvertAll(x => x.ToRequestModel());
            var training = TrainingToEdit.ToRequestModel();
            
            var trainingResponse = await TrainingRepository.Insert(training) as TrainingResponseModel;
            var isSuccess = trainingResponse.IsResponseSuccess;
            if (isSuccess)
            {
                var trainingId = trainingResponse.Id;
                var loadOutResponse = 
                    await TrainingRepository.InsertLoadOut(trainingId, loadOutRequests) as List<LoadOutPartResponseModel>;
                isSuccess = loadOutResponse[0].IsResponseSuccess;
            }
            return isSuccess;
        }
        
        void ShowSnackbar()
        {
            var infoMessage = CreateInfoMessage();
            Snackbar.Add(infoMessage, Severity.Success);
        }
        
        string CreateInfoMessage()
        {
            if (!isSuccess)
            {
                if (_editMode)
                {
                    return $"{String.Format(Localization.TWithNameNotsuccessfullyUpdated, Localization.training, TrainingToEdit.Name)}";
                }
                return $"{String.Format(Localization.TWithNameNotsuccessfullyCreated, Localization.training, TrainingToEdit.Name)}";
            }
            if (_editMode)
            {
                return $"{String.Format(Localization.TWithNameSuccessfullyUpdated, Localization.training, TrainingToEdit.Name)}";
            }
            return $"{String.Format(Localization.TWithNameSuccessfullyCreated, Localization.training, TrainingToEdit.Name)}";
        }
    }
}                                                       