namespace Pcm.Application.Interfaces.RequestModels;

public interface IPersonRequest
{
    int PersonnelNumber { get; init; }
    string FirstName { get; init; }
    string LastName { get; init; }
    string EmailAddress { get; init; }
    string TrainingName { get; init; }
    string TrainingType { get; init; }
    string YearStarted { get; init; }
}