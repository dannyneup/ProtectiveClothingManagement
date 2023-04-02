using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Components.Dialogs;
using Pcm.WebUi.Models;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Pages;

public partial class Trainings
{
    private string _searchString = "";
    private Training _trainingBeforeEdit = new();

    private List<ItemCategory> _itemCategories = Enumerable.Empty<ItemCategory>().ToList();
    private List<Training> _trainings = Enumerable.Empty<Training>().ToList();
    [Inject] public IDialogService DialogService { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;
    [Inject] public ITrainingRepository<TrainingResponse, TrainingRequest> TrainingRepository { get; set; } = default!;
    [Inject] public IRepository<ItemCategoryResponse, ItemCategoryRequest> CategoryRepository { get; set; } = default!;
    [Inject] public IRepository<PersonResponse, PersonRequest> PersonRepository { get; set; } = default!;
    

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _trainings = await GetTrainings();
            StateHasChanged();
        }

        async Task<List<Training>> GetTrainings()
        {
            var queries = new Dictionary<string, string>
            {
                {"extended", "true"}
            };
            var trainingResponses = await TrainingRepository.GetAll(queries);
            trainingResponses = trainingResponses.ToList();
            if (!trainingResponses.FirstOrDefault()!.IsResponseSuccess)
            {
                var errorMessage = String.Format(Localization.TCouldNotLoadedSuccessfully, Localization.trainings);
                Snackbar.Add(errorMessage, Severity.Error);
                return Enumerable.Empty<Training>().ToList();
            }
            return trainingResponses.Select(x => new Training
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                TraineeCount = x.TraineeCount,
                YearCount = x.YearCount
            }).ToList();
        }
    }

    


    private async Task OnAddButtonClicked()
    {
        _itemCategories = await GetItemCategoriesIfNotAlreadyLoaded();
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
            var training = (result.Data as Training) ?? new Training();
            await AddTraining(training);
        }
    }

    private async Task<List<ItemCategory>> GetItemCategoriesIfNotAlreadyLoaded()
    {
        if (_itemCategories.Count == 0)
        {
            return await GetItemCategories();
        }

        return _itemCategories;
    }

    private async Task AddTraining(Training training)
    {
        _trainings = _trainings.Append(training).ToList();
        StateHasChanged();

        var trainingRequest = new TrainingRequest
        {
            Name = training.Name,
            Type = training.Type,
            LoadOut = training.LoadOut.Select(x => new LoadOutPartRequest
            {
                ItemCategoryId = x.CategoryId,
                Count = x.Count
            }).ToList()
        };
        var trainingResponse = await TrainingRepository.Insert(trainingRequest);
        if (!trainingResponse.IsResponseSuccess)
        {
            var errorMessage = String.Format(Localization.tWithNameNotSuccessfullyCreated, Localization.training,
                training.Name);
            Snackbar.Add(errorMessage, Severity.Error);
            return;
        }
        var successMessage = String.Format(Localization.tWithNameSuccessfullyCreated, Localization.training,
                training.Name);
        Snackbar.Add(successMessage, Severity.Success);
    }

    private async Task ShowDetailDialog(Training training)
    {
        var persons = await GetPersons();
        training.LoadOut = await GetLoadOut(training);

        var parameters = new DialogParameters
        {
            {"Persons", persons},
            {"LoadOut", training.LoadOut}
        };
        await DialogService.ShowAsync<TrainingDetailsDialog>(Localization.trainingDetails, parameters);
        
        
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
        training.LoadOut = await GetLoadOut(training);

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
                    string.Format(Localization.tWithNameSuccessfullyUpdated, Localization.training, training.Name);
                Snackbar.Add(successMessage, Severity.Success);
                return;
            }

            ResetTraining();
            var errorMessage =
                string.Format(Localization.tWithNameNotSuccessfullyUpdated, Localization.training,
                    _trainingBeforeEdit.Name);
            Snackbar.Add(errorMessage, Severity.Error);
            return;
        }

        ResetTraining();
        

        async Task<TrainingResponse> UpdateTraining()
        {
            training = (result.Data as Training) ?? new Training();
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

    private async Task<List<LoadOutPart>> GetLoadOut(Training training)
    {
        var loadOutResponse = (await TrainingRepository.GetLoadOut(training.Id) as List<LoadOutPartResponse>) ?? new List<LoadOutPartResponse>();
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
            var successMessage = string.Format(Localization.tWithNameSuccessfullyDeleted, Localization.training,
                training.Name);
            Snackbar.Add(successMessage, Severity.Success);
            return;
        }

        var errorMessage = string.Format(Localization.tWithNameNotSuccessfullyDeleted, Localization.training,
            training.Name);
        Snackbar.Add(errorMessage, Severity.Error);
    }
    private async Task<List<ItemCategory>> GetItemCategories()
    {
        var itemCategoryResponses = await CategoryRepository.GetAll();
        itemCategoryResponses = itemCategoryResponses.ToList();
        if (!itemCategoryResponses.FirstOrDefault()!.IsResponseSuccess)
        {
            var errorMessage = String.Format(Localization.TCouldNotLoadedSuccessfully, Localization.itemCategories);
            Snackbar.Add(errorMessage, Severity.Error);
            return Enumerable.Empty<ItemCategory>().ToList();
        }

        return itemCategoryResponses.Select(x => new ItemCategory
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}