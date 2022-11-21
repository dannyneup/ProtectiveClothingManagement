using System.ComponentModel;
using Dapper.FluentMap.Mapping;


namespace PcmBackendApi.Models;

public class Article
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Manufacturer { get; set; }
    public string Style { get; set; }
    public string Category { get; set; }
    public string Size { get; set; }
    public string OrderNumber { get; set; }
    public string OrderDate { get; set; }
    public string Status { get; set; }
    public string StatusChanged { get; set; }
}


internal class ArticleMap : EntityMap<Article>
{
    public ArticleMap()
    {
        Map(a => a.Id).ToColumn("articleId");
        Map(a => a.Type).ToColumn("articleType");
    }
}