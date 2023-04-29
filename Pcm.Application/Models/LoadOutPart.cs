namespace Pcm.Application.Models;

public record LoadOutPart
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public int Count { get; set; }
}