namespace Pcm.Application.Interfaces.ResponseModels;

public interface ILoadOutPartResponseModel
{
    int CategoryId { get; set; }
    string CategoryName { get; set; }
    int Count { get; set; }
}