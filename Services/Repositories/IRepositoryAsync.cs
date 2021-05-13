using BikeRentalApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public interface IRepositoryAsync<T> where T : IEntity
    {
        HttpClient Client { get; }

        Task<IEnumerable<T>> GetAsync(string route);
        Task<T> GetAsync(int id, string route);
        Task<T> InsertAsync(T item, string route);
        Task<T> DeleteAsync(int id, string route);
        Task<T> UpdateAsync(T item, string route);

        Task<TResult> DeserializeFromResponse<TResult>(HttpResponseMessage response);
    }
}
