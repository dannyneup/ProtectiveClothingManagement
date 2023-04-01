namespace Pcm.Core.Entities;

public interface IPerson
{
    int PersonnelNumber { get; init; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string EmailAddress { get; set; }
}