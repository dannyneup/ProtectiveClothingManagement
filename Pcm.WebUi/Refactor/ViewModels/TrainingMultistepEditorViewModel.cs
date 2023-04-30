using Pcm.WebUi.Refactor.Enums;
using Pcm.WebUi.Refactor.Models;

namespace Pcm.WebUi.Refactor.ViewModels;

public class TrainingMultistepEditorViewModel
{
    public TrainingEditStatus Status { get; private set; }
    public int AddedTrainingId { get; private set; }
    private readonly DataModel _dataModel;

    public TrainingMultistepEditorViewModel(DataModel dataModel)
    {
        _dataModel = dataModel;
        //_trainingModel.InsertRequestFinished = EventCallback.Factory
          //  .Create<TrainingResponse>(this, async (x) 
            //    => await NewTrainingAdded(x));
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
        if (count != 0)
        {
            Status = TrainingEditStatus.AddApprentices;
        }
    }
}