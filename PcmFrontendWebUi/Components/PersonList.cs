using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcmFrontendWebUi.Models;
using PcmFrontendWebUi.Repositories;

namespace PcmFrontendWebUi.Components;

public partial class PersonList : ComponentBase
{
    [Inject] public IRepository<Person, int> PersonRepository { get; set; }
    private IEnumerable<Person> _persons = new List<Person>();
    private string _searchString = "";
    private bool _newPersonPopOverIsOpen = false;


    protected override async Task OnInitializedAsync()
    {
        _persons = await PersonRepository.GetAll();
    }

    private bool PersonFilter(Person person)
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;
        var properties = new List<string>();
        properties.Add(person.Id.ToString());
        properties.Add(person.FirstName);
        properties.Add(person.LastName);
        properties.Add(person.Apprenticeship.Name);
        properties.Add(person.EmailAddress);
        properties.RemoveAll(x => x == null);
        var matchedProperty = properties.FirstOrDefault(x => x.ToLower().Contains(_searchString.ToLower()));
        return matchedProperty != null;
    }

    private void ToggleNewPersonPopover()
    {
        _newPersonPopOverIsOpen = !_newPersonPopOverIsOpen;
    }
}