using Pcm.Application.Interfaces.RequestModels;

namespace Pcm.Infrastructure.RequestModels;

public class LoadOutPartRequest : ILoadOutPartRequest
{
    public int ItemCategoryId { get; set; }
    public int Count { get; set; }
}