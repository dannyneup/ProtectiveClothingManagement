using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components.Lists;

public partial class ApprenticeshipList : ComponentBase
{
    private bool _anyPopoverIsOpen;
    private IEnumerable<Apprenticeship>? _apprenticeships = new List<Apprenticeship>();
    private bool _detailPopupIsOpen = false;
    private bool _newApprenticeshipPopOverIsOpen = false;
    private string? _searchString;
    private Apprenticeship _selectedApprenticeship = new();
    private bool _showDialog;

    [Inject] public IRepository<IApprenticeship, int> ApprenticeshipRepository { get; set; }
    [Inject] public IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _apprenticeships = await ApprenticeshipRepository.GetAll() as IEnumerable<Apprenticeship>;
    }

    private bool ApprenticeshipFilter(IApprenticeship apprenticeship)
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;
        var properties = new List<string>();
        properties.Add(apprenticeship.Name);
        properties.RemoveAll(x => string.IsNullOrEmpty(x));
        var matchedProperty = properties.FirstOrDefault(x => x.ToLower().Contains(_searchString.ToLower()));
        return matchedProperty != null;
    }

    private void OnRowClick(TableRowClickEventArgs<Apprenticeship> rowClickEvent)
    {
        _detailPopupIsOpen = !_detailPopupIsOpen;
        _selectedApprenticeship = rowClickEvent.Item;
    }
}