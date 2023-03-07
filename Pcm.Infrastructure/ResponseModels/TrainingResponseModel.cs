using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Core.Entities;
using Pcm.Infrastructure.RequestModels;

namespace Pcm.Infrastructure.ResponseModels;

public class TrainingResponseModel : ResponseBase, ITrainingResponseModel
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        var training = obj as TrainingResponseModel;
        if (training == null)
        {
            return false;
        }
        return this.Id == training.Id &&
               this.Name == training.Name;
    }

    public TrainingRequestModel ToRequestModel()
    {
        return new TrainingRequestModel
        {
            Type = this.Type,
            Name = this.Name
        };
    }
}