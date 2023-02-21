using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class PersonAutocomplete : ComponentBase
{
    private IEnumerable<Person>? _persons;

    [Parameter] public Person Value { get; set; }
    [Parameter] public EventCallback<Person> ValueChanged { get; set; }
    [Parameter] public bool Required { get; set; } = false;
    [Inject] public IRepository<IPerson, int> PersonRepository { get; set; }
    [Inject] public IRepository<IApprenticeship, int> ApprenticeshipRepository { get; set; }

    protected override async void OnInitialized()
    {
        _persons = await PersonRepository.GetAll() as IEnumerable<Person>;
        var apprenticeships = await ApprenticeshipRepository.GetAll() as IEnumerable<ApprenticeshipResponseModel>;
        if (apprenticeships == null) return;
        foreach (var person in _persons)
            person.Apprenticeship = apprenticeships.First(x => person.Apprenticeship.Id.Equals(x.Id));
    }

    private async Task<IEnumerable<Person>> SearchPersonAutocomplete(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
            return _persons;
        var searchWords = StringHandleController.CreateWordArray(searchString);
        var filteredPersons = FilterPersonsByWords(searchWords);
        return filteredPersons;
    }

    private IEnumerable<Person> FilterPersonsByWords(string[] searchWords)
    {
        var filtered = _persons;

        foreach (var word in searchWords)
        {
            var matches = filtered.Where(x =>
                x.FirstName.ToLower().Contains(word) ||
                x.LastName.ToLower().Contains(word) ||
                x.Id.ToString().Contains(word) ||
                x.Apprenticeship.Name.ToLower().Contains(word) ||
                x.EmailAddress.ToLower().Contains(word));
            filtered = matches.ToList();
        }

        return filtered.Distinct();
    }

    private async void OnValueChanged(Person newPerson)
    {
        Value = newPerson;
        await ValueChanged.InvokeAsync(Value);
    }
}