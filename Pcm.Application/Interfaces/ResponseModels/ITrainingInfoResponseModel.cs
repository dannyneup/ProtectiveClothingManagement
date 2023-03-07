using Pcm.Core.Entities;

namespace Pcm.Application.Interfaces.ResponseModels;

public interface ITrainingInfoResponseModel : ITraining
{
    int Id { get; set; }
    string Type { get; set; }
    string Name { get; set; }
    int TraineeCount { get; set; }
    int YearCount { get; set; }
}