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
    public class FaturaKalemleriController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FaturaKalemleriController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: FaturaKalemleri
        public IActionResult Index()
        {
            var result = from FaturaKalemleri in _db.FaturaKalemleri
                         join Birimler in _db.Birimler
                             on FaturaKalemleri.Birimi equals Birimler.Id into grouping
                         from Birimler in grouping.DefaultIfEmpty()
                         select new
                         {
                             Id = FaturaKalemleri.Id,
                             Tanim = FaturaKalemleri.Tanim,
                             Ozelligi = FaturaKalemleri.Ozelligi,
                             Birimi = Birimler.Tanim,
                             Kdv = FaturaKalemleri.Kdv,
                             Tarih = FaturaKalemleri.Tarih
                         };

            return View(result.ToList());
        }

        // GET: FaturaKalemleri/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _db.FaturaKalemleri == null)
            {
                return NotFound();
            }

            var faturaKalemleri = _db.FaturaKalemleri.Find(id);

            if (faturaKalemleri == null)
            {
                return NotFound();
            }

            return View(faturaKalemleri);
        }

        // GET: FaturaKalemleri/Create
        public IActionResult Create()
        {
            ViewBag.Birimler = _db.Birimler;
            return View();
        }

        // POST: FaturaKalemleri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FaturaKalemleri faturaKalemleri)
        {
            if (ModelState.IsValid)
            {
                _db.Add(faturaKalemleri);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(faturaKalemleri);
        }

        // GET: FaturaKalemleri/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _db.FaturaKalemleri == null)
            {
                return NotFound();
            }

            var faturaKalemleri = _db.FaturaKalemleri.Find(id);
            if (faturaKalemleri == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.Birimler = _db.Birimler;
            }
            return View(faturaKalemleri);
        }

        // POST: FaturaKalemleri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FaturaKalemleri faturaKalemleri)
        {
            if (id != faturaKalemleri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(faturaKalemleri);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaturaKalemleriExists(faturaKalemleri.Id))
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
            return View(faturaKalemleri);
        }

        // GET: FaturaKalemleri/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _db.FaturaKalemleri == null)
            {
                return NotFound();
            }

            var faturaKalemleri = _db.FaturaKalemleri.Find(id);
            if (faturaKalemleri == null)
            {
                return NotFound();
            }

            return View(faturaKalemleri);
        }

        // POST: FaturaKalemleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_db.FaturaKalemleri == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FaturaKalemleri'  is null.");
            }
            var faturaKalemleri = _db.FaturaKalemleri.Find(id);
            if (faturaKalemleri != null)
            {
                _db.FaturaKalemleri.Remove(faturaKalemleri);
            }
            
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool FaturaKalemleriExists(int id)
        {
          return (_db.FaturaKalemleri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
