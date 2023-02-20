using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class ArticleCategoryAutocomplete : ComponentBase
{
    private IEnumerable<ArticleCategory> _articleCategories;

    [Parameter] public ArticleCategory Value { get; set; }

    [Parameter] public EventCallback<ArticleCategory> ValueChanged { get; set; }

    [Parameter] public bool ResetValueOnEmptyText { get; set; } = false;

    [Inject] private IRepository<IArticleCategory, int> ArticleCategoryRepository { get; set; }

    protected override async void OnInitialized()
    {
        var articleCategories = await ArticleCategoryRepository.GetAll();
        _articleCategories = articleCategories as IEnumerable<ArticleCategory>;
    }

    private Task<IEnumerable<ArticleCategory>> SearchArticleCategoryAutocomplete(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Task.FromResult(_articleCategories);
        var filtered =
            _articleCategories.Where(x => x.ToString().Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(filtered);
    }
}