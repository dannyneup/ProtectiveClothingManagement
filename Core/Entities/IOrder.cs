namespace Pcm.Core.Entities;

public interface IOrder
{
    string Shop { get; set; }
    string OrderNumber { get; set; }
    DateTime Date { get; set; }
}