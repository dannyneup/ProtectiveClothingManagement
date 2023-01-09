namespace Pcm.Core.Entities;

public interface IOrder : IResponseBase
{
    int Id { get; set; }
    string Shop { get; set; }
    string Number { get; set; }
    DateTime Date { get; set; }
}