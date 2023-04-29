using Pcm.Application.Models;

namespace Pcm.WebUi.CustomEventArgs;

public class CreateNewTrainingEventArgs : EventArgs
{
    public List<ItemCategory> ItemCategories = default!;
    public List<Training> Trainings = default!;
    public Training Result = new Training();
}