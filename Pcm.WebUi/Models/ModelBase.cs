using Pcm.WebUi.CustomEventArgs;
using Pcm.WebUi.Enums;

namespace Pcm.WebUi.Models;

public class ModelBase
{
    public event EventHandler<ModelOperationDoneEventArgs>? ModelOperationDone;
    protected void InvokeOperationDone(ModelBase sender, object obj, CrudOperation operation, bool success)
    {
        var args = new ModelOperationDoneEventArgs
        {
            Object = obj,
            Operation = operation,
            Success = success
        };
        ModelOperationDone?.Invoke(sender, args);
    }
}