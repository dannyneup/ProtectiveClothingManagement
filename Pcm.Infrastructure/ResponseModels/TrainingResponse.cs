using Pcm.Application.Interfaces.ResponseModels;

namespace Pcm.Infrastructure.ResponseModels;

public class TrainingResponse : ResponseBase, ITrainingResponse
{
    public int Id { get; init; }
    public string Type { get; init; } = "";
    public string Name { get; init; } = "";
    public int TraineeCount { get; init; }
    public int YearCount { get; init; }
}