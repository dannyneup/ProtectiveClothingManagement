using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.WebUi.Models;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class ItemCategoryAutocomplete : ComponentBase
{
    [Parameter, EditorRequired] public List<ItemCategory> ItemCategories { get; set; } = Enumerable.Empty<ItemCategory>().ToList();
    [Parameter] public ItemCategory ItemCategory { get; set; } = new();
    [Parameter] public EventCallback<ItemCategory> ItemCategoryChanged { get; set; }
    [Parameter] public bool Required { get; set; }

    private Task<IEnumerable<ItemCategory>> SearchAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return Task.FromResult<IEnumerable<ItemCategory>>(ItemCategories);
        return Task.FromResult(ItemCategories.Where(x => x.Name.Contains(searchString)));
    }

    private async Task OnValueChanged(ItemCategory itemCategory)
    {
        ItemCategory = itemCategory;
        await ItemCategoryChanged.InvokeAsync(itemCategory);
    }
}