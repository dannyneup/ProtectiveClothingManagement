using Pcm.Application.Interfaces.RequestModels;

namespace Pcm.Infrastructure.RequestModels;

public class LoadOutPartRequest : ILoadOutPartRequest
{
    public int ItemCategoryId { get; init; }
    public int Count { get; init; }
}