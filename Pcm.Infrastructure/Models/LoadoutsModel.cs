using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.Models;

public class LoadoutsModel
{
    private readonly IRepository<LoadOutPartResponse, LoadOutPartRequest> _loadoutRepository;
    private readonly IRepository<ItemCategoryResponse, ItemCategoryRequest> _itemCategoryRepository;
    private List<LoadOutPartResponse> _loadOutParts = new();
    private List<ItemCategoryResponse> _itemCategories = new();


    public EventCallback<LoadOutPartResponse> LoadoutRequestFinished { get; set; }

    public IEnumerable<LoadOutPartResponse> Loadouts
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


    public LoadoutsModel(IRepository<LoadOutPartResponse, LoadOutPartRequest> loadoutRepository,
        IRepository<ItemCategoryResponse, ItemCategoryRequest> itemCategoryRepository)
    {
        _loadoutRepository = loadoutRepository;
        _itemCategoryRepository = itemCategoryRepository;
    }

    public async Task AddLoadout(LoadOutPartRequest request)
    {
        var loadoutResponse = await _loadoutRepository.Insert(request);
        bool isSuccessful = loadoutResponse.IsResponseSuccess;
        if (isSuccessful)
        {
            _loadOutParts.Add(loadoutResponse);
        }

        await LoadoutRequestFinished.InvokeAsync(loadoutResponse);
    }

    public async Task UpdateLoadout(LoadOutPartRequest request, int id)
    {
        var loadoutResponse = await _loadoutRepository.Update(request, id);
        var isSuccessful = loadoutResponse.IsResponseSuccess;
        if (isSuccessful)
        {
            _loadOutParts.RemoveAll(x => x.Id == id);
            _loadOutParts.Add(loadoutResponse);
        }

        await LoadoutRequestFinished.InvokeAsync(loadoutResponse);
    }
}