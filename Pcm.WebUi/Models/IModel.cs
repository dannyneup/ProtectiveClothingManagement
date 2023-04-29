using Pcm.Application.Models;
using Pcm.WebUi.CustomEventArgs;

namespace Pcm.WebUi.Models;

public interface IModel<T>
{
    Task<IEnumerable<T>> GetAll(bool extended = false);
    Task<T> Get(int id, bool extended = false);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    event EventHandler<ModelOperationDoneEventArgs>? ModelOperationDone;
}