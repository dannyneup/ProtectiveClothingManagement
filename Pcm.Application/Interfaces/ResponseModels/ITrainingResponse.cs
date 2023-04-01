namespace Pcm.Application.Interfaces.ResponseModels;

public interface ITrainingResponse : IResponseBase
{
    int Id { get; set; }
    string Type { get; set; }
    string Name { get; set; }
    int TraineeCount { get; set; }
    int YearCount { get; set; }
    bool IsResponseSuccess { get; set; }
}