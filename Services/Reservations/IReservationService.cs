using BikeRentalApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Reservations
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationRequest request);
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Reservation> DeleteReservationAsync(int reservationId);
    }
}
