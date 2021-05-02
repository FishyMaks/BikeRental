﻿using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using EmployeeSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BikeRentalApi;

namespace EmployeeSite.Controllers
{
    public class HomeController : Controller
    {
        readonly IRepositoryAsync<Reservation> _resRepo;
        readonly IRepositoryAsync<Customer> _custRepo;
        readonly IRepositoryAsync<Bike> _bikeRepo;

        public HomeController(IRepositoryAsync<Reservation> res, IRepositoryAsync<Customer> cust,
            IRepositoryAsync<Bike> bike)
        {
            _resRepo = res;
            _custRepo = cust;
            _bikeRepo = bike;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Reservation> res = await _resRepo.GetAsync(BikeRentalRoute.Reservations);
            IEnumerable<Bike> bike = await _bikeRepo.GetAsync(BikeRentalRoute.Bikes);
            IEnumerable<Customer> cust = await _custRepo.GetAsync(BikeRentalRoute.Customers);
            foreach(Reservation r in res)
            {
                r.Customer = cust.FirstOrDefault(p=>r.CustomerId == p.Id);
                r.Bike = bike.FirstOrDefault(p => p.Id == r.BikeId);
            }
            return View(res);
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