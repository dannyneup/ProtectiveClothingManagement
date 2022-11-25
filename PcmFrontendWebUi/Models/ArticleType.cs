namespace PcmFrontendWebUi.Models;

public class ArticleType : ResponseBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }

    protected bool Equals(ArticleType other)
    {
        return Id == other.Id && Name == other.Name && Manufacturer == other.Manufacturer;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Manufacturer);
    }

    public override string ToString()
    {
        return $"{this.Name} ({this.Manufacturer})";
    }
}