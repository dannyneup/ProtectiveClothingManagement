using System.ComponentModel.DataAnnotations;

namespace Pcm.Infrastructure.ResponseModels;

public class PersonResponseModel : ResponseBase, IPersonResponseModel
{
    public int PersonnelNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
}