using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSite.Views
{
    public class biketestcontroller : Controller
    {
        // GET: biketestcontroller
        public ActionResult Index()
        {
            return View();
        }

        // GET: biketestcontroller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: biketestcontroller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: biketestcontroller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: biketestcontroller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: biketestcontroller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: biketestcontroller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: biketestcontroller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
