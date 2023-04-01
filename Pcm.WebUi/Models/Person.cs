using Pcm.Core.Entities;

namespace Pcm.WebUi.Models;

public class Person : IPerson
{
    public int PersonnelNumber { get; init; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string TrainingName { get; set; } = "";
    public string TrainingType { get; set; } = "";
    public string YearStarted { get; set; } = "";
}