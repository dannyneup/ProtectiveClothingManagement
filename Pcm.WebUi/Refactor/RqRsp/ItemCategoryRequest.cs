using Pcm.Infrastructure;

namespace Pcm.WebUi.Refactor.RqRsp;

public class ItemCategoryRequest : ResponseBase
{
    public string Name { get; init; } = "";
}