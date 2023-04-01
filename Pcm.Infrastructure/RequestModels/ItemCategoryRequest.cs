using Pcm.Application.Interfaces.RequestModels;

namespace Pcm.Infrastructure.RequestModels;

public class ItemCategoryRequest : IItemCategoryRequest
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
}