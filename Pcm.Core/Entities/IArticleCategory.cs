namespace Pcm.Core.Entities;

public interface IArticleCategory : IResponseBase
{
    int Id { get; set; }
    string Name { get; set; }
    
    
}