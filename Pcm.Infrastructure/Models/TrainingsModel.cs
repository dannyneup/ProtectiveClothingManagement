using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.Models;

public class TrainingsModel
{
    private readonly IRepository<TrainingResponse, TrainingRequest> _trainingRepository;
    private List<TrainingResponse> _trainings = new();

    public EventCallback<TrainingResponse> TrainingInsertRequestFinished { get; set; }

    public IEnumerable<TrainingResponse> Trainings
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

    public TrainingsModel(IRepository<TrainingResponse, TrainingRequest> trainingRepository)
    {
        _trainingRepository = trainingRepository;
    }

    public async Task AddTraining(TrainingRequest request)
    {
        var trainingResponse = await _trainingRepository.Insert(request);
        var isSuccessful = trainingResponse.IsResponseSuccess;
        if (isSuccessful)
        {
            _trainings.Add(trainingResponse);
        }

        await TrainingInsertRequestFinished.InvokeAsync(trainingResponse);
    }
}