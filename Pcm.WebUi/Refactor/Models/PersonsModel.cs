using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.WebUi.Refactor.Models;

public class PersonsModel
{
    private readonly IRepository<TraineeResponse, TraineeRequest> _traineeRepository;
    private List<TraineeResponse> _trainees = new();

    public PersonsModel(IRepository<TraineeResponse, TraineeRequest> traineeRepository)
    {
        _traineeRepository = traineeRepository;
    }

    public EventCallback<TraineeResponse> TraineeRequestFinished { get; set; }
    
    public List<TraineeResponse> Trainees
    {
        get
        {
            if (_trainees.Count == 0)
            {
                _trainees = Task.Run(async () => await 
                    _traineeRepository.GetAll()).GetAwaiter().GetResult().ToList();
            }
            return _trainees;
        }
    }

    public async Task AddTrainee(TraineeRequest request)
    {
        var traineeResponse = await _traineeRepository.Insert(request);
        bool isSuccesful = traineeResponse.IsResponseSuccess;
        if (isSuccesful)
        {
            _trainees.Add(traineeResponse);
        }

        await TraineeRequestFinished.InvokeAsync(traineeResponse);
    }
    
    public async Task UpdateTrainee(TraineeRequest request, int personellNumber)
    {
        var traineeResponse = await _traineeRepository.Update(request, personellNumber);
        bool isSuccesful = traineeResponse.IsResponseSuccess;
        if (isSuccesful)
        {
            _trainees.RemoveAll(x => x.PersonnelNumber == personellNumber);
            _trainees.Add(traineeResponse);
        }

        await TraineeRequestFinished.InvokeAsync(traineeResponse);
    }
}