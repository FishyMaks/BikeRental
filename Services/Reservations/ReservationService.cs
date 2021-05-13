using BikeRentalApi.Models;
using Services.Repositories;
using Services.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Reservations
{
    public class ReservationService : IReservationService
    {
        readonly IRepositoryAsync<Reservation> _reservationsRepo;
        readonly IStoreService _storeService;

        public ReservationService(IRepositoryAsync<Reservation> reservations,
            IStoreService storeService)
        {
            _reservationsRepo = reservations;
            _storeService = storeService;
        }

        public async Task<Reservation> CreateReservationAsync(ReservationRequest request)
        {
            Bike bike = await _storeService.ReserveBikeAsync(request.RequestedBikeId);
            Customer customer = await _storeService.RegisterCustomerAsync(request.Customer);
            PricingInfo pricingInfo = await _storeService.CalculatePriceAsync(bike, request.DaysRequested);

            // Get the reservation object
            Reservation reservation = request.BuildReservation(pricingInfo);

            // Post it
            reservation = await _reservationsRepo
                .InsertAsync(reservation, BikeRentalRoute.Reservations);

            // We have to set these properties after posting or there will be key conflicts
            reservation.Customer = customer;
            reservation.CustomerId = customer.Id;
            reservation.Bike = bike;

            // Update it
            reservation = await _reservationsRepo
                .UpdateAsync(reservation, BikeRentalRoute.Reservations);

            return reservation;
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            return await _reservationsRepo
                .InsertAsync(reservation, BikeRentalRoute.Reservations);
        }

        public async Task<Reservation> DeleteReservationAsync(int reservationId)
        {
            Reservation deleted = await _reservationsRepo
                .DeleteAsync(reservationId, BikeRentalRoute.Reservations);

            deleted.Bike = await _storeService.TurnInBikeAsync(deleted.BikeId);

            return deleted;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            IEnumerable<Reservation> reservations = await _reservationsRepo
                .GetAsync(BikeRentalRoute.Reservations);

            IEnumerable<Customer> customers = await _storeService.GetCustomersAsync();
            IEnumerable<Bike> bikes = await _storeService.GetBikesAsync();

            foreach (Reservation r in reservations)
            {
                r.Customer = customers.FirstOrDefault(c => c.Id == r.CustomerId);
                r.Bike = bikes.FirstOrDefault(b => b.Id == r.BikeId);
            }

            return reservations;
        }
    }
}
