using Pcm.Core.Entities;

namespace Pcm.Infrastructure.ResponseModels;

public class LoadOutResponseModel : ILoadOut
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int Count { get; init; }
}