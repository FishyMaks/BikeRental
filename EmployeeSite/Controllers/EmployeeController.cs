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
    public class EmployeeController : Controller
    {
        readonly IRepositoryAsync<Employee> _empRepo;

        public EmployeeController(IRepositoryAsync<Employee> emp)
        {
            _empRepo = emp;
        }
        // GET: Employee
        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> emp = await _empRepo.GetAsync(BikeRentalRoute.Employees);
            return View(emp);
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
        public async Task<IActionResult> Create([Bind("JobTitle,FirstName,LastName,Emailaddress,PhoneNumberStoreId,SupervisorId")] Employee emp)
        {
            if (!ModelState.IsValid)
            {
                TempData["InvalidSubmit"] = true;
                return View(nameof(Index), emp);
            }
            emp = await _empRepo.InsertAsync(emp, BikeRentalRoute.Employees);
            return View("Employee created", emp);
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
            Employee employee = await _empRepo.GetAsync(id, BikeRentalRoute.Employees);
            if (employee == null)
                return NotFound();

            await _empRepo.DeleteAsync(id, BikeRentalRoute.Employees);

            return RedirectToAction(nameof(Index));

        }
    }
}
