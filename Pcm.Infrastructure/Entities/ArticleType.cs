using System.Text.Json.Serialization;
using Pcm.Core.Entities;

namespace Pcm.Infrastructure.Entities;

public class ArticleType : ResponseBase, IArticleType
{
    public string Style { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }

    public IArticleCategory Category { get; set; }

    protected bool Equals(ArticleType other)
    {
        return Id == other.Id && Name == other.Name && Manufacturer == other.Manufacturer && Style == other.Style;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Manufacturer);
    }

    public override string ToString()
    {
        var outputString = Name;
        if (Style != null) outputString += $" ({Style})";
        return outputString;
    }
}