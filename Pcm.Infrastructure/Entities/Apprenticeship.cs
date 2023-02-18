using Pcm.Core;
using Pcm.Core.Entities;

namespace Pcm.Infrastructure.Entities;

public class Apprenticeship : ResponseBase, IApprenticeship
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public IEnumerable<IPerson>? Apprentices { get; set; }
}