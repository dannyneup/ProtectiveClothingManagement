using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.Refactor.Models;

public class DataModel
{
    private readonly IRepository<TrainingResponse, TrainingRequest> _trainingRepository;
    private readonly IRepository<LoadOutPartResponse, LoadOutPartRequest> _loadoutRepository;
    private readonly IRepository<ItemCategoryResponse, ItemCategoryRequest> _itemCategoryRepository;
    private List<TrainingResponse> _trainings = new();
    private List<ItemCategoryResponse> _itemCategories = new();
    private List<LoadOutPartResponse> _loadOutParts = new();
    
    public EventCallback<TrainingResponse> TrainingInsertRequestFinished { get; set; }
    public EventCallback<LoadOutPartResponse> LoadoutInsertRequestFinished { get; set; }
    
    public List<TrainingResponse> Trainings
    {
        get
        {
            if (_trainings.Count == 0)
            {
                _trainings = Task.Run(async () => await 
                    _trainingRepository.GetAll()).GetAwaiter().GetResult().ToList();
            }
            return _trainings;
        }
    }
    
    public List<LoadOutPartResponse> Loadouts
    {
        get
        {
            if (_loadOutParts.Count == 0)
            {
                _loadOutParts = Task.Run(async () => await 
                    _loadoutRepository.GetAll()).GetAwaiter().GetResult().ToList();
            }
            return _loadOutParts;
        }
    }
    
    public List<ItemCategoryResponse> ItemCategories
    {
        get
        {
            if (_itemCategories.Count == 0)
            {
                _itemCategories = Task.Run(async () => await 
                    _itemCategoryRepository.GetAll()).GetAwaiter().GetResult().ToList();
            }
            return _itemCategories;
        }
    }

    public DataModel(IRepository<TrainingResponse, TrainingRequest> trainingRepository, 
        IRepository<LoadOutPartResponse, LoadOutPartRequest> loadoutRepository, 
        IRepository<ItemCategoryResponse, ItemCategoryRequest> itemCategoryRepository)
    {
        _trainingRepository = trainingRepository;
        _loadoutRepository = loadoutRepository;
        _itemCategoryRepository = itemCategoryRepository;
    }

    public async Task AddTraining(TrainingRequest request)
    {
        var trainingResponse = await _trainingRepository.Insert(request);
        bool isSuccesful = trainingResponse.IsResponseSuccess;
        if (isSuccesful)
        {
            _trainings.Add(trainingResponse);
        }

        await TrainingInsertRequestFinished.InvokeAsync(trainingResponse);
    }
    
    public async Task AddLoadout(LoadOutPartRequest request)
    {
        var loadoutResponse = await _loadoutRepository.Insert(request);
        bool isSuccesful = loadoutResponse.IsResponseSuccess;
        if (isSuccesful)
        {
            _loadOutParts.Add(loadoutResponse);
        }

        await LoadoutInsertRequestFinished.InvokeAsync(loadoutResponse);
    }
    
    
}