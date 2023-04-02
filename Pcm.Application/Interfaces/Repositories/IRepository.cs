namespace Pcm.Application.Interfaces.Repositories;

public interface IRepository<TResponse, in TRequest> where TResponse : class where TRequest : class
{
    Task<IEnumerable<TResponse>> GetAll(Dictionary<string, string> queries = default!);
    Task<TResponse> Get(int id, Dictionary<string, string> queries = null);
    Task<TResponse> Insert(TRequest requestModel);
    Task<TResponse> Update(TRequest requestModel, int id);
    Task<bool> Delete(int id);
}