using Microsoft.AspNetCore.Components;
using PcmFrontendWebUi.Models;
using PcmFrontendWebUi.Repositories;

namespace PcmFrontendWebUi.Components;

public partial class NewPerson : ComponentBase
{
    [Inject] public IRepository<Apprenticeship, int> ApprenticeshipRepository { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string ApprenticeshipName { get; set; }

    private IEnumerable<Apprenticeship> _apprenticeships;
    private bool _processing = false;

    private async Task<IEnumerable<string>> SerachApprenticeshipAutocomplete(string value)
    {
        _apprenticeships = await ApprenticeshipRepository.GetAll();
        
        var apprenticeshipNames = _apprenticeships.Select(o => o.Name).ToList();
        if (string.IsNullOrEmpty(value))
            return apprenticeshipNames;
        return apprenticeshipNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }
    
    async Task SaveNewPerson()
    {
        _processing = true;
        await Task.Delay(2000);
        _processing = false;
    }
}