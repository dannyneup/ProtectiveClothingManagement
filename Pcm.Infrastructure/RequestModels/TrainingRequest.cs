namespace Pcm.Infrastructure.RequestModels;

public class TrainingRequest
{
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public int YearStarted { get; set; }
    //public List<LoadOutPartRequest> LoadOuts { get; init; } = new();
}