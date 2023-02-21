using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components;

public partial class ApprenticeshipDetails : ComponentBase
{
    [Parameter] public Apprenticeship Apprenticeship { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public bool FixedHeader { get; set; } = true;
}