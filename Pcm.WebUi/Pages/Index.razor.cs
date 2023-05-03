using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.Models;

namespace Pcm.WebUi.Pages;

public partial class Index
{
    [Inject] public TrainingsModel TrainingsModel { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }
}