
namespace Pcm.Infrastructure.ResponseModels;

public class TrainingResponse : ResponseBase
{
    public int Id { get; set; }
    public string Type { get; set; } = "";
    public string Name { get; set; } = "";
    public int TraineeCount { get; set; }
    public int YearCount { get; set; }

    /*public override bool Equals(object? obj)
    {
        var trainingInfo = obj as TrainingInfoResponseModel;
        if (trainingInfo == null)
        {
            return false;
        }
        return this.Id == trainingInfo.Id &&
               this.Name == trainingInfo.Name;
    }

    public TrainingResponseModel ToTrainingResponseModel()
    {
        return new TrainingResponseModel()
        {
            Id = this.Id,
            Name = this.Name,
            Type = this.Type
        };
    }*/
}