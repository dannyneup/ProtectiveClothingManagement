using Microsoft.AspNetCore.Components;
using PcmFrontendWebUi.Models;
using PcmFrontendWebUi.Repositories;

namespace PcmFrontendWebUi.Components;

public partial class NewArticle : ComponentBase
{
    [Inject] public IRepository<Article, int> ArticleRepository { get; set; }
    [Inject] public IRepository<Person, int> PersonRepository { get; set; }
    [Inject] public IRepository<ArticleCategory, int> ArticleCategoryRepository { get; set; }
    [Inject] public IRepository<ArticleType, int> ArticleTypeRepository { get; set; }

    private string _personName;
    private IEnumerable<Person> _persons;
    private string _articleCategoryName;
    private IEnumerable<ArticleCategory> _articleCategories;
    private ArticleType _articleType;
    private IEnumerable<ArticleType> _articleTypes;
    private string _style;
    private IEnumerable<string> _styles;
    private string _size;
    private IEnumerable<string> _sizes;
    private string _orderNumber;
    private IEnumerable<string> _orderNumbers;
    private string _shop;
    private IEnumerable<string> _shops;
    private DateTime _orderDate;
    private IEnumerable<DateTime> _orderDates;
    private string _statusName;
    private IEnumerable<string> _states;
    private DateTime _statusChanged;
    
    private bool _processing;

    protected override async void OnInitialized()
    {
        var persons = await PersonRepository.GetAll();
        _persons = persons;
        var articleCategories = await ArticleCategoryRepository.GetAll();
        _articleCategories = articleCategories;
        var articleTypes = await ArticleTypeRepository.GetAll();
        _articleTypes = articleTypes;
    }


    private Task<IEnumerable<string>> SearchPersonAutocomplete(string value)
    {
        var personNames = _persons.Select(p=>$"{p.FirstName} {p.LastName}");
        if (string.IsNullOrEmpty(value))
            return Task.FromResult<IEnumerable<string>>(personNames);
        var filtered = personNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(filtered);
    }

    private Task<IEnumerable<string>> SearchArticleCategoryAutocomplete(string value)
    {
        var articleCategoryNames = _articleCategories.Select(x=>x.Name);
        if (string.IsNullOrEmpty(value))
            return Task.FromResult<IEnumerable<string>>(articleCategoryNames);
        var filtered = articleCategoryNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(filtered);
    }

    private Task<IEnumerable<ArticleType>> SearchArticleTypeNameAutocomplete(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Task.FromResult<IEnumerable<ArticleType>>(_articleTypes);
        var filtered = _articleTypes.Where(x => $"{x.Name} {x.Manufacturer}".Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(filtered);
    }

    private Task<IEnumerable<string>> SearchStyleAutocomplete(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Task.FromResult<IEnumerable<string>>(_styles);
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