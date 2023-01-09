namespace Pcm.Core.Entities;

public interface IArticleType : IResponseBase
{
    int Id { get; set; }
    string Name { get; set; }
    string Manufacturer { get; set; }
    IArticleCategory ArticleCategory { get; set; }
    string Style { get; set; }
}