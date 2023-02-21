using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.Infrastructure.ResponseModels;

public class ApprenticeshipResponseModel : ResponseBase, IApprenticeship
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string Type { get; init; }
    public List<ApprenticeResponseModel> Apprentices { get; init; } = new();
    public List<LoadOutResponseModel> DefaultLoadOuts { get; init;} = new();
}