using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.WebUi.Components.DetailViews;
using Pcm.WebUi.Components.Dialogs.Helpers;
using Pcm.WebUi.Components.Dialogs.ModelEditors;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Pages;

public partial class Trainings
{
    //[Inject]
    //public IRepository<ITrainingInfoResponseModel, ITrainingInfoRequestModel> ApprenticeshipRepository { get; set; }
    [Inject] public IDialogService DialogService { get; set; }
    
    private string? _searchString;
    //private IEnumerable<TrainingInfoResponseModel>? _trainingInfoResponses = new List<TrainingInfoResponseModel>();

    private void OpenNewTrainingPopOver()
    {
        DialogService.Show<TrainingEditorDialog>(String.Format(Localization.createNewT, Localization.training));
    }

    private void OpenTrainingDetailsPopOver(int trainingId)
    {
        var componentParams = new Dictionary<string, object>
        {
            {"TrainingId", trainingId},
            {"Height", "30vw"}
        };
        var renderFragment =
            RenderFragmentCreationController.CreateRenderFragmentFromComponent<TrainingDetails>(componentParams);

        var parameters = new DialogParameters {{"Component", renderFragment}};

        DialogService.Show<ComponentDialog>(Localization.trainingDetails, parameters);
    }
}