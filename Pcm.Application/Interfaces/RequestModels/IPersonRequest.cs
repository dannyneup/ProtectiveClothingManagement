namespace Pcm.Application.Interfaces.RequestModels;

public interface IPersonRequest
{
    int PersonnelNumber { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string EmailAddress { get; set; }
    string TrainingName { get; set; }
    string TrainingType { get; set; }
    string YearStarted { get; set; }
}