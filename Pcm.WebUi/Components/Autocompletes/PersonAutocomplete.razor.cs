using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class PersonAutocomplete : ComponentBase
{
    private IEnumerable<PersonResponse>? _persons;

    [Parameter] public PersonResponse Value { get; set; }
    [Parameter] public EventCallback<PersonResponse> ValueChanged { get; set; }
    [Parameter] public bool Required { get; set; } = false;
    [Inject] public IRepository<PersonResponse, PersonRequest> PersonInfoRepository { get; set; }

    protected override async void OnInitialized()
    {
        _persons = await PersonInfoRepository.GetAll();
    }

    private async Task<IEnumerable<PersonResponse>> SearchPersonAutocomplete(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
            return _persons;
        var searchWords = StringHandleController.CreateWordArray(searchString);
        var filteredPersons = FilterPersonsByWords(searchWords);
        return filteredPersons;
    }

    private IEnumerable<PersonResponse> FilterPersonsByWords(string[] searchWords)
    {
        var filtered = _persons;

        foreach (var word in searchWords)
        {
            var matches = filtered.Where(x =>
                x.FirstName.ToLower().Contains(word) ||
                x.LastName.ToLower().Contains(word) ||
                x.PersonnelNumber.ToString().Contains(word) ||
                x.TrainingName.ToLower().Contains(word) ||
                x.TrainingType.ToLower().Contains(word) ||
                x.EmailAddress.ToLower().Contains(word));
            filtered = matches.ToList();
        }

        return filtered.Distinct();
    }

    private async Task OnValueChanged(PersonResponse newPerson)
    {
        Value = newPerson;
        await ValueChanged.InvokeAsync(Value);
    }
}