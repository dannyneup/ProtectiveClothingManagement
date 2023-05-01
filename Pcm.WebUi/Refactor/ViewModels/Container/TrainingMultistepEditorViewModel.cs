using Microsoft.AspNetCore.Components;
using Pcm.WebUi.Refactor.Enums;

namespace Pcm.WebUi.Refactor.ViewModels.Container;

public class TrainingMultistepEditorViewModel
{
    public TrainingEditStatus Status { get; private set; }
    public int AddedTrainingId { get; set; }
    private NavigationManager _navigationManager;

    public TrainingMultistepEditorViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public async Task TrainingEditingDone(int id)
    {
        if (id != 0)
        {
            AddedTrainingId = id;
            Status = TrainingEditStatus.AddLoadout;
        }
    }
    
    public async Task LoadoutEditingDone(int count)
    {
        if (count > 0)
        {
            Status = TrainingEditStatus.AddApprentices;
        }
    }
    
    public async Task TraineeEditionDone(int count)
    {
        if (count > 0)
        {
            Status = TrainingEditStatus.Done;
            _navigationManager.NavigateTo("/");
        }
    }
}