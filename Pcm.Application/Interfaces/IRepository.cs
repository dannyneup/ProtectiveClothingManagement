namespace Pcm.Application.Interfaces;

public interface IRepository<TResponse, TRequest>
{
    Task<IEnumerable<TResponse>> GetAll();
    Task<TResponse> Get(int id);
    Task<TResponse> Insert(TRequest requestModel);
    Task<TResponse> Update(TResponse responseModel);
    Task<bool> Delete(int id);
}