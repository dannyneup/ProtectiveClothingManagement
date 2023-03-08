using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Lists;

public partial class PersonList : ComponentBase
{
    private IEnumerable<PersonResponse> _persons;
    [Parameter] public string? SearchString { get; set; }
    [Parameter] public int Elevation { get; set; }
    [Inject] public IRepository<PersonResponse, PersonRequest> PersonInfoRepository { get; set; }


    protected override async Task OnInitializedAsync()
    {
        _persons = await PersonInfoRepository.GetAll();
    }

    private bool PersonFilter(PersonResponse person)
    {
        return ListItemFilterController<PersonResponse>.CheckIfStringMatchesProperties(person, SearchString);
    }
}