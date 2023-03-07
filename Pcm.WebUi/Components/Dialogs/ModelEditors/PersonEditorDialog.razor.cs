using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.RequestModels;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Dialogs.ModelEditors;

public partial class PersonEditorDialog : ComponentBase
{
    private PersonInfoRequestModel _personInfoRequest = new();

    [Inject] public IRepository<IPersonInfoResponseModel, IPersonInfoRequestModel> PersonInfoRepository { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }

    private async Task SaveNewPerson()
    {
        var response = await PersonInfoRepository.Insert(_personInfoRequest);
        var personName = $"{response.FirstName} {response.LastName}";
        Snackbar.Add($"{String.Format(Localization.TWithNameSuccessfullyCreated, Localization.person, personName)}", Severity.Success);
    }
}