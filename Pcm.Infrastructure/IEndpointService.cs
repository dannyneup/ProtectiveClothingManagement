namespace Pcm.Infrastructure;

public interface IEndpointService
{
    string GetMappedUrl(object responseModel);
}