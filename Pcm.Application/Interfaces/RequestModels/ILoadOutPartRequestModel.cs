namespace Pcm.Application.Interfaces.RequestModels;

public interface ILoadOutPartRequestModel
{
    int ItemCategoryId { get; set; }
    int Count { get; set; }
}