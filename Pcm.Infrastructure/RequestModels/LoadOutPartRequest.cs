namespace Pcm.Infrastructure.RequestModels;

public class LoadOutPartRequest
{
    public int Id { get; set; }
    public int ItemCategoryId { get; set; }
    public int TrainingId { get; set; }
    public int Count { get; set; }
}