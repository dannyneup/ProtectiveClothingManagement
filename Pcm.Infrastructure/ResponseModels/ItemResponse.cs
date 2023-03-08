namespace Pcm.Infrastructure.ResponseModels;

public class ItemResponse
{
    public int Id { get; set; }
    public string Style { get; set; } = "";
    public string Size { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime StatusChanged { get; set; }
}