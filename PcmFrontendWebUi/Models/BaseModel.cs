using System.Text.Json.Serialization;

namespace PcmFrontendWebUi.Models;

public class ResponseBase
{
    [JsonIgnore] public bool? IsResponseSuccess { get; set; } = true;
}