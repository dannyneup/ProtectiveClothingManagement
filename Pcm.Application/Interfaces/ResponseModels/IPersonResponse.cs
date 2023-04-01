namespace Pcm.Application.Interfaces.ResponseModels;

public interface IPersonResponse : IResponseBase
{
    string TrainingName { get; init; }
    string TrainingType { get; init; }
    string YearStarted { get; init; }
    int PersonnelNumber { get; init; }
    string FirstName { get; init; }
    string LastName { get; init; }
    string EmailAddress { get; init; }
}