using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class ItemCategoryAutocomplete : ComponentBase
{
    [Parameter, EditorRequired] public List<ItemCategoryResponse> ItemCategories { get; set; } = Enumerable.Empty<ItemCategoryResponse>().ToList();
    [Parameter] public ItemCategoryResponse ItemCategory { get; set; } = new();
    [Parameter] public EventCallback<ItemCategoryResponse> ItemCategoryChanged { get; set; }
    [Parameter] public bool Required { get; set; }

    private Task<IEnumerable<ItemCategoryResponse>> SearchAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return Task.FromResult<IEnumerable<ItemCategoryResponse>>(ItemCategories);
        return Task.FromResult(ItemCategories.Where(x => x.Title.Contains(searchString)));
    }

    private async Task OnValueChanged(ItemCategoryResponse itemCategory)
    {
        ItemCategory = itemCategory;
        await ItemCategoryChanged.InvokeAsync(itemCategory);
    }
}