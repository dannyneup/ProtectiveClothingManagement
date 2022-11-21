namespace PcmFrontendWebUi;

public interface IHttpController
{
    Task<Person> Get(int id);
}