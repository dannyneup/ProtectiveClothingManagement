using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.WebUi.Models;

namespace Pcm.WebUi.Components.Dialogs;

public partial class TrainingEditorDialog : ComponentBase
{
    private MudForm _form;
    private MudTable<LoadOutPart> _table;

    private string _searchString = "";
    
    private bool _formIsValid;
    private bool _activeRowEdit;
    private bool _isAddingLoadOutPart;
    private bool _noItemCategoriesRemaining;
    
    private LoadOutPart? _loadOutPartBeforeEdit;

    private List<ItemCategory> _remainingCategories;
    private List<ItemCategory> _availableItemCategories;

    [Parameter]
    public Training Training { get; set; } = new()
    {
        LoadOut = new List<LoadOutPart>()
    };

    [Parameter] public List<Training> Trainings { get; set; }
    [Parameter] public List<ItemCategory> ItemCategories { get; set; }
    [Parameter] public bool EditMode { get; set; }

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (EditMode)
        {
            CategoriesChanged();
            await _form.Validate();
        }
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void Submit()
    {
        MudDialog.Close(DialogResult.Ok(Training));
    }

    private async Task OnAddLoadOutPart()
    {
        var newItem = new LoadOutPart();
        Training.LoadOut.Add(newItem);
        await Task.Delay(5);
        _table.SetEditingItem(newItem);

        _activeRowEdit = true;
        _isAddingLoadOutPart = true;
        CategoriesChanged();
    }

    private void OnRowEdit(object o)
    {
        _loadOutPartBeforeEdit = new LoadOutPart
        {
            CategoryName = ((LoadOutPart) o).CategoryName,
            Count = ((LoadOutPart) o).Count
        };
        _activeRowEdit = true;
        StateHasChanged();
    }

    private void OnRowEditCancel(object o)
    {
        if (_loadOutPartBeforeEdit == null)
            RemoveLoadOutPart((LoadOutPart) o);
        else
            RestoreCategory();

        _activeRowEdit = false;
        _isAddingLoadOutPart = false;
        StateHasChanged();

        void RestoreCategory()
        {
            ((LoadOutPart) o).CategoryName = _loadOutPartBeforeEdit.CategoryName;
            ((LoadOutPart) o).Count = _loadOutPartBeforeEdit.Count;
        }
    }

    private void RemoveLoadOutPart(LoadOutPart loadOutPart)
    {
        Training.LoadOut.Remove(loadOutPart);
        CategoriesChanged();
        StateHasChanged();
    }

    private void OnRowEditSubmit(object o)
    {
        var loadOutPart = (LoadOutPart) o;
        if (loadOutPart.CategoryId == 0 || loadOutPart.Count == 0) RemoveLoadOutPart(loadOutPart);

        _activeRowEdit = false;
        _isAddingLoadOutPart = false;
        CategoriesChanged();
        StateHasChanged();
    }

    private void CategoriesChanged()
    {
        _remainingCategories = GetRemainingCategories();
        _noItemCategoriesRemaining = NoCategoriesRemaining();
    }

    private bool NoCategoriesRemaining()
    {
        return _remainingCategories.Count == 0;
    }

    private List<ItemCategory> GetRemainingCategories()
    {
        var used = Training.LoadOut.Select(x => new ItemCategory
            {
                Id = x.CategoryId,
                Name = x.CategoryName
            }).Distinct()
            .ToList();
        var remaining = ItemCategories.Except(used).ToList();
        return remaining;
    }

    private void OnCategoryChanged(ItemCategory? itemCategory, LoadOutPart context)
    {
        if (itemCategory == null) return;
        context.CategoryId = itemCategory.Id;
        context.CategoryName = itemCategory.Name;
    }

    private void OnTrainingTypeChanged(string obj)
    {
        Training.Type = obj;
    }
}