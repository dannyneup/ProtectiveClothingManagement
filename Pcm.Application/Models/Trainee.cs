using Pcm.Core.Entities;

namespace Pcm.Application.Models;

public record Trainee : IPerson
{
    public int PersonnelNumber { get; init; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public Training Training { get; set; } = default!;
    public int YearStarted { get; set; } = default;
}