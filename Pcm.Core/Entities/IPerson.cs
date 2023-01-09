namespace Pcm.Core.Entities;

public interface IPerson : IResponseBase
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    IApprenticeship Apprenticeship { get; set; }
    string EmailAddress { get; set; }
}