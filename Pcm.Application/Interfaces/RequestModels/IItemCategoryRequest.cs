namespace Pcm.Application.Interfaces.RequestModels;

public interface IItemCategoryRequest
{
    string Id { get; init; }
    string Name { get; init; }
}