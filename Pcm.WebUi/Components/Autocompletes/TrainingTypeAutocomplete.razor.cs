using Microsoft.AspNetCore.Components;
using Pcm.Application.Models;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class TrainingTypeAutocomplete : ComponentBase
{
    [Parameter]
    public List<string> TrainingTypes { get; set; } = new();
    [Parameter] public string TrainingType { get; set; } = "";
    [Parameter] public EventCallback<string> TrainingChanged { get; set; }
    [Parameter] public bool Required { get; set; }

    

    private Task<IEnumerable<string>> SearchAutocomplete(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return Task.FromResult<IEnumerable<string>>(TrainingTypes);
        return Task.FromResult(TrainingTypes.Where(x => x.Contains(searchString)));
    }

    private async Task OnValueChanged(string trainingType)
    {
        TrainingType = trainingType;
        await TrainingChanged.InvokeAsync(trainingType);
    }
}