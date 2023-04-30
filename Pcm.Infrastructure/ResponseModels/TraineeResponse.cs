
using Pcm.Core.Entities;

namespace Pcm.Infrastructure.ResponseModels;

public class TraineeResponse : ResponseBase, IPerson
{
    public int PersonnelNumber { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public int TrainingId { get; set; }
    public string TrainingName { get; set; }
    public string TrainingType { get; set; }
    public int YearStarted { get; set; }
}