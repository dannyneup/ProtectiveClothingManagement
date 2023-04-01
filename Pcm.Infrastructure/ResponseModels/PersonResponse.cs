using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Core.Entities;

namespace Pcm.Infrastructure.ResponseModels;

public class PersonResponse : ResponseBase, IPerson, IPersonResponse
{
    public string TrainingName { get; init; } = "";
    public string TrainingType { get; init; } = "";
    public string YearStarted { get; init; } = "";
    public int PersonnelNumber { get; init; }
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = "";
    public string EmailAddress { get; init; } = "";
}