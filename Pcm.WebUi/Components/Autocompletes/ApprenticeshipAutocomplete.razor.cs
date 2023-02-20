using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;
using Pcm.WebUi.Controller;

namespace Pcm.WebUi.Components.Autocompletes;

public partial class ApprenticeshipAutocomplete : ComponentBase
{
    private IEnumerable<Apprenticeship>? _apprenticeships;

    [Parameter] public Apprenticeship Value { get; set; }
    [Parameter] public EventCallback<Apprenticeship> ValueChanged { get; set; }
    [Parameter] public bool Required { get; set; } = false;
    [Inject] public IRepository<IApprenticeship, int> ApprenticeshipRepository { get; set; }

    protected override async void OnInitialized()
    {
        _apprenticeships = await ApprenticeshipRepository.GetAll() as IEnumerable<Apprenticeship>;
    }

    private async Task<IEnumerable<Apprenticeship>> SearchApprenticeshipAutocomplete(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
            return _apprenticeships;
        var searchWords = StringHandleController.CreateWordArray(searchString);
        var filteredApprenticeships = FilterApprenticeshipsByWords(searchWords);
        return filteredApprenticeships;
    }

    private IEnumerable<Apprenticeship> FilterApprenticeshipsByWords(string[] searchWords)
    {
        var filtered = _apprenticeships;

        foreach (var word in searchWords)
        {
            var matches = filtered.Where(x =>
                x.Name.ToLower().Contains(word) ||
                x.Type.ToLower().Contains(word));
            filtered = matches.ToList();
        }

        return filtered.Distinct();
    }

    private async void OnValueChanged(Apprenticeship newApprenticeship)
    {
        Value = newApprenticeship;
        await ValueChanged.InvokeAsync(Value);
    }
}