using Pcm.Infrastructure.RequestModels;

namespace Pcm.Infrastructure.ResponseModels;

public class LoadOutPartResponse : LoadOutPartRequest
{ 
    public string CategoryName { get; init; } = "";
    
}