using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.ModelEditors;

public partial class TrainingEditor : ComponentBase
{
    [Parameter] public TrainingResponse Training { get; set; }
    [Parameter] public EventCallback<TrainingResponse> TrainingChanged { get; set; }
    [Parameter] public List<LoadOutPartResponse> LoadOut { get; set; } = new();
    [Parameter] public EventCallback<List<LoadOutPartResponse>> LoadOutChanged { get; set; }
    [Parameter] public bool EditMode { get; set; } = false;
    [Inject] public IRepository<ItemCategoryResponse, ItemCategoryRequest> ItemCategoryRepository { get; set; }
    [Inject] public IRepository<LoadOutPartResponse, LoadOutPartRequest> TrainingRepository { get; set; }
    [Inject] public IRepository<LoadOutPartResponse, LoadOutPartRequest> LoadoutRepository { get; set; }

    private IEnumerable<ItemCategoryResponse> _itemCategories;
    private string _searchString;

    protected override async Task OnInitializedAsync()
    {
        _itemCategories = await ItemCategoryRepository.GetAll();
        if(EditMode)
        {
            var loadouts = await LoadoutRepository.GetAll();
            LoadOut = loadouts.ToList();
            return;
        }
        LoadOut = new();
    }

    private int getItemCount(ItemCategoryResponse category)
    {
        if (LoadOut.Count == 0)
        {
            return 0;
        }
        return LoadOut.Select(x => x.CategoryId == category.Id).Count();
    }
    
    private void OnCountChanged(ItemCategoryResponse category, int count)
    {
        
        var categoryAlreadyInLoadOut = LoadOut.Exists(x => x.CategoryId == category.Id);
        if (!categoryAlreadyInLoadOut)
        {
            LoadOut.Add(new LoadOutPartResponse()
            {
                CategoryId = category.Id,
                Count = count
            });
            return;
        }

        var loadOutPart = LoadOut.Find(x => x.CategoryId == category.Id);
        loadOutPart.Count = count;
    }

    private bool ItemCategoryFilter(ItemCategoryResponse arg)
    {
        return ListItemFilterController<ItemCategoryResponse>.CheckIfStringMatchesProperties(arg,
            _searchString);
    }
}