using Pcm.WebUi.Enums;

namespace Pcm.WebUi.CustomEventArgs;

public class ModelOperationDoneEventArgs
{
    public object Object = default!;
    public CrudOperation Operation = default!;
    public bool Success = false;
}