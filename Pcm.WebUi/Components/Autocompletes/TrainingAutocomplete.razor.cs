using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Models;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class TrainingAutocomplete : ComponentBase
{
    [Parameter] public List<Training> Trainings { get; set; } = Enumerable.Empty<Training>().ToList();
    [Parameter] public Training Training { get; set; } = new();
    [Parameter] public EventCallback<Training> TrainingChanged { get; set; }
    [Parameter] public bool Required { get; set; }

    private Task<IEnumerable<Training>> SearchAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return Task.FromResult<IEnumerable<Training>>(Trainings);
        return Task.FromResult(Trainings.Where(x => x.Name.Contains(searchString)));
    }

    private async Task OnValueChanged(Training training)
    {
        await TrainingChanged.InvokeAsync(training);
    }
}