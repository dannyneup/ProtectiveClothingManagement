
namespace Pcm.Infrastructure.RequestModels;

public class TrainingRequestModel
{
    public int Id { get; init; }
    public string Name { get; set; } = "";
    //public int TraineeCount { get; set; }
    public string Type { get; set; } = "";
}