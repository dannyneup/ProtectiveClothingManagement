using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Refactor.ViewModels;

namespace Pcm.WebUi.Refactor.Views;

public partial class ApprenticesForm
{
    [Parameter] 
    public bool IsEnabled { get; set; }
}