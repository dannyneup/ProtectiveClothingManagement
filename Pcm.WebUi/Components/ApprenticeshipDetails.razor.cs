using Microsoft.AspNetCore.Components;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.Components;

public partial class ApprenticeshipDetails : ComponentBase
{
    [Parameter] 
    public List<ApprenticeResponseModel> Apprentices { get; set; }
    
    [Parameter] 
    public string? Height { get; set; }
    
    [Parameter] 
    public bool FixedHeader { get; set; } = true;
}