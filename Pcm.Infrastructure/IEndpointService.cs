namespace Pcm.Infrastructure;

public interface IEndpointService
{
    string GetMappedUrl(Type responseModel);
}