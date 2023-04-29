namespace Pcm.Application.Models;

public record Training
{
    public int Id { get; init; }
    public string Type { get; set; } = "";
    public string Name { get; set; } = "";
    public int TraineeCount { get; set; }
    public int YearCount { get; set; }
    public List<LoadOutPart> LoadOut { get; set; } = Enumerable.Empty<LoadOutPart>().ToList();

}