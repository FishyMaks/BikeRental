using BikeRentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Repositories;
using Services.Reservations;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class ReservationController : Controller
    {
        readonly IReservationService _reservationService;
        readonly IRepositoryAsync<Bike> _bikesRepo;

        public ReservationController(IReservationService reservationService,
            IRepositoryAsync<Bike> bikes)
        {
            _reservationService = reservationService;
            _bikesRepo = bikes;
        }

        public async Task<IActionResult> Index(int id)
        {
            Bike requestedBike = await _bikesRepo.GetAsync(id, BikeRentalRoute.Bikes);

            if (requestedBike == null)
                return NotFound();

            return View(requestedBike);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationRequest reservationRequest)
        {
            // Refresh page if model validation fails
            if (!ModelState.IsValid)
            {
                TempData["InvalidSubmit"] = true;
                return RedirectToAction(nameof(Index), reservationRequest.RequestedBikeId);
            }

            // Create a reservation and move to confirmation page
            Reservation createdReservation = await _reservationService
                .CreateReservationAsync(reservationRequest);

            return View("ReservationConfirmed", createdReservation);
        }
    }
}
