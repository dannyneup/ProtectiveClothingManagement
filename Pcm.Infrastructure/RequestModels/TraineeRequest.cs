using Pcm.Core.Entities;

namespace Pcm.Infrastructure.RequestModels;

public class TraineeRequest : IPerson
{
    public int PersonnelNumber { get; init; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public int TrainingId { get; set; } = default!;
    public int YearStarted { get; set; } = default!;
}