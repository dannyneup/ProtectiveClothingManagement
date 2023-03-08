using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.RequestModels;
using Pcm.WebUi.Components.DetailViews;
using Pcm.WebUi.Components.Dialogs.Helpers;
using Pcm.WebUi.Components.Dialogs.ModelEditors;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Pages;

public partial class Trainings
{
    
    [Inject] public IDialogService DialogService { get; set; }
    
    private string? _searchString; 
    [Inject] public ISnackbar Snackbar { get; set; }

    private async Task OpenNewTrainingPopOver()
    {
        var dialog = await DialogService.ShowAsync<TrainingEditorDialog>(string.Format(Localization.createNewT, Localization.training));
        var result = await dialog.Result;
        if (result.Canceled == false)
        {
            var requestModel = (TrainingRequestModel) result.Data;
            if (requestModel != null)
            {
                Snackbar.Add("Training added", Severity.Success);
            }
        }
    }

    private void OpenTrainingDetailsPopOver(int trainingId)
    {
        /*var componentParams = new Dictionary<string, object>
        {
            {"TrainingId", trainingId},
            {"Height", "30vw"}
        };
        var renderFragment = RenderFragmentCreationController.CreateRenderFragmentFromComponent<TrainingDetails>(componentParams);
        var parameters = new DialogParameters {{"Component", renderFragment}};*/
        var parameters = new DialogParameters { ["TrainingId"] = trainingId, ["Height"] = "30vw" };
        DialogService.Show<ComponentDialog>(Localization.trainingDetails, parameters);
    }
}