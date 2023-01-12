using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.Entities;


namespace Pcm.WebUi.ViewModels;

public class NewArticleViewModel : INewArticleViewModel
{
    public NewArticleViewModel(IRepository<IArticleType, int> articleTypeRepository)
    {
        _articleTypeRepository = articleTypeRepository;
    }

    public ArticleType Value { get; set; }

    public ArticleCategory ArticleCategory { get; set; }

    private IRepository<IArticleType, int> _articleTypeRepository { get; set; }
}