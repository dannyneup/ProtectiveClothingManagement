using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.ViewModels;

public class NewArticleViewModel : INewArticleViewModel
{
    public NewArticleViewModel(IRepository<IArticleType, int> articleTypeRepository)
    {
        _articleTypeRepository = articleTypeRepository;
    }

    private IRepository<IArticleType, int> _articleTypeRepository { get; }

    public ArticleType Value { get; set; }

    public ArticleCategory ArticleCategory { get; set; }
}