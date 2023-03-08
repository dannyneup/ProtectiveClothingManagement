using Pcm.Core.Entities;

namespace Pcm.Infrastructure.ResponseModels;

public class OrderResponse : ResponseBase, IOrder
{
    public int Id { get; init; }
    public string Shop { get; set; } = "";
    public string OrderNumber { get; set; } = "";
    public DateTime Date { get; set; }
    public int ItemCount { get; init; }
}