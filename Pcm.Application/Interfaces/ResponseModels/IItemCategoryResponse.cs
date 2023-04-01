namespace Pcm.Application.Interfaces.ResponseModels;

public interface IItemCategoryResponse : IResponseBase
{
    int Id { get; set; }
    string Name { get; set; }
    bool IsResponseSuccess { get; set; }
}