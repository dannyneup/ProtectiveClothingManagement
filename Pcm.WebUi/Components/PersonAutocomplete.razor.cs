using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Models;
using Pcm.Infrastructure.Repositories;

namespace Pcm.WebUi.Components;

public partial class PersonAutocomplete : ComponentBase
{
    private IEnumerable<IPerson> _persons;

    [Parameter] public IPerson Value { get; set; }
    [Parameter] public EventCallback<IPerson> ValueChanged { get; set; }
    [Parameter] public bool Required { get; set; } = false;
    [Inject] public IRepository<IPerson, int> PersonRepository { get; set; }

    protected override async void OnInitialized()
    {
        var persons = await PersonRepository.GetAll();
        _persons = persons;
    }

    private async Task<IEnumerable<IPerson>> SearchPersonAutocomplete(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
            return _persons;
        var searchWords = StringHandleController.CreateWordArray(searchString);
        var filteredArticleTypes = filterPersonsByWords(searchWords);
        return filteredArticleTypes;
    }

    private IEnumerable<IPerson> filterPersonsByWords(string[] searchWords)
    {
        var filtered = _persons;
        
        foreach (var word in searchWords)
        {
            foreach (var person in _persons)
            {
                var aName = person.Apprenticeship.Name;
                var test = aName.Contains(word);
            }
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

    private async void OnValueChanged(IPerson newPerson)
    {
        Value = newPerson;
        await ValueChanged.InvokeAsync(Value);
    }
}