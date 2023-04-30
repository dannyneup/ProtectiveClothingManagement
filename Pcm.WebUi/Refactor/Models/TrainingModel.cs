using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using ItemCategoryRequest = Pcm.WebUi.Refactor.RqRsp.ItemCategoryRequest;
using ItemCategoryResponse = Pcm.WebUi.Refactor.RqRsp.ItemCategoryResponse;

namespace Pcm.WebUi.Refactor.Models;

public class TrainingModel
{
    private readonly IRepository<TrainingResponse, TrainingRequest> _trainingRepository;
    private readonly IRepository<LoadOutPartResponse, LoadOutPartRequest> _loadoutRepository;
    private readonly IRepository<ItemCategoryResponse, ItemCategoryRequest> _itemCategoryRepository;
    private List<TrainingResponse> _trainings = new();
    private List<ItemCategoryResponse> _itemCategories = new();
    private List<LoadOutPartResponse> _loadOutParts = new();
    
    public EventCallback<TrainingResponse> InsertRequestFinished { get; set; }
    
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

    public TrainingModel(IRepository<TrainingResponse, TrainingRequest> trainingRepository, 
        IRepository<LoadOutPartResponse, LoadOutPartRequest> loadoutRepository, 
        IRepository<ItemCategoryResponse, ItemCategoryRequest> itemCategoryRepository)
    {
        _trainingRepository = trainingRepository;
        _loadoutRepository = loadoutRepository;
        _itemCategoryRepository = itemCategoryRepository;
    }

    public async Task Add(TrainingRequest request)
    {
        var trainingResponse = await _trainingRepository.Insert(request);
        bool isSuccesful = trainingResponse.IsResponseSuccess;
        if (isSuccesful)
        {
            _trainings.Add(trainingResponse);
        }

        await InsertRequestFinished.InvokeAsync(trainingResponse);
    }
}