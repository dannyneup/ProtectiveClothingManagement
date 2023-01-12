using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.ViewModels;

public interface INewArticleViewModel
{
    ArticleType Value { get; set; }
    ArticleCategory ArticleCategory { get; set; }
}