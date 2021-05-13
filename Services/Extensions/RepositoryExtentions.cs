using BikeRentalApi.Models;
using Services.Repositories;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class RepositoryExtentions
    {
        // Sends GET to customers/email/{email} - returns Customer
        public static async Task<Customer> GetByEmailAsync(
            this IRepositoryAsync<Customer> repo, string email)
        {
            using HttpResponseMessage response = await repo.Client
                .GetAsync($"{BikeRentalRoute.CustomersByEmail}/{email}");
            return await repo.DeserializeFromResponse<Customer>(response);
        }

        // Sends GET to bikes/reservation/{bikeId} - returns Reservation
        public static async Task<Reservation> GetByBikeIdAsync(
            this IRepositoryAsync<Reservation> repo, int bikeId)
        {
            using HttpResponseMessage response = await repo.Client
                .GetAsync($"{BikeRentalRoute.ReservationsByBikeId}/{bikeId}");
            return await repo.DeserializeFromResponse<Reservation>(response);
        }
    }
}
