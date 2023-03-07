using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Lists;

public partial class PersonList : ComponentBase
{
    private IEnumerable<PersonInfoResponseModel> _persons;
    [Parameter] public string? SearchString { get; set; }
    [Parameter] public int Elevation { get; set; }
    [Inject] public IRepository<IPersonInfoResponseModel, IPersonInfoRequestModel> PersonInfoRepository { get; set; }


    protected override async Task OnInitializedAsync()
    {
        _persons = await PersonInfoRepository.GetAll() as IEnumerable<PersonInfoResponseModel>;
    }

    private bool PersonFilter(IPersonInfoResponseModel person)
    {
        return ListItemFilterController<IPersonInfoResponseModel>.CheckIfStringMatchesProperties(person, SearchString);
    }
}