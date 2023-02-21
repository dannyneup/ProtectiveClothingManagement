namespace Pcm.Core.Entities;

public interface IApprenticeship : IResponseBase
{
    int Id { get; init; }
    string Name { get; init;}
    string Type { get; init;}
}