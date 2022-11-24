namespace PcmFrontendWebUi.Models;

public class Order : ResponseBase
{
    public int Id { get; set; }
    public string Shop { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
}