using Pcm.Core;
using Pcm.Core.Entities;

namespace Pcm.WebUi.Models;

public class Person : ResponseBase, IPerson
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IApprenticeship Apprenticeship { get; set; }
    public string EmailAddress { get; set; }

    public override string ToString()
    {
        return $"{this.FirstName} {this.LastName} ({this.Id})";
    }
}