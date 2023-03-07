using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Application.Interfaces.RequestModels;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Core.Entities;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class TrainingAutocomplete : ComponentBase
{
    private IEnumerable<TrainingResponseModel>? _trainingResponses;
    private MudAutocomplete<TrainingResponseModel> _autocomplete;

    [Parameter] public TrainingResponseModel Value { get; set; }
    [Parameter] public EventCallback<TrainingResponseModel> ValueChanged { get; set; }
    [Parameter] public bool Required { get; set; }
    [Inject] public ITrainingRepository TrainingRepository { get; set; }

    protected override async void OnInitialized()
    {
        _trainingResponses = await TrainingRepository.GetAll() as IEnumerable<TrainingResponseModel>;
    }

    private async Task<IEnumerable<TrainingResponseModel>> SearchTrainingAutocomplete(string searchString)
    {
        return ListItemFilterController<TrainingResponseModel>.FilterByWords(_trainingResponses, searchString);
    }

    private async Task OnValueChanged(TrainingResponseModel? newTrainingResponse)
    {
        Value = newTrainingResponse;
        await ValueChanged.InvokeAsync(Value);
    }
}