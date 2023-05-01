using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels.Tables;

public class TraineesTableViewModel
{
    private readonly PersonsModel _trainees;
    

    public TraineesTableViewModel(PersonsModel trainees)
    {
        _trainees = trainees;
    }

    public List<TraineeResponse> GetCorrespondingToTrainingId(int trainingId)
    {
        return _trainees.Trainees.Where(x => x.TrainingId == trainingId).ToList();
    }
}