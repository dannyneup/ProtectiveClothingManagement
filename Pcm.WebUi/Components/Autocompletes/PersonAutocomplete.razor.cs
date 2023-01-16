using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Autocompletes
{
    public partial class PersonAutocomplete : ComponentBase
    {
        private IEnumerable<Person> _persons;

        [Parameter] public Person Value { get; set; }
        [Parameter] public EventCallback<Person> ValueChanged { get; set; }
        [Parameter] public bool Required { get; set; } = false;
        [Inject] public IRepository<IPerson, int> PersonRepository { get; set; }

        protected override async void OnInitialized()
        {
            var persons = await PersonRepository.GetAll();
            _persons = persons as IEnumerable<Person>;
        }

        private async Task<IEnumerable<Person>> SearchPersonAutocomplete(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return _persons as IEnumerable<Person>;
            var searchWords = StringHandleController.CreateWordArray(searchString);
            var filteredArticleTypes = filterPersonsByWords(searchWords);
            return filteredArticleTypes;
        }

        private IEnumerable<Person> filterPersonsByWords(string[] searchWords)
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

        private async void OnValueChanged(Person newPerson)
        {
            Value = newPerson;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}