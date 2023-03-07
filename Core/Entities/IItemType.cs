namespace Pcm.Core.Entities;

public interface IItemType
{
    string Name { get; set; }
    string Manufacturer { get; set; }
    IItemCategory Category { get; set; }
}