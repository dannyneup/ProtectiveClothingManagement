using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Models;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class ItemCategoryAutocomplete : ComponentBase
{
    [Parameter] public List<ItemCategory> ItemCategories { get; set; }
    [Parameter] public ItemCategory ItemCategory { get; set; }
    [Parameter] public EventCallback<ItemCategory> ItemCategoryChanged { get; set; }
    [Parameter] public bool Required { get; set; }

    private async Task<IEnumerable<ItemCategory>> SearchAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return ItemCategories;
        return ItemCategories.Where(x => x.Name.Contains(searchString));
    }

    private async Task OnValueChanged(ItemCategory itemCategory)
    {
        ItemCategory = itemCategory;
        await ItemCategoryChanged.InvokeAsync(itemCategory);
    }
}