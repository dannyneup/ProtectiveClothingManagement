using System.Text.Json.Serialization;
using Pcm.Core.Entities;
using Pcm.Infrastructure.JsonConverters;

namespace Pcm.Infrastructure.Entities;

public class Person : ResponseBase, IPerson
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonConverter(typeof(ConcreteTypeConverter<Apprenticeship, IApprenticeship>))]
    public IApprenticeship? Apprenticeship { get; set; }

    public string? EmailAddress { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} ({Id})";
    }
}