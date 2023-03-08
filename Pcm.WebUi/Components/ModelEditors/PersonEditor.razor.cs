using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.ModelEditors;

public partial class PersonEditor : ComponentBase
{
    private TrainingResponse _trainingResponse;
    private string _emailAddress;
    private string _firstName;
    private string _lastName;

    [Parameter] public PersonRequest PersonRequest { get; set; }
    [Parameter] public EventCallback<PersonRequest> PersonInfoRequestChanged { get; set; }

    protected override void OnInitialized()
    {
        PersonRequest = new();
    }

    private async Task OnTrainingChanged(TrainingResponse trainingResponse)
    {
        
        PersonRequest.TrainingName = trainingResponse.Name;
        PersonRequest.TrainingType = trainingResponse.Type;
        await PersonInfoRequestChanged.InvokeAsync(PersonRequest);
    }

    private string EMailValidation(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            return null;
        }
        if (!MailValidationController.IsValidEmail(arg))
        {
            return Localization.emailNotValid;
        }
        return null;
    }
}