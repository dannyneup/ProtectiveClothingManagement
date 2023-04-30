namespace Pcm.Infrastructure.ResponseModels;

public class TrainingResponse : ResponseBase
{
    public int Id { get; init; }
    public string Type { get; init; } = "";
    public string Name { get; init; } = "";
    public int TraineeCount { get; init; }
    public int YearStarted { get; init; }
}