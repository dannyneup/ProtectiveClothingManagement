namespace Pcm.Application.Interfaces.RequestModels;

public interface ILoadOutPartRequest
{
    public int ItemCategoryId { get; init; }
    public int Count { get; init; }
}