using System.Security.AccessControl;

namespace Pcm.Infrastructure.Repositories;

public class Endpoints
{
    private const string HostUrl = "http://localhost";
    private const string Port = "3001";
    private const string ApiVersion = "v1";
    public const string BaseUrl = $"{HostUrl}:{Port}/{ApiVersion}";
    
    public const string Persons = "/persons";
    public const string PersonInfos = "/person-infos";
    public const string Training = "/trainings";
    public const string TrainingInfo = "/training-infos";
    public const string LoadOut = "/loadout";
    public const string ItemCategory = "/categories";
}