using BikeRentalApi.Models;
using Services.Extensions;
using Services.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Stores
{
    public class StoreService : IStoreService
    {
        readonly IRepositoryAsync<BikeStore> _storesRepo;
        readonly IRepositoryAsync<Bike> _bikesRepo;
        readonly IRepositoryAsync<Customer> _customersRepo;

        public StoreService(IRepositoryAsync<BikeStore> stores, IRepositoryAsync<Bike> bikes,
            IRepositoryAsync<Customer> customers)
        {
            _storesRepo = stores;
            _bikesRepo = bikes;
            _customersRepo = customers;
        }

        public async Task<PricingInfo> CalculatePriceAsync(Bike bike, int daysRequested)
        {
            BikeStore store = await _storesRepo
                .GetAsync(bike.OwningStoreId, BikeRentalRoute.BikeStores);
            PricingInfo pricingInfo = new PricingInfo(store, bike, daysRequested);
            return pricingInfo;
        }

        public async Task<Customer> RegisterCustomerAsync(Customer customer)
        {
            Customer existing = await _customersRepo.GetByEmailAsync(customer.EmailAddress);

            if (existing != null)
                return existing;

            Customer newCustomer = await _customersRepo
                .InsertAsync(customer, BikeRentalRoute.Customers);

            return newCustomer;
        }

        public async Task<Bike> ReserveBikeAsync(int bikeId) =>
            await UpdateBikeAvailability(bikeId, false);

        public async Task<Bike> TurnInBikeAsync(int bikeId) =>
            await UpdateBikeAvailability(bikeId, true);

        public async Task<Bike> FindBikeAsync(int id) =>
            await _bikesRepo.GetAsync(id, BikeRentalRoute.Bikes);

        public async Task<IEnumerable<Bike>> GetBikesAsync() =>
            await _bikesRepo.GetAsync(BikeRentalRoute.Bikes);

        public async Task<IEnumerable<Customer>> GetCustomersAsync() =>
            await _customersRepo.GetAsync(BikeRentalRoute.Customers);

        async Task<Bike> UpdateBikeAvailability(int bikeId, bool available)
        {
            Bike bike = await _bikesRepo.GetAsync(bikeId, BikeRentalRoute.Bikes);
            if (bike != null)
                bike.Available = available;
            return await _bikesRepo.UpdateAsync(bike, BikeRentalRoute.Bikes);
        }
    }
}
