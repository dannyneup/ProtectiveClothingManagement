using System.Text.Json.Serialization;
using Pcm.Application.Interfaces;

namespace Pcm.Infrastructure;

public class ResponseBase : IResponseBase
{
    [JsonIgnore] public bool IsResponseSuccess { get; init; } = true;
}