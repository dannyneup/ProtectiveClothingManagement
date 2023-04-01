using Pcm.Application.Interfaces.ResponseModels;

namespace Pcm.Infrastructure.ResponseModels;

public class TrainingResponse : ResponseBase, ITrainingResponse
{
    public int Id { get; set; }
    public string Type { get; set; } = "";
    public string Name { get; set; } = "";
    public int TraineeCount { get; set; }
    public int YearCount { get; set; }
}