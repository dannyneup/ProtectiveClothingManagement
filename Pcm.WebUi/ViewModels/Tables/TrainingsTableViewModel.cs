using Pcm.Infrastructure.Models;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Services;

namespace Pcm.WebUi.ViewModels.Tables;

public class TrainingsTableViewModel
{
    public IEnumerable<TrainingResponse> Trainings => _trainingsModel.Trainings;
    public string SearchString { get; set; } = "";
    public Func<TrainingResponse, bool> Filter => x => SearchService.Search(x, SearchString);
    
    private readonly TrainingsModel _trainingsModel;
    

    public TrainingsTableViewModel(TrainingsModel trainingsModel)
    {
        _trainingsModel = trainingsModel;
    }

}