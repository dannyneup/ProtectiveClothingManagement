using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pcm.WebUi.Controller;

public static class ObjectCloneController
{
    public static T Clone<T>(this T obj)
    {
        var serialized = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(serialized);
    }
}