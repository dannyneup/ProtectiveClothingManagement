using Microsoft.AspNetCore.Components;
using PcmFrontendWebUi.Models;
using PcmFrontendWebUi.Repositories;

namespace PcmFrontendWebUi.Components;

public partial class ArticleList : ComponentBase
{
    private IEnumerable<Article> _articles = new List<Article>();

    private string _searchString = "";

    [Inject] public IRepository<Article, int> ArticleRepository { get; set; }
    [Inject] public IRepository<Order, int> OrderRepository { get; set; }
    [Inject] public IRepository<ArticleType, int> ArticleTypeRepository { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _articles = await ArticleRepository.GetAll();
        foreach (var article in _articles)
        {
            article.Order = await OrderRepository.Get(article.Id);
            article.Type = await ArticleTypeRepository.Get(article.Id);
        }
    }

    private bool ArticleFilter(Article article)
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;
        var properties = new List<string>();
        properties.Add(article.Category.Name);
        properties.Add(article.Size);
        properties.Add(article.Type.Name);
        properties.Add(article.Type.Manufacturer);
        properties.Add(article.Order.Number);
        properties.RemoveAll(x => x == null);
        var matchedProperty = properties.FirstOrDefault(x => x.ToLower().Contains(_searchString.ToLower()));
        return matchedProperty != null;
    }
}