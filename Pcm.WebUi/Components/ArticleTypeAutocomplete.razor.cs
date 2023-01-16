using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.Entities;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components;

public partial class ArticleTypeAutocomplete : ComponentBase
{
    [Parameter] public EventCallback<ArticleType> ValueChanged { get; set; }

    [Parameter] public ArticleType Value { get; set; }

    [Inject] public IRepository<IArticleType, int> ArticleTypeRepository { get; set; }
    [Inject] public IRepository<IArticleCategory, int> ArticleCategoryRepository { get; set; }
    
    public ArticleCategory SelectedArticleCategory { get; set; }
    
    private List<ArticleType> _articleTypes = new();

    private string _manufacturer;
    private IEnumerable<string> _manufacturers;

    private MudAutocomplete<ArticleType> _articleTypeAutocomplete;

    protected override async void OnInitialized()
    {
        var articleTypes = await ArticleTypeRepository.GetAll();
        _articleTypes = articleTypes as List<ArticleType>;
        var manufacturers = _articleTypes.Select(aT => aT.Manufacturer);
        _manufacturers = manufacturers.Distinct();
    }

    private async Task<IEnumerable<ArticleType>> SearchArticleTypeNameAutocomplete(string searchString)
    {
        var searchStringNullOrEmpty = string.IsNullOrEmpty(searchString);
        if (searchStringNullOrEmpty && SelectedArticleCategory == null)
            return _articleTypes;
        var filteredArticleTypes = FilterArticleTypesByArticleCategory(_articleTypes, SelectedArticleCategory);
        if (searchStringNullOrEmpty)
        {
            return filteredArticleTypes;
        }
        var searchWords = StringHandleController.CreateWordArray(searchString);
        filteredArticleTypes = FilterArticleTypesByWords(filteredArticleTypes, searchWords);
        return filteredArticleTypes;
    }
    private List<ArticleType> FilterArticleTypesByArticleCategory(IEnumerable<ArticleType> articleTypes, ArticleCategory articleCategory)
    {
        if (articleCategory == null)
        {
            return _articleTypes.ToList();
        }
        var filteredArticleTypes = articleTypes.Where(x => x.Category.Id == articleCategory.Id);
        return filteredArticleTypes.ToList();
    }

    private List<ArticleType> FilterArticleTypesByWords(List<ArticleType> articleTypes, string[] searchWords)
    {
        List<ArticleType> filteredArticleTypes = new();
        foreach (var word in searchWords)
        {
            var matches = articleTypes.Where(x =>
                x.Name.Contains(word) ||
                x.Manufacturer.Contains(word));
            filteredArticleTypes.AddRange(matches);
        }
        return filteredArticleTypes.Distinct().ToList();
    }

    private Task<IEnumerable<string>> SearchManufacturerAutocomplete(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Task.FromResult(_manufacturers);
        var filtered = _manufacturers.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(filtered);
    }

    private async Task OnValueChanged(ArticleType newValue)
    {
        Value = newValue;
        await ValueChanged.InvokeAsync(Value);
    }
    
}