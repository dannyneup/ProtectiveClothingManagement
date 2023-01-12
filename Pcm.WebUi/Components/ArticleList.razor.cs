using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components;

public partial class ArticleList : ComponentBase
{
    private IEnumerable<Article> _articles = new List<Article>();
    private bool _newArticlePopupIsOpen;
    private string _searchString = "";

    [Inject] public IRepository<IArticle, int> ArticleRepository { get; set; }
    [Inject] public IRepository<IOrder, int> OrderRepository { get; set; }
    [Inject] public IRepository<IArticleType, int> ArticleTypeRepository { get; set; }
    [Inject] public IRepository<IArticleCategory, int> ArticleCategoryRepository { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _articles = await ArticleRepository.GetAll() as List<Article>;
        foreach (var article in _articles)
        {
            article.Order = await OrderRepository.Get(article.Order.Id);
            article.Type = await ArticleTypeRepository.Get(article.Order.Id);
        }
    }

    private bool ArticleFilter(Article article)
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;
        var properties = new List<string>();
        properties.Add(article.Type.Category.Name);
        properties.Add(article.Size);
        properties.Add(article.Type.Name);
        properties.Add(article.Type.Manufacturer);
        properties.Add(article.Order.Number);
        properties.RemoveAll(x => x == null);
        var matchedProperty = properties.FirstOrDefault(x => x.ToLower().Contains(_searchString.ToLower()));
        return matchedProperty != null;
    }

    private void ToggleNewArticlePopover()
    {
        _newArticlePopupIsOpen = !_newArticlePopupIsOpen;
    }
}