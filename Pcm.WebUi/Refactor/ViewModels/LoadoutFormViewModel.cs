
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels;

public class LoadoutFormViewModel : FormViewModel
{
    public LoadOutPartRequest LoadoutRequest { get; } = new();
    public int SelectedCategoryId { get; set; }
    public int CountAdded { get; private set; }
    
    public List<ItemCategoryResponse> AvailableCategories => _loadoutsModel.ItemCategories;
    
    private readonly LoadoutsModel _loadoutsModel;
    public int TempLoadoutId { get; private set; }

    public LoadoutFormViewModel(LoadoutsModel loadoutsModel)
    {
        _loadoutsModel = loadoutsModel;
        _loadoutsModel.LoadoutRequestFinished = EventCallback.Factory
            .Create<LoadOutPartResponse>(this, async (x) 
                => await UpdateStatus(x));
        StatusText = "Alle Felder sind erforderlich!";
    }

    public async Task AddNewLoadout()
    {
        IsLoading = true;
        StatusColor = Color.Info;
        StatusText = "Neue Ausstattung wird hinzugefügt";
        LoadoutRequest.CategoryId = SelectedCategoryId;
        await _loadoutsModel.AddLoadout(LoadoutRequest);
    }
    
    public async Task UpdateLoadout()
    {
        IsLoading = true;
        StatusColor = Color.Info;
        StatusText = "Ausstattung wird aktualisiert";
        await _loadoutsModel.UpdateLoadout(LoadoutRequest, TempLoadoutId);
    }

    public void UpdateRequestModel(LoadOutPartResponse loadout)
    {
        LoadoutRequest.Count = loadout.Count;
        LoadoutRequest.CategoryId = loadout.CategoryId;
        TempLoadoutId = loadout.Id;
        StatusColor = Color.Info;
        StatusText = "Ausstattung ändern?";
    }
    

    private async Task UpdateStatus(LoadOutPartResponse response)
    {
        if (response.IsResponseSuccess)
        {
            CountAdded++;
            LoadoutRequest.Count = 1;
            LoadoutRequest.CategoryId = 0;
            StatusText = $"Ausstattung mit ID {response.Id} hinzugefügt.";
            StatusColor = Color.Success;
            TempLoadoutId = 0;
        }
        else
        {
            StatusText = "Ausstattung konnte nicht hinzugefügt werden.";
            StatusColor = Color.Error;
        }
        IsLoading = false;
    }
}