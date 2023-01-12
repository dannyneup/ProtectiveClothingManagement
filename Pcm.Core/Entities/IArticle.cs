namespace Pcm.Core.Entities;

public interface IArticle : IResponseBase
{
    int Id { get; set; }
    IArticleType Type { get; set; }
    string Style { get; set; }
    string Size { get; set; }
    IOrder Order { get; set; }
    string Status { get; set; }
    string StatusChanged { get; set; }
}