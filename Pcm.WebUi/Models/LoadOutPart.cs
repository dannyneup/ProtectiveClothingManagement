namespace Pcm.WebUi.Models;

public class LoadOutPart : IEquatable<LoadOutPart>
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int Count { get; set; }

    public bool Equals(LoadOutPart? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return CategoryId == other.CategoryId && CategoryName == other.CategoryName && Count == other.Count;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LoadOutPart) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CategoryId, CategoryName, Count);
    }
}