namespace Pcm.Infrastructure.RequestModels;

public record TrainingRequest : ResponseBase
{
    public string Title { get; set; } = "";
    public string Type { get; set; } = "";
    public int YearStarted { get; set; }
    //public List<LoadOutPartRequest> LoadOuts { get; init; } = new();
}