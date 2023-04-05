namespace Pcm.Infrastructure.DTOs;

public record PersonDto : ResponseBase
{
    public int PersonnelNumber { get; init; }
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = "";
    public string TrainingName { get; init; } = "";
    public string TrainingType { get; init; } = "";
    public string YearStarted { get; init; } = "";
    public string EmailAddress { get; init; } = "";
}