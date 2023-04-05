namespace Pcm.Infrastructure.DTOs.ResponseModels;

public record LoadOutPartResponse : ResponseBase
{
    public int CategoryId { get; init; }
    public string CategoryName { get; init; } = "";
    public int Count { get; init; }
}