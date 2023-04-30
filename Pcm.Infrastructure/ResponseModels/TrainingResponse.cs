using Pcm.Infrastructure.RequestModels;

namespace Pcm.Infrastructure.ResponseModels;

public record TrainingResponse : TrainingRequest
{
    public int Id { get; init; }
    public int TraineeCount { get; init; }
}