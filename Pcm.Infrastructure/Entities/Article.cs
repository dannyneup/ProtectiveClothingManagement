using Pcm.Core;
using Pcm.Core.Entities;

namespace Pcm.Infrastructure.Entities;

public class Article : ResponseBase, IArticle
{
    public int Id { get; set; }
    public IArticleType Type { get; set; }
    public string Style { get; set; }
    public IArticleCategory Category { get; set; }
    public string Size { get; set; }
    public IOrder Order { get; set; }
    public string Status { get; set; }
    public string StatusChanged { get; set; }
}