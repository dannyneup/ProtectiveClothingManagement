namespace Pcm.Infrastructure.DTOs.ResponseModels;

public record TrainingResponse : ResponseBase
{
    public int Id { get; init; }
    public string Type { get; init; } = "";
    public string Name { get; init; } = "";
    public int TraineeCount { get; init; }
    public int YearCount { get; init; }
}