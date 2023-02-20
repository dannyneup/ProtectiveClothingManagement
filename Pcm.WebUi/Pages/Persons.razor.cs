namespace Pcm.WebUi.Pages;

public partial class Persons
{
    private bool _newPersonPopOverIsOpen;
    private string? _searchString;


    private void ToggleNewPersonPopover()
    {
        _newPersonPopOverIsOpen = !_newPersonPopOverIsOpen;
    }
}