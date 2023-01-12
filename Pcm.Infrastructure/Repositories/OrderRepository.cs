using System.Net.Http.Json;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.Infrastructure.Repositories;

public class OrderRepository : IRepository<IOrder, int>
{
    private readonly HttpClient _httpClient;

    public OrderRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<IEnumerable<IOrder>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IOrder> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{Endpoints.BaseUrl}{Endpoints.Order}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var order = await responseMessage.Content.ReadFromJsonAsync<Order>();
            return order;
        }

        return new Order {IsResponseSuccess = false};
    }

    public Task<bool> Insert(IOrder entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}