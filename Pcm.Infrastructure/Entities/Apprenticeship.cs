using System.Text.Json.Serialization;
using Pcm.Core.Entities;
using Pcm.Infrastructure.JsonConverters;

namespace Pcm.Infrastructure.Entities;

public class Apprenticeship : ResponseBase, IApprenticeship
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    [JsonConverter(typeof(ConcreteTypeConverter<List<Person>, IEnumerable<IPerson>>))]
    public IEnumerable<IPerson> Apprentices { get; set; }
}