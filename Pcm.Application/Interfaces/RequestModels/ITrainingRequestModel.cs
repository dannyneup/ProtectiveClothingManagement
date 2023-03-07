namespace Pcm.Infrastructure.RequestModels;

public interface ITrainingRequestModel
{
    string Type { get; set; }
    string Name { get; set; }
}