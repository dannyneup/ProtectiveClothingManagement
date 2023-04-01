namespace Pcm.Application.Interfaces.RequestModels;

public interface ILoadOutPartRequest
{
    public int ItemCategoryId { get; set; }
    public int Count { get; set; }
}