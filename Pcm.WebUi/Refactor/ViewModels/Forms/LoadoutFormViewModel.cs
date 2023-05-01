using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels.Forms;

public class LoadoutFormViewModel : FormViewModel
{
    public LoadOutPartRequest LoadoutRequest { get; } = new();
    public int SelectedCategoryId { get; set; } = 1;
    
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
        LoadoutRequest.Count = 1;
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
        StatusColor = Color.Warning;
        LoadoutRequest.Count = loadout.Count;
        LoadoutRequest.CategoryId = loadout.CategoryId;
        TempLoadoutId = loadout.Id;
        StatusText = "Ausstattung ändern?";
    }

    private async Task UpdateStatus(LoadOutPartResponse response)
    {
        if (response.IsResponseSuccess)
        {
            StatusText = $"Ausstattung mit ID {response.Id} {(TempLoadoutId > 0 ? "geändert" : "hinzugefügt")}.";
            StatusColor = Color.Success;
            TempLoadoutId = 0;
        }
        else
        {
            StatusText = $"Ausstattung konnte nicht {(TempLoadoutId > 0 ? "geändert" : "hinzugefügt")} werden.";
            StatusColor = Color.Error;
        }
        IsLoading = false;
    }
}