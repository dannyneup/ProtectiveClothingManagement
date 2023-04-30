namespace Pcm.Infrastructure.RequestModels;

public record ItemCategoryRequest : ResponseBase
{
    public string Title { get; init; } = "";
}