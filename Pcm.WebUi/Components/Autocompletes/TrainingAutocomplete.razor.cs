using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class TrainingAutocomplete : ComponentBase
{
    private IEnumerable<TrainingResponse> _trainingResponses;
    private MudAutocomplete<TrainingResponse> _autocomplete;

    [Parameter] public TrainingResponse Value { get; set; }
    [Parameter] public EventCallback<TrainingResponse> ValueChanged { get; set; }
    [Parameter] public bool Required { get; set; }
    [Inject] public IRepository<TrainingResponse, TrainingRequestModel> TrainingRepository { get; set; }

    protected override async void OnInitialized()
    {
        _trainingResponses = await TrainingRepository.GetAll();
    }

    private async Task<IEnumerable<TrainingResponse>> SearchTrainingAutocomplete(string searchString)
    {
        return ListItemFilterController<TrainingResponse>.FilterByWords(_trainingResponses, searchString);
    }

    private async Task OnValueChanged(TrainingResponse newTrainingResponse)
    {
        Value = newTrainingResponse;
        await ValueChanged.InvokeAsync(Value);
    }
}