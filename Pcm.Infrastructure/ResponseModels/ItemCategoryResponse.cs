using Pcm.Infrastructure.RequestModels;

namespace Pcm.Infrastructure.ResponseModels;

public record ItemCategoryResponse : ItemCategoryRequest
{
    public int Id { get; init; }
}