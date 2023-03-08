using Microsoft.Extensions.Configuration;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure;

public class EndpointService : IEndpointService
{
    private readonly string _serviceUrl;
    private const string FallbackUrl = "localhost/";

    public EndpointService(IConfiguration configurationRoot)
    {
        _serviceUrl = (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PCM_SERVICE_URL")) 
            ? configurationRoot.GetSection("BackendServices")["Url"] : FallbackUrl) ?? FallbackUrl;
    }

    public string GetMappedUrl(object responseModel)
    {
        if (ReferenceEquals(responseModel, typeof(PersonResponse)))
        {
            return _serviceUrl + ResourceUrls.Persons;
        }
        if (ReferenceEquals(responseModel, typeof(ItemResponse)))
        {
            return _serviceUrl + ResourceUrls.Items;
        }
        if (ReferenceEquals(responseModel, typeof(ModelResponse)))
        {
            return _serviceUrl + ResourceUrls.Models;
        }
        if (ReferenceEquals(responseModel, typeof(ItemCategoryResponse)))
        {
            return _serviceUrl + ResourceUrls.ItemCategorys;
        }
        if (ReferenceEquals(responseModel, typeof(OrderResponse)))
        {
            return _serviceUrl + ResourceUrls.Orders;
        }
        if (ReferenceEquals(responseModel, typeof(TrainingResponse)))
        {
            return _serviceUrl + ResourceUrls.Trainings;
        }
        if (ReferenceEquals(responseModel, typeof(LoadOutPartResponse)))
        {
            return _serviceUrl + ResourceUrls.Loadouts;
        }

        return _serviceUrl;
    }
}