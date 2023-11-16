using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cari.Data;
using Cari.Models;

namespace Cari.Controllers
{
    public class FaturaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FaturaController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Fatura
        public IActionResult Index()
        {

            var result = from Fatura in _db.Fatura
                         join Customer in _db.Customer
                             on Fatura.CustomerId equals Customer.Id into grouping
                         from Customer in grouping.DefaultIfEmpty()
                         select new
                         {
                             Id = Fatura.Id,
                             Unvan = Customer.Unvan,
                             SiraNo = Fatura.SiraNo,
                             FaturaTarihi = Fatura.FaturaTarihi,
                             Tarih = Fatura.Tarih
                         };

            return View(result);

        }

        // GET: Fatura/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _db.Fatura == null)
            {
                return NotFound();
            }

            var fatura = _db.Fatura.Find(id);

            if (fatura == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.Customer = _db.Customer.Find(fatura.CustomerId);
            }

            return View(fatura);
        }

        // GET: Fatura/Create
        public IActionResult Create()
        {
            ViewBag.Customers = _db.Customer.ToList();
            return View();
        }

        // POST: Fatura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fatura fatura)
        {
            if (ModelState.IsValid)
            {
                _db.Add(fatura);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(fatura);
        }

        // GET: Fatura/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _db.Fatura == null)
            {
                return NotFound();
            }

            ViewBag.Customers = _db.Customer.ToList();
            var fatura = _db.Fatura.Find(id);
            if (fatura == null)
            {
                return NotFound();
            }
            return View(fatura);
        }

        // POST: Fatura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Fatura fatura)
        {
            if (id != fatura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(fatura);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaturaExists(fatura.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fatura);
        }

        // GET: Fatura/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _db.Fatura == null)
            {
                return NotFound();
            }

            var fatura = _db.Fatura.Find(id);
            if (fatura == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.Customer = _db.Customer.Find(fatura.CustomerId);
            }

            return View(fatura);
        }

        // POST: Fatura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_db.Fatura == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fatura'  is null.");
            }
            var fatura = _db.Fatura.Find(id);
            if (fatura != null)
            {
                _db.Fatura.Remove(fatura);
            }
            
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool FaturaExists(int id)
        {
          return (_db.Fatura?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
