using System.ComponentModel;
using Dapper.FluentMap.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcmBackendApi.Models;

public class Apprenticeship
{
    public Apprenticeship(int id, string name)
    {
        int Id = id;
        Name = name;
    }
    public int Id { get; set; }
    
    public string Name { get; set; }
    public List<ArticleCategory> ArticleCategories { get; set; }
}



public class ApprenticeshipMap : EntityMap<Apprenticeship>
{
    public ApprenticeshipMap()
    {
        Map(a => a.Id).ToColumn("apprenticeshipId");
        Map(a => a.Name).ToColumn("apprenticeshipName");
    }
}