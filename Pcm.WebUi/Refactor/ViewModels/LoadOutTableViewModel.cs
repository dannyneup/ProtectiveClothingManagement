using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels;

public class LoadOutTableViewModel
{
    private readonly LoadoutsModel _loadouts;
    

    public LoadOutTableViewModel(LoadoutsModel loadouts)
    {
        _loadouts = loadouts;
    }

    public List<LoadOutPartResponse> GetCorrespondingToTrainingId(int trainingId)
    {
        return _loadouts.Loadouts.Where(x => x.TrainingId == trainingId).ToList();
    }
}