using Pcm.Core;
using Pcm.Core.Entities;

namespace Pcm.WebUi.Models;

public class Order : ResponseBase, IOrder
{
    public int Id { get; set; }
    public string Shop { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
}