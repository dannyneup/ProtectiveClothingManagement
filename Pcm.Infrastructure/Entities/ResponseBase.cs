using System.Text.Json.Serialization;

namespace Pcm.Infrastructure.Entities;

public class ResponseBase
{
    [JsonIgnore] public bool? IsResponseSuccess { get; set; } = true;
}