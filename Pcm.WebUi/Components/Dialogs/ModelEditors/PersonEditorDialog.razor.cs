using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Dialogs.ModelEditors;

public partial class PersonEditorDialog : ComponentBase
{
    private PersonRequest _personRequest = new();

    [Inject] public IRepository<PersonResponse, PersonRequest> PersonInfoRepository { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }

    private async Task SaveNewPerson()
    {
        var response = await PersonInfoRepository.Insert(_personRequest);
        var personName = $"{response.FirstName} {response.LastName}";
        Snackbar.Add($"{String.Format(Localization.TWithNameSuccessfullyCreated, Localization.person, personName)}", Severity.Success);
    }
}