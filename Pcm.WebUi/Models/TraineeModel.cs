using Pcm.Application.Interfaces.Repositories;
using Pcm.Application.Models;
using Pcm.Infrastructure.DTOs.RequestModels;
using Pcm.Infrastructure.DTOs.ResponseModels;
using Pcm.WebUi.Enums;

namespace Pcm.WebUi.Models;

public class TraineeModel : ModelBase, IModel<Trainee>
{
    private readonly IRepository<TraineeResponse, TraineeRequest> _traineeRepository;
    
    public TraineeModel(IRepository<TraineeResponse, TraineeRequest> traineeRepository)
    {
        _traineeRepository = traineeRepository;
    }
    
    public async Task<IEnumerable<Trainee>> GetAll(bool extended = false)
    {
        var queries = new Dictionary<string, string>
        {
            {"extended", extended.ToString()}
        };
        var traineeResponse = await _traineeRepository.GetAll(queries);
        traineeResponse = traineeResponse.ToList();
        if (!traineeResponse.FirstOrDefault()!.IsResponseSuccess)
        {
            InvokeOperationDone(this, new List<Trainee>(), CrudOperation.Load, false);
            return new List<Trainee>();
        }

        var trainee = traineeResponse.Select(MapTraineeResponseToTrainee).ToList();
        return trainee;
    }

    public async Task<Trainee> Get(int id, bool extended = false)
    {
        var query = new Dictionary<string, string>
        {
            {"extended", extended.ToString()}
        };
        var traineeResponse = await _traineeRepository.Get(id, query);
        if (!traineeResponse.IsResponseSuccess)
        {
            InvokeOperationDone(this, new Trainee(), CrudOperation.Load, false);
            return new Trainee();
        }
        
        var trainee = MapTraineeResponseToTrainee(traineeResponse);
        return trainee;
    }

    public async Task Add(Trainee entity)
    {
        var personRequest = new TraineeRequest
        {
            PersonnelNumber = entity.PersonnelNumber,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            EmailAddress = entity.EmailAddress,
            TrainingId = entity.Training.Id,
            YearStarted = entity.YearStarted
        };
        var traineeResponse = await _traineeRepository.Insert(personRequest);
        if (!traineeResponse.IsResponseSuccess)
        {
            InvokeOperationDone(this, entity, CrudOperation.Create, false);
            return;
        }

        var insertedTrainee = MapTraineeResponseToTrainee(traineeResponse);
        InvokeOperationDone(this, insertedTrainee, CrudOperation.Create, true);
    }

    public async Task Update(Trainee entity)
    {
        var traineeRequest = new TraineeRequest
        {
            PersonnelNumber = entity.PersonnelNumber,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            EmailAddress = entity.EmailAddress,
            TrainingId = entity.Training.Id,
            YearStarted = entity.YearStarted
        };
        var traineeResponse = await _traineeRepository.Update(traineeRequest, entity.PersonnelNumber);
        if (!traineeResponse.IsResponseSuccess)
        {
            InvokeOperationDone(this, entity, CrudOperation.Update, false);
            return;
        }

        var updatedTrainee = MapTraineeResponseToTrainee(traineeResponse);
        InvokeOperationDone(this, updatedTrainee, CrudOperation.Update, true);
    }

    public async Task Delete(Trainee entity)
    {
        var deleted = await _traineeRepository.Delete(entity.PersonnelNumber);
        if (!deleted)
        {
            InvokeOperationDone(this, entity, CrudOperation.Delete, false);
            return;
        }

        InvokeOperationDone(this, entity, CrudOperation.Delete, true);
    }
    
    private Trainee MapTraineeResponseToTrainee(TraineeResponse traineeResponse)
    {
        return new Trainee
        {
            PersonnelNumber = traineeResponse.PersonnelNumber,
            FirstName = traineeResponse.FirstName,
            LastName = traineeResponse.LastName,
            EmailAddress = traineeResponse.EmailAddress,
            YearStarted = traineeResponse.YearStarted
        };
    }
}