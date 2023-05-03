using MudBlazor;

namespace Pcm.WebUi.Services;

public class DialogService<T> where T : class
{
    private readonly IDialogService _service;

    public DialogService(IDialogService service)
    {
        _service = service;
    }

    public async Task Create()
    {
    }

    public async Task<IDialogReference> Test()
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", "Do you really want to delete these records? This process cannot be undone.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);
        var options = new DialogOptions {CloseButton = true, MaxWidth = MaxWidth.ExtraSmall};
        var task = await _service.ShowAsync(typeof(T), "", parameters, options);
        return task;
    }
}