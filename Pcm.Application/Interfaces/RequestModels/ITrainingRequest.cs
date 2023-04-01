namespace Pcm.Application.Interfaces.RequestModels;

public interface ITrainingRequest
{
    string Name { get; set; }
    string Type { get; set; }
}