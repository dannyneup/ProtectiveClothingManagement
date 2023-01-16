using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.Entities;
using Pcm.WebUi.Components.Autocompletes;

namespace Pcm.WebUi.Components;

public partial class NewArticle : ComponentBase
{
    private ArticleCategory _articleCategory;
    private IEnumerable<Article> _articles;
    private ArticleType _articleType;

    private bool _isVisible = false;
    private string _manufacturer;
    private DateTime _orderDate;
    private IEnumerable<DateTime> _orderDates;
    private string _orderNumber;
    private IEnumerable<string> _orderNumbers;

    private Person _person;

    private bool _processing;
    private string _shop;
    private IEnumerable<string> _shops;
    private string _size;
    private IEnumerable<string> _sizes;
    private IEnumerable<string> _states;
    private DateTime _statusChanged;
    private string _statusName;
    private string _style;
    private IEnumerable<string> _styles;

    private ArticleTypeAutocomplete _articleTypeAutocompleteRef;

    private async Task articleCategoryChanged(ArticleCategory category)
    {
        _articleCategory = category;
        _articleTypeAutocompleteRef.SelectedArticleCategory = category;
    }

    [Inject] public IRepository<IArticle, int> ArticleRepository { get; set; }
    [Inject] public IRepository<IArticleCategory, int> ArticleCategoryRepository { get; set; }

    protected override async void OnInitialized()
    {
        var articles = await ArticleRepository.GetAll();
        _articles = (IEnumerable<Article>) articles;
    }

    private Task<IEnumerable<string>> SearchStyleAutocomplete(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(_articleType.ToString()))
            return new Task<IEnumerable<string>>(null);
        var filtered = _styles.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(filtered);
    }

    private async Task SaveNewArticle()
    {
        _processing = true;
        await Task.Delay(2000);
        _processing = false;
    }
}