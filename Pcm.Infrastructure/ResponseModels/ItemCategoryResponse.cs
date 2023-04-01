using Pcm.Application.Interfaces.ResponseModels;

namespace Pcm.Infrastructure.ResponseModels;

public class ItemCategoryResponse : ResponseBase, IItemCategoryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}