using System.Text.Json.Serialization;

namespace Pcm.Infrastructure.ResponseModels;

public class ResponseBase
{
    [JsonIgnore] public bool IsResponseSuccess { get; set; } = true;
}