using Cari.Data;
using Cari.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cari.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext db, ILogger<CustomersController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET: CustomersController
        public ActionResult Index()
        {
            IEnumerable<Customer> objCustomer = _db.Customer;
            return View(objCustomer);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            var result = _db.Customer.Find(id);
            return View(result);
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer obj)
        {
            try
            {
                _db.Customer.Add(obj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _db.Customer.Find(id);
            return View(result);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer obj)
        {
            try
            {
                _db.Customer.Update(obj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _db.Customer.Find(id);
            return View(result);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var obj = _db.Customer.Find(id);
                _db.Customer.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
