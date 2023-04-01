using Pcm.Application.Interfaces.ResponseModels;

namespace Pcm.Infrastructure.ResponseModels;

public class LoadOutPartResponse : ResponseBase, ILoadOutPartResponse
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public int Count { get; set; }
}