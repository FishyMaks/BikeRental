using BikeRentalApi;
using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using EmployeeSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EmployeeSite.Controllers
{
    public class BikeController : Controller
    {
        readonly IRepositoryAsync<Bike> _bikeRepo;

        public BikeController(IRepositoryAsync<Bike> bike)
        {
            _bikeRepo = bike;
        }
        // GET: BikeController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Bike> bike = await _bikeRepo.GetAsync(BikeRentalRoute.Bikes);
            return View(bike);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("StoreId,JobTitle,FirstName,LastName,PhoneNumber")] Bike bike)
        {
            if (!ModelState.IsValid)
            {
                TempData["InvalidSubmit"] = true;
                return View(nameof(Index), bike);
            }
            bike = await _bikeRepo.InsertAsync(bike, BikeRentalRoute.Bikes);
            return View("Bike created", bike);
        }
        public IActionResult Edit()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Delete(int id)
        {
            Bike bike = await _bikeRepo.GetAsync(id, BikeRentalRoute.Bikes);
            if (bike == null)
                return NotFound();

            await _bikeRepo.DeleteAsync(id, BikeRentalRoute.Bikes);

            return RedirectToAction(nameof(Index));

        }
    }
}
