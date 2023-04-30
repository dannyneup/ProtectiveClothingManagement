using Pcm.Infrastructure.RequestModels;

namespace Pcm.Infrastructure.ResponseModels;

public class ItemCategoryResponse : ItemCategoryRequest
{
    public int Id { get; init; }
}