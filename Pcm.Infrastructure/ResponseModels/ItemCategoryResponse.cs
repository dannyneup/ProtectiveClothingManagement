using Pcm.Application.Interfaces.ResponseModels;

namespace Pcm.Infrastructure.ResponseModels;

public class ItemCategoryResponse : ResponseBase, IItemCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}