namespace Pcm.Application.Interfaces;

public interface IRepository<TResponse, in TRequest> where TResponse : class where TRequest : class
{
    Task<IEnumerable<TResponse>> GetAll(Dictionary<string, string>? querys = default);
    Task<TResponse> Get(int id, Dictionary<string, string>? querys = default);
    Task<TResponse> Insert(TRequest requestModel);
    Task<TResponse> Update(TResponse responseModel);
    Task<bool> Delete(int id);
}


