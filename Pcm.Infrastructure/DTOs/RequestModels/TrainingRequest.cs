namespace Pcm.Infrastructure.DTOs.RequestModels;

public record TrainingRequest
{
    public string Name { get; init; } = "";
    public string Type { get; init; } = "";
    public List<LoadOutPartRequest> LoadOut { get; init; } = Enumerable.Empty<LoadOutPartRequest>().ToList();
}