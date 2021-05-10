using BikeRentalApi.Models;
using CustomerSite.Models;
using CustomerSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class ReservationController : Controller
    {
        readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index(int id)
        {
            Bike requestedBike = await _reservationService.GetBikeFromId(id);

            if (requestedBike == null)
                return NotFound();

            return View(requestedBike);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CustomerReservationViewModel vm)
        {
            Bike bike = await _reservationService.GetBikeFromId(vm.RequestedBikeId);

            if (bike == null)
                return BadRequest();

            // Refresh page if model validation fails
            if (!ModelState.IsValid)
            {
                TempData["InvalidSubmit"] = true;
                return View(nameof(Index), bike);
            }

            // Create a reservation and move to confirmation page
            Reservation createdReservation = await _reservationService
                .CreateReservation(vm.Customer, vm.RequestedBikeId, vm.DaysRequested);

            return View("ReservationConfirmed", createdReservation);
        }
    }
}
