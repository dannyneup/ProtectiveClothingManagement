
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels;

public class LoadoutFormViewModel
{
    public LoadOutPartRequest LoadoutRequest { get; } = new();
    public int SelectedCategoryId { get; set; }
    public int CountAdded { get; private set; }
    public bool IsLoading { get; private set; }
    public string StatusText { get; private set; }
    public Color StatusColor { get; private set; } = Color.Info;
    public List<ItemCategoryResponse> AvailableCategories => _dataModel.ItemCategories;
    
    private readonly DataModel _dataModel;

    public LoadoutFormViewModel(DataModel dataModel)
    {
        _dataModel = dataModel;
        _dataModel.LoadoutInsertRequestFinished = EventCallback.Factory
            .Create<LoadOutPartResponse>(this, async (x) 
                => await NewLoadoutAdded(x));
        StatusText = "Alle Felder sind erforderlich!";
    }

    public async Task AddNewLoadout()
    {
        IsLoading = true;
        StatusColor = Color.Info;
        StatusText = "Neue Ausstattung wird der Datenbank hinzugefügt";
        LoadoutRequest.CategoryId = SelectedCategoryId;
        await _dataModel.AddLoadout(LoadoutRequest);
    }

    private async Task NewLoadoutAdded(LoadOutPartResponse response)
    {
        if (response.IsResponseSuccess)
        {
            CountAdded++;
            StatusText = $"Ausstattung mit ID {response.Id} hinzugefügt.";
            StatusColor = Color.Success;
        }
        else
        {
            StatusText = "Ausstattung konnte nicht hinzugefügt werden.";
            StatusColor = Color.Error;
        }
        IsLoading = false;

    }
    
    
    
    
    
}