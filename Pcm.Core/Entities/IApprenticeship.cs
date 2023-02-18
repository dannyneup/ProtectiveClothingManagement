namespace Pcm.Core.Entities;

public interface IApprenticeship : IResponseBase
{
    int Id { get; set; }
    string Name { get; set; }
    string Type { get; set; }
    IEnumerable<IPerson>? Apprentices { get; set; }
}