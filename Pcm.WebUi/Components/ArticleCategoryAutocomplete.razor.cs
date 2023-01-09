using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.WebUi.Models;
using Pcm.Infrastructure.Repositories;

namespace Pcm.WebUi.Components;

public partial class ArticleCategoryAutocomplete : ComponentBase
{
    
    private IEnumerable<ArticleCategory> _articleCategories;
    
    [Parameter] public ArticleCategory Value { get; set; }
    [Parameter] public EventCallback<ArticleCategory> ValueChanged { get; set; }

    [Inject] private IRepository<ArticleCategory, int> ArticleCategoryRepository { get; set; }

    protected override async void OnInitialized()
    {
        var articleCategories = await ArticleCategoryRepository.GetAll();
        _articleCategories = articleCategories;
    }
    
    private Task<IEnumerable<ArticleCategory>> SearchArticleCategoryAutocomplete(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Task.FromResult(_articleCategories);
        var filtered = _articleCategories.Where(x => x.ToString().Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(filtered);
    }
    
    
}