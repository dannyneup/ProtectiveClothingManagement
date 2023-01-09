using Pcm.Core;
using Pcm.Core.Entities;

namespace Pcm.WebUi.Models;

public class ArticleType : ResponseBase, IArticleType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public IArticleCategory ArticleCategory { get; set; }
    public string Style { get; set; }

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