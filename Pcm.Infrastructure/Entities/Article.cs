using System.Text.Json.Serialization;
using Pcm.Core;
using Pcm.Core.Entities;
using Pcm.Infrastructure.JsonConverters;

namespace Pcm.Infrastructure.Entities;

public class Article : ResponseBase, IArticle
{
    public int Id { get; set; }
    [JsonConverter(typeof(ConcreteTypeConverter<ArticleType, IArticleType>))]
    public IArticleType Type { get; set; }
    public string Style { get; set; }
    public string Size { get; set; }
    [JsonConverter(typeof(ConcreteTypeConverter<Order, IOrder>))]
    public IOrder Order { get; set; }
    public string Status { get; set; }
    public string StatusChanged { get; set; }
}