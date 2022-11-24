namespace PcmFrontendWebUi.Repositories;

public interface IRepository<T1, T2> where T1 : class
{
    Task<IEnumerable<T1>> GetAll();
    Task<T1> Get(T2 id);
    Task<bool> Insert(T1 entity);
    Task<bool> Delete(T2 id);
}