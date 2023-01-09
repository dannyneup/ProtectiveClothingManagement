namespace Pcm.WebUi.Models;

public interface INewArticleViewModel
{
    ArticleType Value { get; set; }
    ArticleCategory ArticleCategory { get; set; }
}