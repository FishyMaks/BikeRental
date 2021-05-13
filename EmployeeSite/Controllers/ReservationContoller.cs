using BikeRentalApi.Models;
using EmployeeSite.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Reservations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EmployeeSite.Controllers
{
    public class ReservationController : Controller
    {
        readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Reservation> reservations = await _reservationService
                .GetReservationsAsync();

            return View(reservations);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CustomerId,BikeId,CurrentStoreId,Archive,DateReserved,DateDue,DateReturned,GrandTotal")] Reservation res)
        {
            if (!ModelState.IsValid)
            {
                TempData["InvalidSubmit"] = true;
                return View(nameof(Index), res);
            }
            res = await _reservationService.CreateReservationAsync(res);
            return View("Bike created", res);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
