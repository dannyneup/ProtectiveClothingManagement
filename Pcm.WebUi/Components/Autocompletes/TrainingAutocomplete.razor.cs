using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Models;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class TrainingAutocomplete : ComponentBase
{
    [Parameter] public List<Training> Trainings { get; set; }
    [Parameter] public Training Training { get; set; }
    [Parameter] public EventCallback<Training> TrainingChanged { get; set; }
    [Parameter] public bool Required { get; set; }

    private async Task<IEnumerable<Training>> SearchAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return Trainings;
        return Trainings.Where(x => x.Name.Contains(searchString));
    }

    private async Task OnValueChanged(Training Training)
    {
        Training = Training;
        await TrainingChanged.InvokeAsync(Training);
    }
}