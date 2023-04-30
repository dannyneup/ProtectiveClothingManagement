using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Models;
using Pcm.Infrastructure.Repositories;
using Pcm.WebUi.Components.Dialogs;
using Pcm.WebUi.CustomEventArgs;
using Pcm.WebUi.Refactor.Views;
using Pcm.WebUi.Resources;
using Pcm.WebUi.ViewModels;

namespace Pcm.WebUi.Pages;

public partial class Trainings
{
    [Inject] public TrainingsViewModel ViewModel { get; set; } = default!;
    [Inject] public IDialogService DialogService { get; set; } = default!;

    private string _searchString = "";
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ViewModel.Init();
            ViewModel.OpenCreateNewTraining += ShowCreateNewTrainingDialog;
            ViewModel.OpenTrainingDetails += ShowTrainingDetailsDialog;
            ViewModel.OpenEditTraining += ShowEditTrainingDialog;
            StateHasChanged();
        }
    }

    private async void ShowCreateNewTrainingDialog(object? sender, CreateNewTrainingEventArgs createNewTrainingEventArgs)
    {
        var dialogParams = new DialogParameters
        {
            {"Trainings", createNewTrainingEventArgs.Trainings},
            {"ItemCategories", createNewTrainingEventArgs.ItemCategories},
            {"EditMode", false}
        };
        /*var dialog = await DialogService.ShowAsync<TrainingEditorDialogRef>(
            string.Format(Localization.editT, Localization.training),
            dialogParams);
        var result = await dialog.Result;
        var newTrainingCreated = !result.Canceled &&
                                 result.Data is Training;
        
        if (!newTrainingCreated) return;
        
        var newTraining = result.Data as Training ?? new Training();
        await ViewModel.SaveNewTraining(newTraining);*/
    }

    private async void ShowTrainingDetailsDialog(object? sender, OpenTrainingDetailsEventArgs openTrainingDetailsEventArgs)
    {
        var parameters = new DialogParameters
        {
            {"Trainees", openTrainingDetailsEventArgs.Trainees},
            {"LoadOut", openTrainingDetailsEventArgs.LoadOut}
        };
        await DialogService.ShowAsync<TrainingDetailsDialog>(Localization.trainingDetails, parameters);
    }
    
    private async void ShowEditTrainingDialog(object? sender, EditTrainingEventArgs editTrainingEventArgs)
    {
        var trainingBeforeEdit = editTrainingEventArgs.Training;
        var dialogParams = new DialogParameters
        {
            {"Training", trainingBeforeEdit},
            {"Trainings", editTrainingEventArgs.Trainings},
            {"ItemCategories", editTrainingEventArgs.ItemCategories},
            {"EditMode", true}
        };
        /*var dialog = await DialogService.ShowAsync<TrainingEditorDialogRef>(
            string.Format(Localization.editT, Localization.training),
            dialogParams);
        var result = await dialog.Result;
        if (result.Canceled)
        {
            ViewModel.ResetEditedTraining(editTrainingEventArgs.Training);
        }
        var editedTraining = result.Data as Training ?? trainingBeforeEdit;
        await ViewModel.UpdateEditedTraining(editedTraining);*/
    }
}