using Dapper.FluentMap.Mapping;

namespace PcmBackendApi.Models;

public class Apprenticeship
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, int> DefaultLoadout { get; set; }
}

internal class ApprenticeshipMap : EntityMap<Apprenticeship>
{
    public ApprenticeshipMap()
    {
        Map(a => a.Id).ToColumn("apprenticeshipId");
        Map(a => a.Name).ToColumn("apprenticeshipName");
    }
}