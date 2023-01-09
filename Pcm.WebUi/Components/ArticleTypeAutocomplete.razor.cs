using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Models;
using Pcm.Infrastructure.Repositories;
using ArticleCategory = Pcm.WebUi.Models.ArticleCategory;

namespace Pcm.WebUi.Components;

public partial class ArticleTypeAutocomplete : ComponentBase
{
    [Parameter] public EventCallback<IArticleType> ValueChanged { get; set; }

    [Parameter] public IArticleType Value { get; set; }

    [Inject] public IRepository<IArticleType, int> ArticleTypeRepository { get; set; }
    
    private List<IArticleType> _articleTypes = new();

    private string _manufacturer;
    private IEnumerable<string> _manufacturers;

    private ArticleCategory _articleCategory = new();
    private MudAutocomplete<IArticleType> _articleTypeAutocomplete;


    public async Task Update(IArticleCategory category)
    {
        _articleCategory = (ArticleCategory) category;
        await InvokeAsync(StateHasChanged);
    }

    protected override async void OnInitialized()
    {
        var articleTypes = await ArticleTypeRepository.GetAll();
        foreach (var aT in articleTypes)
        {
            var result = await ArticleTypeRepository.Get(aT.Id);
            _articleTypes.Add(result);
        }
        var manufacturers = _articleTypes.Select(aT => aT.Manufacturer);
        _manufacturers = manufacturers.Distinct();
        Value = articleTypes.First();
    }

    private async Task<IEnumerable<IArticleType>> SearchArticleTypeNameAutocomplete(string searchString)
    {
        if (string.IsNullOrEmpty(searchString) && _articleCategory.Name == "")
            return _articleTypes;
        var searchWords = StringHandleController.CreateWordArray(searchString);
        var filteredArticleTypes = filterArticleTypesByWords(searchWords);
        return filteredArticleTypes;
    }

    private List<IArticleType> filterArticleTypesByWords(string[] searchWords)
    {
        List<IArticleType> filteredArticleTypes = new();
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

    private async Task OnValueChanged(IArticleType newValue)
    {
        Value = newValue;
        await ValueChanged.InvokeAsync(Value);
    }
    
}