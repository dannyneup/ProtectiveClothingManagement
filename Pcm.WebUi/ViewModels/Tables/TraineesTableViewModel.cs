using Pcm.Infrastructure.Models;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Services;

namespace Pcm.WebUi.ViewModels.Tables;

public class TraineesTableViewModel
{
    public IEnumerable<TraineeResponse> Trainees
    {
        get
        {
            if (TrainingId == null)
            {
                return _traineesModel.Trainees;
            }

            return _traineesModel.Trainees.Where(x => x.TrainingId == TrainingId);
        }
    }

    public int TrainingId { get; set; }

    public Func<TraineeResponse, bool> Filter => x => SearchService.Search(x, SearchString);
    public string SearchString { get; set; } = "";
    private readonly TraineesModel _traineesModel;
    
    public TraineesTableViewModel(TraineesModel traineesModel)
    {
        _traineesModel = traineesModel;
    }

    public List<TraineeResponse> GetCorrespondingToTrainingId(int trainingId)
    {
        return _traineesModel.Trainees.Where(x => x.TrainingId == trainingId).ToList();
    }
}