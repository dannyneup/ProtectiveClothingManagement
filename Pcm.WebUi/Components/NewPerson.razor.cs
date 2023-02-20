using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components;

public partial class NewPerson : ComponentBase
{
    private Apprenticeship _apprenticeship;
    private IEnumerable<Apprenticeship> _apprenticeships;
    private string _emailAddress;
    private string _firstName;
    private string _lastName;
    private bool _processing = false;
    [Inject] public IRepository<IApprenticeship, int> ApprenticeshipRepository { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _apprenticeships = await ApprenticeshipRepository.GetAll() as IEnumerable<Apprenticeship>;
    }

    private async Task SaveNewPerson()
    {
        _processing = true;
        await Task.Delay(2000);
        _processing = false;
    }
}