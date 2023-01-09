using Pcm.Core;
using Pcm.Core.Entities;

namespace Pcm.Infrastructure.Entities;

public class ArticleCategory : ResponseBase, IArticleCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public override string ToString()
    {
        return Name;
    }
}