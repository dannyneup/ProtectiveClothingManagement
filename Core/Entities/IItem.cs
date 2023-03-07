namespace Pcm.Core.Entities;

public interface IItem
{
    IItemType Type { get; set; }
    string Style { get; set; }
    string Size { get; set; }
}