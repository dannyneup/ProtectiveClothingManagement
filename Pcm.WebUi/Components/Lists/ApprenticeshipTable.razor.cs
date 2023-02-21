using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.Components.Lists;

public partial class ApprenticeshipTable : ComponentBase
{
    private bool _anyPopoverIsOpen;
    private IEnumerable<ApprenticeshipResponseModel>? _apprenticeships = new List<ApprenticeshipResponseModel>();
    private bool _detailPopupIsOpen = false;
    private bool _newApprenticeshipPopOverIsOpen = false;
    private string? _searchString;
    private ApprenticeshipResponseModel _selectedApprenticeshipResponseModel = new();
    private bool _showDialog;

    [Inject] public IRepository<IApprenticeship, int> ApprenticeshipRepository { get; set; }
    [Inject] public IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _apprenticeships = await ApprenticeshipRepository.GetAll() as IEnumerable<ApprenticeshipResponseModel>;
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

    private void OnRowClick(TableRowClickEventArgs<ApprenticeshipResponseModel> rowClickEvent)
    {
        _detailPopupIsOpen = !_detailPopupIsOpen;
        _selectedApprenticeshipResponseModel = rowClickEvent.Item;
    }
}