namespace Pcm.Infrastructure.ResponseModels;

public class LoadOutPartResponse : ResponseBase
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public int Count { get; set; }

    /*public LoadOutPartRequestModel ToRequestModel()
    {
        return new LoadOutPartRequestModel()
        {
            ItemCategoryId = this.CategoryId,
            Count = this.Count
        };
    }*/
}