using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Pcm.WebUi.Services;

public class NotificationService
{
    [Inject] public ISnackbar Snackbar { get; set; } = default!;

}