using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Dialogs.ModelEditors;

public partial class TrainingEditorDialog : ComponentBase
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    private TrainingRequestModel Training { get; set; } = new();
    //private TrainingRequestModel TrainingToEdit2 { get; set; }
    //[Inject] public IRepository<TrainingResponse, TrainingRequestModel> TrainingRepository { get; set; }
    //[Inject] public IRepository<LoadOutPartResponse, LoadOutPartRequest> LoadoutRepository { get; set; }
    bool success;
    private IEnumerable<ItemCategoryResponse> _itemCategories = new List<ItemCategoryResponse>();
    private string _searchString = "";

    
    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void Submit()
    {
        MudDialog.Close(DialogResult.Ok(Training));
    }
    
    private int getItemCount(ItemCategoryResponse category)
    {
        return 3;
    }
    
    

    private bool ItemCategoryFilter(ItemCategoryResponse arg)
    {
        return ListItemFilterController<ItemCategoryResponse>.CheckIfStringMatchesProperties(arg,
            _searchString);
    }
    
    //private TrainingResponse _trainingBeforeEdit;
    //private List<LoadOutPartResponse> _loadOut;
    //private List<LoadOutPartResponse> _loadOutBeforeEdit;
    //private bool _editMode;

    /*protected override async Task OnInitializedAsync()
    {
        if (TrainingToEdit == null)
        {
            _editMode = false;
            return;
        }
        _editMode = true;
    }*/

    /*private async Task OnSave()
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
            var trainingResponse = await TrainingRepository.Update(TrainingToEdit);
            return trainingResponse.IsResponseSuccess;
        }
        
        async Task<bool> InsertNewTraining()
        {
            //var loadOutRequests = _loadOut.ConvertAll(x => x.ToRequestModel());
            //var training = TrainingToEdit.ToRequestModel();
            
            var trainingResponse = await TrainingRepository.Insert(TrainingToEdit2);
            var isSuccess = trainingResponse.IsResponseSuccess;
            if (isSuccess)
            {
                var trainingId = trainingResponse.Id;
                foreach (var loadOut in _loadOut)
                {
                    LoadOutPartRequest request = new()
                    {
                        ItemCategoryId = loadOut.CategoryId,
                        Count = loadOut.Count,
                        TrainingId = trainingId
                    };
                    var loadOutResponse = await LoadoutRepository.Insert(request);
                    
                }
                //isSuccess = loadOutResponse[0].IsResponseSuccess;
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
    }*/
}                                                       