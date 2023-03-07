using Pcm.Core.Entities;

namespace Pcm.Infrastructure.ResponseModels;

public interface IPersonResponseModel : IPerson
{
    int PersonnelNumber { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string EmailAddress { get; set; }
}