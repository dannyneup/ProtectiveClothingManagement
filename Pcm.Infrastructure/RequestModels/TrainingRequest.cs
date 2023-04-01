using Pcm.Application.Interfaces.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.RequestModels;

public class TrainingRequest
{
    public string Name { get; init; } = "";
    public string Type { get; init; } = "";
    public List<LoadOutPartRequest> LoadOut { get; init; } = Enumerable.Empty<LoadOutPartRequest>().ToList();
}