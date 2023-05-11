using System.Text.Json.Serialization;
using Pcm.Application.Interfaces;

namespace Pcm.Infrastructure;

public record ResponseBase : IResponseBase
{
    [JsonIgnore] public bool IsResponseSuccess { get; init; } = true;
}