using BikeRentalApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Stores
{
    public interface IStoreService
    {
        Task<Bike> ReserveBikeAsync(int bikeId);
        Task<Bike> TurnInBikeAsync(int bikeId);
        Task<Customer> RegisterCustomerAsync(Customer customer);
        Task<PricingInfo> CalculatePriceAsync(Bike bike, int daysRequested);
        Task<IEnumerable<Bike>> GetBikesAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Bike> FindBikeAsync(int bikeId);
    }
}
