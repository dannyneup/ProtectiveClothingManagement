using Pcm.Infrastructure.RequestModels;

namespace Pcm.Infrastructure.ResponseModels;

public record LoadOutPartResponse : LoadOutPartRequest
{ 
    public int Id { get; init; }
    public string CategoryName { get; init; } = "";
    
}