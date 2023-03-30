namespace Pcm.WebUi.Models;

public class Training : IEquatable<Training>
{
    public int Id { get; init; }
    public string Type { get; set; }
    public string Name { get; set; }
    public int TraineeCount { get; set; }
    public int YearCount { get; set; }
    public List<LoadOutPart>? LoadOut { get; set; }

    public bool Equals(Training? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Type == other.Type && Name == other.Name && LoadOut.Equals(other.LoadOut);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Training) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Type, Name, LoadOut);
    }
}