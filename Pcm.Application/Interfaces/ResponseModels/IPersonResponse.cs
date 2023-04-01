namespace Pcm.Application.Interfaces.ResponseModels;

public interface IPersonResponse : IResponseBase
{
    string TrainingName { get; set; }
    string TrainingType { get; set; }
    string YearStarted { get; set; }
    int PersonnelNumber { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string EmailAddress { get; set; }
    bool IsResponseSuccess { get; set; }
}