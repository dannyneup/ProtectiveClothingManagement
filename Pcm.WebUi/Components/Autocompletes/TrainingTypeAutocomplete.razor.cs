using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class TrainingTypeAutocomplete : ComponentBase
{
    [Parameter] public string Value { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public bool Required { get; set; }
    [Inject] public IRepository<TrainingResponse, TrainingRequestModel> TrainingRepository { get; set; }

    private IEnumerable<TrainingResponse> _trainingResponses;
    private IEnumerable<string> _trainingTypes;

    protected override async void OnInitialized()
    {
        _trainingResponses = await TrainingRepository.GetAll();
        _trainingTypes = _trainingResponses.Select(x => x.Type).ToList().Distinct();
    }

    private async Task<IEnumerable<string>> SearchTrainingTypeAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return _trainingTypes;
        }
        return _trainingTypes.Where(x => x.Contains(searchString));
    }

    private async Task OnValueChanged(string newTrainingType)
    {
        Value = newTrainingType;
        await ValueChanged.InvokeAsync(Value);
    }
}