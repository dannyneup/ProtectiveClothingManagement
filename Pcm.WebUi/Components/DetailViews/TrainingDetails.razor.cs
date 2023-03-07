using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.DetailViews;

//TODO: css isolation not working
public partial class TrainingDetails : ComponentBase
{
    [Parameter] public int TrainingId { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public bool FixedHeader { get; set; } = true;
    
    [Inject] public ITrainingRepository TrainingRepository { get; set; }

    private IEnumerable<PersonInfoResponseModel> _persons;
    private IEnumerable<LoadOutPartResponseModel> _loadOut;

    protected override async Task OnInitializedAsync()
    {
        _persons = await TrainingRepository.GetTrainees(TrainingId) as IEnumerable<PersonInfoResponseModel>;
        _loadOut = await TrainingRepository.GetLoadOut(TrainingId) as IEnumerable<LoadOutPartResponseModel>;
    }
    
    private TableGroupDefinition<PersonInfoResponseModel> _personInfosGroupDefinition = new()
    {
        GroupName = Localization.group,
        Indentation = false,
        Expandable = true,
        IsInitiallyExpanded = true,
        Selector = (e) => e.YearStarted
    };
}