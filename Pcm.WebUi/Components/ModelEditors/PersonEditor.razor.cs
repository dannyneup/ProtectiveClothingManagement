using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.ModelEditors;

public partial class PersonEditor : ComponentBase
{
    private TrainingResponseModel? _trainingResponse;
    private string _emailAddress;
    private string _firstName;
    private string _lastName;

    [Parameter] public PersonInfoRequestModel PersonInfoRequest { get; set; }
    [Parameter] public EventCallback<PersonInfoRequestModel> PersonInfoRequestChanged { get; set; }

    protected override void OnInitialized()
    {
        PersonInfoRequest = new();
    }

    private async Task OnTrainingChanged(TrainingResponseModel trainingResponse)
    {
        
        PersonInfoRequest.TrainingName = trainingResponse.Name;
        PersonInfoRequest.TrainingType = trainingResponse.Type;
        await PersonInfoRequestChanged.InvokeAsync(PersonInfoRequest);
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