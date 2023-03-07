using Pcm.Core.Entities;

namespace Pcm.Application.Interfaces.ResponseModels;

public interface ITrainingResponseModel : ITraining
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
}