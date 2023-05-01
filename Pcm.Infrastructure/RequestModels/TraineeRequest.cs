using Pcm.Core.Entities;

namespace Pcm.Infrastructure.RequestModels;

public record TraineeRequest : ResponseBase
{
    public int PersonnelNumber { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public int TrainingId { get; set; }
    public int YearStarted { get; set; }
}