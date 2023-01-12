using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.WebUi.Controller;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components;

public partial class ArticleTypeAutocomplete : ComponentBase
{
    [Parameter] public EventCallback<ArticleType> ValueChanged { get; set; }

    [Parameter] public ArticleType Value { get; set; }

    [Inject] public IRepository<IArticleType, int> ArticleTypeRepository { get; set; }
    [Inject] public IRepository<IArticleCategory, int> ArticleCategoryRepository { get; set; }
    
    public ArticleCategory ArticleCategory { get; set; }
    
    private List<ArticleType> _articleTypes = new();

    private string _manufacturer;
    private IEnumerable<string> _manufacturers;

    private MudAutocomplete<ArticleType> _articleTypeAutocomplete;


    public async Task Update(ArticleCategory category)
    {
        ArticleCategory = category;
        await InvokeAsync(StateHasChanged);
    }

    protected override async void OnInitialized()
    {
        var articleTypes = await ArticleTypeRepository.GetAll();
        _articleTypes = articleTypes as List<ArticleType>;
        var articleCategories = await ArticleCategoryRepository.GetAll();
        //foreach (var articleType in _articleTypes)
        //{
        //    articleType.ArticleCategory = articleCategories.Where(x => 
        //        x.Id == articleType.ArticleCategory.Id) 
        //        as ArticleCategory;
        //}
        var manufacturers = _articleTypes.Select(aT => aT.Manufacturer);
        _manufacturers = manufacturers.Distinct();
        Value = articleTypes.First() as ArticleType;
    }

    private async Task<IEnumerable<ArticleType>> SearchArticleTypeNameAutocomplete(string searchString)
    {
        if (string.IsNullOrEmpty(searchString) && ArticleCategory.Name == "")
            return _articleTypes;
        var filteredArticleTypes = _articleTypes.Where(x => x.Category == ArticleCategory);
        var searchWords = StringHandleController.CreateWordArray(searchString);
        filteredArticleTypes = filterArticleTypesByWords(searchWords);
        return filteredArticleTypes;
    }

    private List<ArticleType> filterArticleTypesByWords(string[] searchWords)
    {
        List<ArticleType> filteredArticleTypes = new();
        foreach (var word in searchWords)
        {
            var matches = _articleTypes.Where(x =>
                x.Name.Contains(word) ||
                x.Manufacturer.Contains(word));
            filteredArticleTypes.AddRange(matches);
        }
        return filteredArticleTypes;
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