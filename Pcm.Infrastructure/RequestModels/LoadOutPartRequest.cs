namespace Pcm.Infrastructure.RequestModels;

public class LoadOutPartRequest : ResponseBase
{
    public int CategoryId { get; init; }
    public int Count { get; init; }
}