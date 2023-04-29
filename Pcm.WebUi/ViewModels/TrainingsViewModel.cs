using Microsoft.AspNetCore.Components;
using Pcm.Application.Models;
using Pcm.WebUi.CustomEventArgs;
using Pcm.WebUi.Models;

namespace Pcm.WebUi.ViewModels;

public class TrainingsViewModel : BaseViewModel
{
    private readonly TrainingModel _trainingModel;
    private readonly ItemCategoryModel _itemCategoryModel;
    private readonly TraineeModel _traineeModel;
    
    
    public EventCallback TrainingsChanged { get; set; }
    public event EventHandler<CreateNewTrainingEventArgs>? OpenCreateNewTraining;
    public event EventHandler<OpenTrainingDetailsEventArgs>? OpenTrainingDetails;
    public event EventHandler<EditTrainingEventArgs>? OpenEditTraining;
    
    public List<Training> Trainings { get; private set; } = default!;
    
    private List<ItemCategory> _itemCategories = default!;
    private Training _trainingBeforeEdit = default!;

    public TrainingsViewModel(TrainingModel trainingModel, ItemCategoryModel itemCategoryModel, TraineeModel traineeModel)
    {
        _trainingModel = trainingModel;
        _itemCategoryModel = itemCategoryModel;
        _traineeModel = traineeModel;
    }

    public async Task Init()
    {
        Trainings = (await _trainingModel.GetAll(extended: true)).ToList();
        _itemCategories = (await _itemCategoryModel.GetAll()).ToList();
    }

    public void AddTraining()
    {
        var args = new CreateNewTrainingEventArgs
        {
            Trainings = Trainings,
            ItemCategories = _itemCategories
        };
        OpenCreateNewTraining?.Invoke(this, args);
    }

    public async Task SaveNewTraining(Training training)
    {
        await _trainingModel.Add(training);
    }

    public async Task ShowTrainingDetails(Training training)
    {
        var trainees = await _traineeModel.GetAll();
        training.LoadOut = (await _trainingModel.GetLoadOut(training.Id)).ToList();
        var args = new OpenTrainingDetailsEventArgs
        {
            Trainees = trainees,
            LoadOut = training.LoadOut
        };
        OpenTrainingDetails?.Invoke(this, args);
    }
    
    public async Task EditTraining(Training training)
    {
        training.LoadOut = (await _trainingModel.GetLoadOut(training.Id)).ToList();
        BackupTraining();
        var args = new EditTrainingEventArgs
        {
            Training = training,
            Trainings = Trainings,
            ItemCategories = _itemCategories,
        };
        OpenEditTraining?.Invoke(this, args);
        
        void BackupTraining()
        {
            _trainingBeforeEdit = new Training
            {
                Name = training.Name,
                Type = training.Type,
                LoadOut = training.LoadOut
            };
        }
    }

    public void ResetEditedTraining(Training training)
    {
        training.Name = _trainingBeforeEdit.Name;
        training.Type = _trainingBeforeEdit.Type;
        training.LoadOut = _trainingBeforeEdit.LoadOut;
    }
    
    public async Task UpdateEditedTraining(Training editedTraining)
    {
        if (editedTraining.Equals(_trainingBeforeEdit))
        {
            return;
        }
        await UpdateTraining(editedTraining);
    }

    private async Task UpdateTraining(Training training)
    {
        await _trainingModel.Update(training);
    }

    public async Task DeleteTraining(Training training)
    {
        await _trainingModel.Delete(training);
    }

}