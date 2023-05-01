using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.Refactor.Models;

public class TrainingModel
{
    private readonly IRepository<TrainingResponse, TrainingRequest> _trainingRepository;
    private List<TrainingResponse> _trainings = new();
    
    public EventCallback<TrainingResponse> TrainingInsertRequestFinished { get; set; }
    
    public List<TrainingResponse> Trainings
    {
        get
        {
            if (_trainings.Count == 0)
            {
                _trainings = Task.Run(async () => await 
                    _trainingRepository.GetAll()).GetAwaiter().GetResult().ToList();
            }
            return _trainings;
        }
    }

    public TrainingModel(IRepository<TrainingResponse, TrainingRequest> trainingRepository)
    {
        _trainingRepository = trainingRepository;
    }

    public async Task AddTraining(TrainingRequest request)
    {
        var trainingResponse = await _trainingRepository.Insert(request);
        bool isSuccesful = trainingResponse.IsResponseSuccess;
        if (isSuccesful)
        {
            _trainings.Add(trainingResponse);
        }

        await TrainingInsertRequestFinished.InvokeAsync(trainingResponse);
    }
}