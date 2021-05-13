using BikeRentalApi.Models;
using EmployeeSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Repositories;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EmployeeSite.Controllers
{
    public class StoreController : Controller
    {
        readonly IRepositoryAsync<BikeStore> _storeRepo;

        public StoreController(IRepositoryAsync<BikeStore> store)
        {
            _storeRepo = store;
        }
        // GET: BikeController
        public async Task<IActionResult> Index()
        {
            IEnumerable<BikeStore> store = await _storeRepo.GetAsync(BikeRentalRoute.BikeStores);
            return View(store);
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Delete(int id)
        {
            BikeStore bikeStore = await _storeRepo.GetAsync(id, BikeRentalRoute.BikeStores);
            if (bikeStore == null)
                return NotFound();

            await _storeRepo.DeleteAsync(id, BikeRentalRoute.BikeStores);

            return RedirectToAction(nameof(Index));
        }
    }
}
