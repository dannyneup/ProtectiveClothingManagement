using System.Text.Json.Serialization;
using Pcm.Core.Entities;

namespace Pcm.Infrastructure.Entities;

public class ResponseBase
{
    [JsonIgnore] public bool? IsResponseSuccess { get; set; } = true;
}