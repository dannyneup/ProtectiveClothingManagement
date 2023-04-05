namespace Pcm.Infrastructure.DTOs.RequestModels;

public record LoadOutPartRequest
{
    public int ItemCategoryId { get; init; }
    public int Count { get; init; }
}