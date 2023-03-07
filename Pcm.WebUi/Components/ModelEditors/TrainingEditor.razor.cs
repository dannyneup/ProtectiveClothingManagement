using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.ModelEditors;

public partial class TrainingEditor : ComponentBase
{
    [Parameter] public TrainingResponseModel Training { get; set; }
    [Parameter] public EventCallback<TrainingResponseModel> TrainingChanged { get; set; }
    [Parameter] public List<LoadOutPartResponseModel> LoadOut { get; set; } = new();
    [Parameter] public EventCallback<List<LoadOutPartResponseModel>> LoadOutChanged { get; set; }
    [Parameter] public bool EditMode { get; set; } = false;
    [Inject] public IRepository<IItemCategoryResponseModel, IItemCategoryRequestModel> ItemCategoryRepository { get; set; }
    [Inject] public ITrainingRepository TrainingRepository { get; set; }

    private List<ItemCategoryResponseModel> _itemCategories;
    private string _searchString;

    protected override async Task OnInitializedAsync()
    {
        _itemCategories = await ItemCategoryRepository.GetAll() as List<ItemCategoryResponseModel>;
        if(EditMode)
        {
            LoadOut = await TrainingRepository.GetLoadOut(Training.Id) as List<LoadOutPartResponseModel>;
            return;
        }
        LoadOut = new();
    }

    private int getItemCount(ItemCategoryResponseModel category)
    {
        if (LoadOut.Count == 0)
        {
            return 0;
        }
        return LoadOut.Select(x => x.CategoryId == category.Id).Count();
    }
    
    private void OnCountChanged(ItemCategoryResponseModel category, int count)
    {
        
        var categoryAlreadyInLoadOut = LoadOut.Exists(x => x.CategoryId == category.Id);
        if (!categoryAlreadyInLoadOut)
        {
            LoadOut.Add(new LoadOutPartResponseModel()
            {
                CategoryId = category.Id,
                Count = count
            });
            return;
        }

        var loadOutPart = LoadOut.Find(x => x.CategoryId == category.Id);
        loadOutPart.Count = count;
    }

    private bool ItemCategoryFilter(ItemCategoryResponseModel arg)
    {
        return ListItemFilterController<IItemCategoryResponseModel>.CheckIfStringMatchesProperties(arg,
            _searchString);
    }
}