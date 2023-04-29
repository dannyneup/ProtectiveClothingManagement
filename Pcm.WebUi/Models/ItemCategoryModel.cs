using Pcm.Application.Interfaces.Repositories;
using Pcm.Application.Models;
using Pcm.Infrastructure.DTOs;
using Pcm.WebUi.Enums;

namespace Pcm.WebUi.Models;

public class ItemCategoryModel : ModelBase, IModel<ItemCategory>
{
    private readonly IRepository<ItemCategoryDto, ItemCategoryDto> _itemCategoryRepository;

    public ItemCategoryModel(IRepository<ItemCategoryDto, ItemCategoryDto> itemCategoryRepository)
    {
        _itemCategoryRepository = itemCategoryRepository;
    }

    public async Task<IEnumerable<ItemCategory>> GetAll(bool extended = false)
    {
        var itemCategoryResponses = await _itemCategoryRepository.GetAll();
        itemCategoryResponses = itemCategoryResponses.ToList();
        if (!itemCategoryResponses.FirstOrDefault()!.IsResponseSuccess)
        {
            InvokeOperationDone(this, new List<ItemCategory>(), CrudOperation.Load, false);
            return new List<ItemCategory>();
        }

        var itemCategories = itemCategoryResponses.Select(MapItemCategoryResponseToItemCategory);
        return itemCategories;
    }

    public async Task<ItemCategory> Get(int id, bool extended = false)
    {
        var query = new Dictionary<string, string>
        {
            {"extended", extended.ToString()}
        };
        var itemCategoryResponse = await _itemCategoryRepository.Get(id, query);
        if (!itemCategoryResponse.IsResponseSuccess)
        {
            InvokeOperationDone(this, new ItemCategory(), CrudOperation.Load, false);
            return new ItemCategory();
        }

        var itemCategory = MapItemCategoryResponseToItemCategory(itemCategoryResponse);
        return itemCategory;
    }

    public async Task Add(ItemCategory entity)
    {
        var personRequest = new ItemCategoryDto
        {
            Name = entity.Name
        };
        var itemCategoryDto = await _itemCategoryRepository.Insert(personRequest);
        if (!itemCategoryDto.IsResponseSuccess)
        {
            InvokeOperationDone(this, entity, CrudOperation.Create, false);
            return;
        }

        var insertedTrainee = MapItemCategoryResponseToItemCategory(itemCategoryDto);
        InvokeOperationDone(this, insertedTrainee, CrudOperation.Create, true);
    }

    public async Task Update(ItemCategory entity)
    {
        var itemCategoryRequest = new ItemCategoryDto
        {
            Name = entity.Name
        };
        var itemCategoryResponse = await _itemCategoryRepository.Update(itemCategoryRequest, entity.Id);
        if (!itemCategoryResponse.IsResponseSuccess)
        {
            InvokeOperationDone(this, entity, CrudOperation.Update, false);
            return;
        }

        var updatedItemCategory = MapItemCategoryResponseToItemCategory(itemCategoryResponse);
        InvokeOperationDone(this, updatedItemCategory, CrudOperation.Update, true);
    }

    public async Task Delete(ItemCategory entity)
    {
        var deleted = await _itemCategoryRepository.Delete(entity.Id);
        if (!deleted)
        {
            InvokeOperationDone(this, entity, CrudOperation.Delete, false);
            return;
        }

        InvokeOperationDone(this, entity, CrudOperation.Delete, true);
    }

    private ItemCategory MapItemCategoryResponseToItemCategory(ItemCategoryDto response)
    {
        return new ItemCategory
        {
            Id = response.Id,
            Name = response.Name
        };
    }
}