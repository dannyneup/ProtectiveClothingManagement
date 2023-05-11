using Pcm.Infrastructure.Models;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.ViewModels.Tables;

public class LoadOutTableViewModel
{
    public IEnumerable<LoadOutPartResponse> Trainees => _loadOutsModel.Loadouts;
    
    private readonly LoadoutsModel _loadOutsModel;
    

    public LoadOutTableViewModel(LoadoutsModel loadoutsModel)
    {
        _loadOutsModel = loadoutsModel;
    }

    public List<LoadOutPartResponse> GetCorrespondingToTrainingId(int trainingId)
    {
        return _loadOutsModel.Loadouts.Where(x => x.TrainingId == trainingId).ToList();
    }
}