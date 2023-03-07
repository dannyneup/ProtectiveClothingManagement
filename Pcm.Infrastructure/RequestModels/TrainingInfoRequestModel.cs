using Pcm.Application.Interfaces.RequestModels;

namespace Pcm.Infrastructure.RequestModels;

public class TrainingInfoRequestModel : ITrainingInfoRequestModel
{
    public int Id { get; init; }
    public string Name { get; set; }
    public int TraineeCount { get; set; }
    public string Type { get; set; }
}