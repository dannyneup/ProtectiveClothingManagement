using Pcm.Application.Interfaces.Repositories;
using Pcm.Application.Models;
using Pcm.Infrastructure.DTOs.RequestModels;
using Pcm.Infrastructure.DTOs.ResponseModels;
using Pcm.WebUi.Enums;

namespace Pcm.WebUi.Models;

public class TrainingModel : ModelBase, IModel<Training>
{
    private readonly ITrainingRepository<TrainingResponse, TrainingRequest, LoadOutPartResponse> _trainingRepository;

    public TrainingModel(ITrainingRepository<TrainingResponse, TrainingRequest, LoadOutPartResponse> trainingRepository)
    {
        _trainingRepository = trainingRepository;
    }
    
    public async Task<IEnumerable<Training>> GetAll(bool extended = false)
    {
        var query = new Dictionary<string, string>
        {
            {"extended", extended.ToString()}
        };
        var trainingResponses = await _trainingRepository.GetAll(query);
        trainingResponses = trainingResponses.ToList();
        var isResponseSuccess = trainingResponses.FirstOrDefault()!.IsResponseSuccess;
        if (!isResponseSuccess)
        {
            InvokeOperationDone(this, new List<Training>(), CrudOperation.Load, false);
            return new List<Training>();
        }
        var trainings = trainingResponses.Select(MapTrainingResponseToTraining);
        InvokeOperationDone(this, trainings, CrudOperation.Load,true);
        return trainings;
    }

    public async Task Add(Training training)
    {
        var trainingRequest = new TrainingRequest
        {
            Name = training.Name,
            Type = training.Type,
            LoadOut = training.LoadOut.Select(x => new LoadOutPartRequest
                {
                    ItemCategoryId = x.CategoryId,
                    Count = x.Count
                }
            ).ToList()
        };
        var trainingResponse = await _trainingRepository.Insert(trainingRequest);
        if (!trainingResponse.IsResponseSuccess)
        {
            InvokeOperationDone(this, training, CrudOperation.Create, false);
            return;
        }

        var insertedTraining = MapTrainingResponseToTraining(trainingResponse);
        InvokeOperationDone(this, insertedTraining, CrudOperation.Create, true);
    }

    public async Task Update(Training training)
    {
        var trainingRequest = new TrainingRequest
        {
            Name = training.Name,
            Type = training.Type,
            LoadOut = training.LoadOut.Select(x => new LoadOutPartRequest
            {
                ItemCategoryId = x.CategoryId,
                Count = x.Count
            }).ToList()
        };
        var trainingResponse = await _trainingRepository.Update(trainingRequest, training.Id);
        if (!trainingResponse.IsResponseSuccess)
        {
            InvokeOperationDone(this, training, CrudOperation.Update, false);
            return;
        }

        var updatedTraining = MapTrainingResponseToTraining(trainingResponse);
        InvokeOperationDone(this, updatedTraining, CrudOperation.Update, true);
    }

    public async Task Delete(Training training)
    {
        var deleted = await _trainingRepository.Delete(training.Id);
        if (!deleted)
        {
            InvokeOperationDone(this, training, CrudOperation.Delete, false);
            return;
        }

        InvokeOperationDone(this, training, CrudOperation.Delete, true);
    }

    public async Task<Training> Get(int id, bool extended = false)
    {
        var query = new Dictionary<string, string>
        {
            {"extended", extended.ToString()}
        };
        var trainingResponse = await _trainingRepository.Get(id, query);
        if (!trainingResponse.IsResponseSuccess)
        {
            InvokeOperationDone(this, new Training(), CrudOperation.Load, false);
            return new Training();
        }

        var training = MapTrainingResponseToTraining(trainingResponse);
        return training;
    }
    
    public async Task<IEnumerable<LoadOutPart>> GetLoadOut(int trainingId)
    {
        var loadOutResponse = await _trainingRepository.GetLoadOut(trainingId) as List<LoadOutPartResponse> ??
                              new List<LoadOutPartResponse>();
        return loadOutResponse.Select(x => new LoadOutPart
        {
            CategoryId = x.CategoryId,
            CategoryName = x.CategoryName,
            Count = x.Count
        });
    }

    private static Training MapTrainingResponseToTraining(TrainingResponse response)
    {
        return new Training
        {
            Id = response.Id,
            Name = response.Name,
            Type = response.Type,
            TraineeCount = response.TraineeCount,
            YearCount = response.YearCount
        };
    }
}