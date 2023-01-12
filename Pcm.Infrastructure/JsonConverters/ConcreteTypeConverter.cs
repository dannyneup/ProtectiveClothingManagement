using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pcm.Infrastructure.JsonConverters;

public class ConcreteTypeConverter<C, I> : JsonConverter<I> where C : class, I
{
    public override I Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<C>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, I value, JsonSerializerOptions options) { }
}