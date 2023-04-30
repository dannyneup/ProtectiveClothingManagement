namespace Pcm.Infrastructure.RequestModels;

public record LoadOutPartRequest : ResponseBase
{
    public int CategoryId { get; set; }
    public int TrainingId { get; set; }
    public int Count { get; set; }
}