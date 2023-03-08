using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.DetailViews;

//TODO: css isolation not working
public partial class TrainingDetails : ComponentBase
{
    [Parameter] public int TrainingId { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public bool FixedHeader { get; set; } = true;
    
    [Inject] public IRepository<PersonResponse, PersonRequest> PersonRepository { get; set; }
    [Inject] public IRepository<LoadOutPartResponse, LoadOutPartRequest> LoadOutRepository { get; set; }

    private IEnumerable<PersonResponse> _persons;
    private IEnumerable<LoadOutPartResponse> _loadOut;

    protected override async Task OnInitializedAsync()
    {
        Dictionary<string, string>? query = new()
        {
            {"extended", "true"},
            {"training-id", TrainingId.ToString()}
        };
        _persons = await PersonRepository.GetAll(query);
        _loadOut = await LoadOutRepository.GetAll(query);
    }
    
    private readonly TableGroupDefinition<PersonResponse> _personInfosGroupDefinition = new()
    {
        GroupName = Localization.group,
        Indentation = false,
        Expandable = true,
        IsInitiallyExpanded = true,
        Selector = (e) => e.YearStarted
    };
}