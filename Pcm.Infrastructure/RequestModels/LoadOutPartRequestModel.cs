using Pcm.Application.Interfaces.RequestModels;

namespace Pcm.Infrastructure.RequestModels;

public class LoadOutPartRequestModel : ILoadOutPartRequestModel
{
    public int ItemCategoryId { get; set; }
    public int Count { get; set; }
}