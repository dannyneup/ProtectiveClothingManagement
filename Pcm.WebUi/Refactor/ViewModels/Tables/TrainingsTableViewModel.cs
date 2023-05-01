using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels.Tables;

public class TrainingsTableViewModel
{
    public List<TrainingResponse> Trainings => _trainings.Trainings;
    private readonly TrainingModel _trainings;
    

    public TrainingsTableViewModel(TrainingModel trainings)
    {
        _trainings = trainings;
    }
}