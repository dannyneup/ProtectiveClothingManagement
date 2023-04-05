using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Models;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Components.Dialogs;

public partial class TrainingDetailsDialog
{
    private readonly TableGroupDefinition<Person> _personInfosGroupDefinition = new()
    {
        GroupName = Localization.startingYear,
        Indentation = false,
        Expandable = true,
        IsInitiallyExpanded = false,
        Selector = e => e.YearStarted
    };

    private const string Height = "30vw";

    [Parameter, EditorRequired] public List<Person> Persons { get; set; } = Enumerable.Empty<Person>().ToList();
    [Parameter, EditorRequired] public List<LoadOutPart> LoadOut { get; set; } = Enumerable.Empty<LoadOutPart>().ToList();
    [Parameter] public bool FixedHeader { get; set; } = true;
}