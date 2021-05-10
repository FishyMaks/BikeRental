using BikeRentalApi;
using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using EmployeeSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSite.Controllers
{
    public class ReservationController : Controller
    {
        readonly IRepositoryAsync<Reservation> _resRepo;
        readonly IRepositoryAsync<Customer> _custRepo;
        readonly IRepositoryAsync<Bike> _bikeRepo;

        public ReservationController(IRepositoryAsync<Reservation> res, IRepositoryAsync<Customer> cust,
            IRepositoryAsync<Bike> bike)
        {
            _resRepo = res;
            _custRepo = cust;
            _bikeRepo = bike;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Reservation> res = await _resRepo.GetAsync(BikeRentalRoute.Reservations);
            IEnumerable<Bike> bike = await _bikeRepo.GetAsync(BikeRentalRoute.Bikes);
            IEnumerable<Customer> cust = await _custRepo.GetAsync(BikeRentalRoute.Customers);
            foreach (Reservation r in res)
            {
                r.Customer = cust.FirstOrDefault(p => r.CustomerId == p.Id);
                r.Bike = bike.FirstOrDefault(p => p.Id == r.BikeId);
            }
            return View(res);
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
            res = await _resRepo.InsertAsync(res, BikeRentalRoute.Reservations);
            return View("Bike created", res);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Delete(int id)
        {
            Reservation res = await _resRepo.GetAsync(id, BikeRentalRoute.Reservations);
            if (res == null)
                return NotFound();

            await _resRepo.DeleteAsync(id, BikeRentalRoute.Reservations);

            return RedirectToAction(nameof(Index));

        }
    }
}
