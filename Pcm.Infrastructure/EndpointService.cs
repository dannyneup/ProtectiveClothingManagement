using Microsoft.Extensions.Configuration;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure;

public class EndpointService : IEndpointService
{
    private const string FallbackUrl = "localhost/";
    private readonly string _serviceUrl;

    public EndpointService(IConfiguration configurationRoot)
    {
        _serviceUrl = (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PCM_SERVICE_URL"))
            ? configurationRoot.GetSection("BackendServices")["Url"]
            : FallbackUrl) ?? FallbackUrl;
    }

    public string GetMappedUrl(object responseModel)
    {
        if (ReferenceEquals(responseModel, typeof(PersonResponse))) return _serviceUrl + ResourceUrls.Persons;
        if (ReferenceEquals(responseModel, typeof(ItemCategoryResponse)))
            return _serviceUrl + ResourceUrls.ItemCategories;
        if (ReferenceEquals(responseModel, typeof(TrainingResponse))) return _serviceUrl + ResourceUrls.Trainings;
        if (ReferenceEquals(responseModel, typeof(LoadOutPartResponse))) return _serviceUrl + ResourceUrls.LoadOuts;
        return _serviceUrl;
    }
}