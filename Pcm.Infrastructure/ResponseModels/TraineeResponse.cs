
using Pcm.Core.Entities;
using Pcm.Infrastructure.RequestModels;

namespace Pcm.Infrastructure.ResponseModels;

public record TraineeResponse : TraineeRequest
{
    public string TrainingTitle { get; set; }
    public string TrainingType { get; set; }
}