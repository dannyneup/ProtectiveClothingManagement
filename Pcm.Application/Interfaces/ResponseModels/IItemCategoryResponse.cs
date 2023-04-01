namespace Pcm.Application.Interfaces.ResponseModels;

public interface IItemCategoryResponse : IResponseBase
{
    int Id { get; init; }
    string Name { get; init; }
}