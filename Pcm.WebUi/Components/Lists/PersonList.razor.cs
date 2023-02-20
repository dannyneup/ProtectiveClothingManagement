using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components.Lists;

public partial class PersonList : ComponentBase
{
    private IEnumerable<Person>? _persons = new List<Person>();
    [Parameter] public string? SearchString { get; set; }
    [Parameter] public int Elevation { get; set; } = 0;
    [Inject] public IRepository<IPerson, int> PersonRepository { get; set; }


    protected override async Task OnInitializedAsync()
    {
        _persons = await PersonRepository.GetAll() as IEnumerable<Person>;
        
    }

    private bool PersonFilter(IPerson person)
    {
        if (string.IsNullOrWhiteSpace(SearchString))
            return true;
        var properties = new List<string>();
        properties.Add(person.Id.ToString());
        properties.Add(person.FirstName);
        properties.Add(person.LastName);
        properties.Add(person.Apprenticeship.Name);
        properties.Add(person.EmailAddress);
        properties.RemoveAll(x => x == null);
        var matchedProperty = properties.FirstOrDefault(x => x.ToLower().Contains(SearchString.ToLower()));
        return matchedProperty != null;
    }
}