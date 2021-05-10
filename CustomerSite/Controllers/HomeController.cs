using BikeRentalApi;
using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        readonly IRepositoryAsync<Bike> _bikesRepo;
        readonly IRepositoryAsync<BikeStore> _bikeStoresRepo;

        public HomeController(IRepositoryAsync<Bike> bikes, 
            IRepositoryAsync<BikeStore> bikeStores)
        {
            _bikesRepo = bikes;
            _bikeStoresRepo = bikeStores;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Bike> bikes = await _bikesRepo.GetAsync(BikeRentalRoute.Bikes);

            foreach (Bike bike in bikes)
                bike.OwningStore = await _bikeStoresRepo
                    .GetAsync(bike.OwningStoreId, BikeRentalRoute.BikeStores);

            return View(bikes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
