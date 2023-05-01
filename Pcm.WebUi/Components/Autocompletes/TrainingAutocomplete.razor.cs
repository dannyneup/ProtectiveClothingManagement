using Microsoft.AspNetCore.Components;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class TrainingAutocomplete : ComponentBase
{
    [Parameter, EditorRequired] public List<TrainingResponse> Trainings { get; set; } = Enumerable.Empty<TrainingResponse>().ToList();
    [Parameter] public TrainingResponse Training { get; set; } = new();
    [Parameter] public EventCallback<TrainingResponse> TrainingChanged { get; set; }
    [Parameter] public bool Required { get; set; }

    private Task<IEnumerable<TrainingResponse>> SearchAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return Task.FromResult<IEnumerable<TrainingResponse>>(Trainings);
        return Task.FromResult(Trainings.Where(x => x.Title.Contains(searchString)));
    }

    private async Task OnValueChanged(TrainingResponse training)
    {
        await TrainingChanged.InvokeAsync(training);
    }
}