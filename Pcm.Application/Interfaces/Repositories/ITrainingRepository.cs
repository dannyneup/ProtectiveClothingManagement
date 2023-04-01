using Pcm.Application.Interfaces.RequestModels;
using Pcm.Application.Interfaces.ResponseModels;

namespace Pcm.Application.Interfaces.Repositories;

public interface ITrainingRepository<TResponse, in TRequest> : IRepository<TResponse, TRequest> 
    where TResponse : class where TRequest : class 
{
    Task<IEnumerable<ILoadOutPartResponse>> GetLoadOut(int trainingId);
    Task<ILoadOutPartResponse> InsertLoadOut(int trainingId, IEnumerable<ILoadOutPartRequest> requestModel);
}