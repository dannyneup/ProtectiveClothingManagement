using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Core.Entities;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class PersonAutocomplete : ComponentBase
{
    private IEnumerable<PersonInfoResponseModel>? _persons;

    [Parameter] public PersonInfoResponseModel Value { get; set; }
    [Parameter] public EventCallback<PersonInfoResponseModel> ValueChanged { get; set; }
    [Parameter] public bool Required { get; set; } = false;
    [Inject] public IRepository<IPersonInfoResponseModel, IPersonInfoResponseModel> PersonInfoRepository { get; set; }

    protected override async void OnInitialized()
    {
        _persons = await PersonInfoRepository.GetAll() as IEnumerable<PersonInfoResponseModel>;
    }

    private async Task<IEnumerable<PersonInfoResponseModel>> SearchPersonAutocomplete(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
            return _persons;
        var searchWords = StringHandleController.CreateWordArray(searchString);
        var filteredPersons = FilterPersonsByWords(searchWords);
        return filteredPersons;
    }

    private IEnumerable<PersonInfoResponseModel> FilterPersonsByWords(string[] searchWords)
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

    private async Task OnValueChanged(PersonInfoResponseModel newPerson)
    {
        Value = newPerson;
        await ValueChanged.InvokeAsync(Value);
    }
}