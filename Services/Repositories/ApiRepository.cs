using BikeRentalApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class ApiRepository<T> : IRepositoryAsync<T> where T : IEntity
    {
        public HttpClient Client { get; }

        public ApiRepository(IHttpClientFactory clientFactory)
        {
            Client = clientFactory.CreateClient("Client");
        }

        // Sends GET to specified route - returns an IEnumerable<T> or null
        public async Task<IEnumerable<T>> GetAsync(string route)
        {
            using HttpResponseMessage response = await Client.GetAsync(route);
            return await DeserializeFromResponse<IEnumerable<T>>(response);
        }

        // Sends GET to specified route with id appended - returns T or null
        public async Task<T> GetAsync(int id, string route)
        {
            using HttpResponseMessage response = await Client.GetAsync($"{route}/{id}");
            return await DeserializeFromResponse<T>(response);
        }

        // Sends DELETE to specified route with id appended - returns T or null
        public async Task<T> DeleteAsync(int id, string route)
        {
            using HttpResponseMessage response = await Client.DeleteAsync($"{route}/{id}");
            return await DeserializeFromResponse<T>(response);
        }

        // Sends POST with item serialized to json in request body to specified route
        // returns T or null
        public async Task<T> InsertAsync(T item, string route)
        {
            using HttpResponseMessage response = await Client.PostAsJsonAsync(route, item);
            return await DeserializeFromResponse<T>(response);
        }

        // Sends PUT with item serialized to json in request body to specified route
        // returns T or null
        public async Task<T> UpdateAsync(T item, string route)
        {
            using HttpResponseMessage response = await Client.PutAsJsonAsync(route, item);
            return await DeserializeFromResponse<T>(response);
        }

        // Returns response content deserialized to TResult, or default(TResult)
        // if the response was unsucessful
        public async Task<TResult> DeserializeFromResponse<TResult>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TResult>();

            return default;
        }
    }
}
