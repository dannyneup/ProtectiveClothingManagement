using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.Repositories;

public class PersonInfoRepository : IRepository<IPersonInfoResponseModel, IPersonInfoRequestModel>
{
    private readonly string _personInfoRequestUri = $"{Endpoints.BaseUrl}{Endpoints.PersonInfos}";
    private readonly HttpClient _httpClient;
    
    public PersonInfoRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<IPersonInfoResponseModel>> GetAll()
    {
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<List<PersonInfoResponseModel>>(_personInfoRequestUri, options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<PersonInfoResponseModel>();
        }
    }

    public Task<IPersonInfoResponseModel> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IPersonInfoResponseModel> Insert(IPersonInfoRequestModel requestModel)
    {
        throw new NotImplementedException();
    }

    public Task<IPersonInfoResponseModel> Update(IPersonInfoResponseModel responseModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}