using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Application.Interfaces;
using Pcm.WebUi.Models;
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

    private const string _height = "30vw";

    [Parameter] public List<Person> Persons { get; set; }
    [Parameter] public List<LoadOutPart> LoadOut { get; set; }
    [Parameter] public bool FixedHeader { get; set; } = true;
}