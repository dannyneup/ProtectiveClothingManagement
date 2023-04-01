namespace Pcm.Application.Interfaces.ResponseModels;

public interface ILoadOutPartResponse : IResponseBase
{
    int CategoryId { get; init; }
    string CategoryName { get; init; }
    int Count { get; init; }
}