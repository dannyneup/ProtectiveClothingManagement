namespace Pcm.Application.Interfaces.Repositories;

public interface ITrainingRepository<TResponse, in TRequest, TLoadOutPartResponse> : IRepository<TResponse, TRequest> 
    where TResponse : class, IResponseBase 
    where TRequest : class 
{
    Task<IEnumerable<TLoadOutPartResponse>> GetLoadOut(int trainingId);
}