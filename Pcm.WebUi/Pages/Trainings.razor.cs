using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Components.Dialogs;
using Pcm.WebUi.Models;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Pages;

public partial class Trainings
{
    private List<ItemCategory> _itemCategories;
    private string? _searchString;
    private Training _trainingBeforeEdit = new();

    private List<Training> _trainings = new();
    [Inject] public IDialogService DialogService { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
    [Inject] public IRepository<TrainingResponse, TrainingRequest> TrainingRepository { get; set; }
    [Inject] public IRepository<ItemCategoryResponse, ItemCategoryRequest> CategoryRepository { get; set; }
    [Inject] public IRepository<LoadOutPartResponse, LoadOutPartRequest> LoadOutRepository { get; set; }
    [Inject] public IRepository<PersonResponse, PersonRequest> PersonRepository { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetTrainings();
        await GetItemCategories();

        async Task GetTrainings()
        {
            var queries = new Dictionary<string, string>
            {
                {"extended", "true"}
            };
            var trainingResponses = await TrainingRepository.GetAll(queries);
            if (!trainingResponses.FirstOrDefault().IsResponseSuccess)
            {
                var errorMessage = String.Format(Localization.TCouldNotLoadedSuccessfully, Localization.trainings);
                Snackbar.Add(errorMessage, Severity.Error);
                return;
            }
            _trainings = trainingResponses.Select(x => new Training
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                TraineeCount = x.TraineeCount,
                YearCount = x.YearCount
            }).ToList();
        }

        async Task GetItemCategories()
        {
            var itemCategoryResponses = await CategoryRepository.GetAll();
            if (!itemCategoryResponses.FirstOrDefault().IsResponseSuccess)
            {
                var errorMessage = String.Format(Localization.TCouldNotLoadedSuccessfully, Localization.itemCategories);
                Snackbar.Add(errorMessage, Severity.Error);
                return;
            }
            _itemCategories = itemCategoryResponses.Select(x => new ItemCategory
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }


    private async Task OnAddButtonClicked()
    {
        var dialogParams = new DialogParameters
        {
            {"Trainings", _trainings},
            {"ItemCategories", _itemCategories},
            {"EditMode", false}
        };
        var dialog = await DialogService.ShowAsync<TrainingEditorDialog>(
            string.Format(Localization.editT, Localization.training),
            dialogParams);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            var training = result.Data as Training;
            _trainings.Append(training);
            StateHasChanged();
        }
    }

    private async Task ShowDetailDialog(Training training)
    {
        var persons = await GetPersons();
        training.LoadOut = await getLoadOut(training);

        var parameters = new DialogParameters
        {
            {"Persons", persons},
            {"LoadOut", training.LoadOut}
        };
        var dialog = await DialogService.ShowAsync<TrainingDetailsDialog>(Localization.trainingDetails, parameters);
        
        async Task<List<Person>> GetPersons()
        {
            var queries = new Dictionary<string, string>
            {
                {"extended", "true"},
                {"training-id", training.Id.ToString()}
            };
            var personResponses = await PersonRepository.GetAll(queries);
            return personResponses.Select(x => new Person
            {
                PersonnelNumber = x.PersonnelNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailAddress = x.EmailAddress,
                TrainingName = x.TrainingName,
                TrainingType = x.TrainingType,
                YearStarted = x.YearStarted
            }).ToList();
        }
    }

    private async Task EditTraining(Training training)
    {
        training.LoadOut = await getLoadOut(training);

        BackupTraining();

        var dialogParams = new DialogParameters
        {
            {"Training", training},
            {"Trainings", _trainings},
            {"ItemCategories", _itemCategories},
            {"EditMode", true}
        };
        var dialog = await DialogService.ShowAsync<TrainingEditorDialog>(
            string.Format(Localization.editT, Localization.training),
            dialogParams);
        var result = await dialog.Result;
        var editedEqualsUnedited = _trainingBeforeEdit.Equals(result.Data);
        if (editedEqualsUnedited) return;
        if (!result.Canceled)
        {
            var response = await UpdateTraining();
            if (response.IsResponseSuccess)
            {
                var successMessage =
                    string.Format(Localization.TWithNameSuccessfullyUpdated, Localization.training, training.Name);
                Snackbar.Add(successMessage, Severity.Success);
                return;
            }

            ResetTraining();
            var errorMessage =
                string.Format(Localization.TWithNameNotSuccessfullyUpdated, Localization.training,
                    _trainingBeforeEdit.Name);
            Snackbar.Add(errorMessage, Severity.Error);
            return;
        }

        ResetTraining();

        

        async Task<TrainingResponse> UpdateTraining()
        {
            training = result.Data as Training;
            StateHasChanged();
            var trainingRequest = new TrainingRequest
            {
                Name = training.Name,
                Type = training.Type
            };

            return await TrainingRepository.Update(trainingRequest, training.Id);
        }

        void BackupTraining()
        {
            _trainingBeforeEdit = new Training
            {
                Name = training.Name,
                Type = training.Type,
                LoadOut = training.LoadOut
            };
        }

        void ResetTraining()
        {
            training.Name = _trainingBeforeEdit.Name;
            training.Type = _trainingBeforeEdit.Type;
            training.LoadOut = _trainingBeforeEdit.LoadOut;
        }
    }

    private async Task<List<LoadOutPart>> getLoadOut(Training training)
    {
        var loadOutQueries = new Dictionary<string, string>
        {
            {"training-id", $"{training.Id}"}
        };
        var loadOutResponse = await LoadOutRepository.GetAll(loadOutQueries) as List<LoadOutPartResponse>;
        return loadOutResponse.Select(x => new LoadOutPart
        {
            CategoryId = x.CategoryId,
            CategoryName = x.CategoryName,
            Count = x.Count
        }).ToList();
    }

    private async Task DeleteTraining(Training training)
    {
        var deleted = await TrainingRepository.Delete(training.Id);
        if (deleted)
        {
            _trainings.Remove(training);
            Console.WriteLine(_trainings.Count());
            var SuccessMessage = string.Format(Localization.TWithNameSuccessfullyDeleted, Localization.training,
                training.Name);
            Snackbar.Add(SuccessMessage, Severity.Success);
            return;
        }

        var ErrorMessage = string.Format(Localization.TWithNameNotSuccessfullyDeleted, Localization.training,
            training.Name);
        Snackbar.Add(ErrorMessage, Severity.Error);
    }
}