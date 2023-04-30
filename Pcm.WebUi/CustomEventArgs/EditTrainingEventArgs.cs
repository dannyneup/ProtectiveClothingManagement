using Pcm.Application.Models;

namespace Pcm.WebUi.CustomEventArgs;

public class EditTrainingEventArgs : EventArgs
{
    public Training Training { get; set; } = default!;
    public List<Training> Trainings { get; set; } = default!;
    public List<ItemCategory> ItemCategories { get; set; } = default!;
}