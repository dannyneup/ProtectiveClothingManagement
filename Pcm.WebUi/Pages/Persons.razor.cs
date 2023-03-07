using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.WebUi.Components.Dialogs.ModelEditors;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Pages;

public partial class Persons
{
    [Inject] private IDialogService DialogService { get; set; }
    
    private bool _newPersonPopOverIsOpen;
    private string? _searchString;


    private void OpenNewPersonPopover()
    {
        DialogService.Show<PersonEditorDialog>(String.Format(Localization.createNewT, Localization.person));
    }
}