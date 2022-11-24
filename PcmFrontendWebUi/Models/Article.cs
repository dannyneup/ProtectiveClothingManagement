namespace PcmFrontendWebUi.Models;

public class Article : ResponseBase
{
    public int Id { get; set; }
    public ArticleType Type { get; set; }
    public string Style { get; set; }
    public ArticleCategory Category { get; set; }
    public string Size { get; set; }
    public Order Order { get; set; }
    public string Status { get; set; }
    public string StatusChanged { get; set; }
}