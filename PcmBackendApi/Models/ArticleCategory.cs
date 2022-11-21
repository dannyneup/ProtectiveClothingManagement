using Dapper.FluentMap.Mapping;

namespace PcmBackendApi.Models;

public class ArticleCategory
{
    public ArticleCategory(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}

public class ArticleCategoryMap : EntityMap<ArticleCategory>
{
    public ArticleCategoryMap()
    {
        Map(aC => aC.Id).ToColumn("articleCategoryId");
        Map(aC => aC.Name).ToColumn("articleCategoryName");
    }
}