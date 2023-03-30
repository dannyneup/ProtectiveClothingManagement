using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Models;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class TrainingTypeAutocomplete : ComponentBase
{
    private List<string> _trainingTypes;
    [Parameter] public List<Training> Trainings { get; set; }
    [Parameter] public string TrainingType { get; set; }
    [Parameter] public EventCallback<string> TrainingChanged { get; set; }
    [Parameter] public bool Required { get; set; }

    protected override void OnInitialized()
    {
        _trainingTypes = Trainings.Select(x => x.Type).Distinct().ToList();
    }

    private async Task<IEnumerable<string>> SearchAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return _trainingTypes;
        return _trainingTypes.Where(x => x.Contains(searchString));
    }

    private async Task OnValueChanged(string trainingType)
    {
        TrainingType = trainingType;
        await TrainingChanged.InvokeAsync(trainingType);
    }
}