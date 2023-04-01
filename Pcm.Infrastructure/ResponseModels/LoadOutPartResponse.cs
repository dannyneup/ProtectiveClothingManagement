using Pcm.Application.Interfaces.ResponseModels;

namespace Pcm.Infrastructure.ResponseModels;

public class LoadOutPartResponse : ResponseBase, ILoadOutPartResponse
{
    public int CategoryId { get; init; }
    public string CategoryName { get; init; } = "";
    public int Count { get; init; }
}