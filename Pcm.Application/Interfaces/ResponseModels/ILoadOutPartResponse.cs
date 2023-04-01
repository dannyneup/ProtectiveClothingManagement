namespace Pcm.Application.Interfaces.ResponseModels;

public interface ILoadOutPartResponse : IResponseBase
{
    int CategoryId { get; set; }
    string CategoryName { get; set; }
    int Count { get; set; }
    bool IsResponseSuccess { get; set; }
}