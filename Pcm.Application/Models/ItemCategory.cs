namespace Pcm.Application.Models;

public record ItemCategory
{
    public int Id { get; init; }
    public string Name { get; set; } = "";
}