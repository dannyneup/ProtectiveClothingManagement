using Pcm.Core.Entities;

namespace Pcm.Application.Interfaces.RequestModels;

public interface ITrainingInfoRequestModel : ITraining
{
    string Type { get; set; }
    string Name { get; set; }
    int TraineeCount { get; set; }
}