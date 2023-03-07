using System.Security;
using Pcm.Application.Interfaces.ResponseModels;

namespace Pcm.Infrastructure.ResponseModels;

public class ItemCategoryResponseModel : ResponseBase, IItemCategoryResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}