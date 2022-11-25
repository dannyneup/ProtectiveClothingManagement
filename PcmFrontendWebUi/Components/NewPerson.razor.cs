using Microsoft.AspNetCore.Components;
using PcmFrontendWebUi.Models;
using PcmFrontendWebUi.Repositories;

namespace PcmFrontendWebUi.Components;

public partial class NewPerson : ComponentBase
{
    private string _apprenticeshipName;
    private List<string> _apprenticeshipNames;
    private string _emailAddress;
    private string _firstName;
    private string _lastName;
    private bool _processing = false;
    [Inject] public IRepository<Apprenticeship, int> ApprenticeshipRepository { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var apprenticeships = await ApprenticeshipRepository.GetAll();
        _apprenticeshipNames = apprenticeships.Select(o => o.Name).ToList();
    }

    private Task<IEnumerable<string>> SearchApprenticeshipAutocomplete(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Task.FromResult<IEnumerable<string>>(_apprenticeshipNames);
        var filtered = _apprenticeshipNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(filtered);
    }

    private async Task SaveNewPerson()
    {
        _processing = true;
        await Task.Delay(2000);
        _processing = false;
    }
}