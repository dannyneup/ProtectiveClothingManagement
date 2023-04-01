namespace Pcm.Application.Interfaces.ResponseModels;

public interface ITrainingResponse : IResponseBase
{
    int Id { get; init; }
    string Type { get; init; }
    string Name { get; init; }
    int TraineeCount { get; init; }
    int YearCount { get; init; }
}