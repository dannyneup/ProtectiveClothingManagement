using System.Text.Json;

namespace Pcm.WebUi.Controller;

public static class ObjectCloneController
{
    public static T Clone<T>(this T obj)
    {
        var serialized = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(serialized);
    }
}