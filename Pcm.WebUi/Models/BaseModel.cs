using System.Text.Json.Serialization;

namespace Pcm.WebUi.Models;

public class ResponseBase
{
    [JsonIgnore] public bool? IsResponseSuccess { get; set; } = true;
}