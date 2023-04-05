namespace Pcm.Infrastructure.DTOs;

public record ItemCategoryDto : ResponseBase
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}