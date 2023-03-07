using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.RequestModels;

public class TrainingRequestModel : ITrainingRequestModel
{
    public string Type { get; set; }
    public string Name { get; set; }
}