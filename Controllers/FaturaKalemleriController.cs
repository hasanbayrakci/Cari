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
            var result = _db.FaturaKalemleri;
            return View(result.ToList());
        }

        // GET: FaturaKalemleri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.FaturaKalemleri == null)
            {
                return NotFound();
            }

            var faturaKalemleri = await _db.FaturaKalemleri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faturaKalemleri == null)
            {
                return NotFound();
            }

            return View(faturaKalemleri);
        }

        // GET: FaturaKalemleri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FaturaKalemleri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tanim,Ozelligi,Birim,Kdv,Tarih")] FaturaKalemleri faturaKalemleri)
        {
            if (ModelState.IsValid)
            {
                _db.Add(faturaKalemleri);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faturaKalemleri);
        }

        // GET: FaturaKalemleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.FaturaKalemleri == null)
            {
                return NotFound();
            }

            var faturaKalemleri = await _db.FaturaKalemleri.FindAsync(id);
            if (faturaKalemleri == null)
            {
                return NotFound();
            }
            return View(faturaKalemleri);
        }

        // POST: FaturaKalemleri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tanim,Ozelligi,Birim,Kdv,Tarih")] FaturaKalemleri faturaKalemleri)
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
                    await _db.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.FaturaKalemleri == null)
            {
                return NotFound();
            }

            var faturaKalemleri = await _db.FaturaKalemleri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faturaKalemleri == null)
            {
                return NotFound();
            }

            return View(faturaKalemleri);
        }

        // POST: FaturaKalemleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.FaturaKalemleri == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FaturaKalemleri'  is null.");
            }
            var faturaKalemleri = await _db.FaturaKalemleri.FindAsync(id);
            if (faturaKalemleri != null)
            {
                _db.FaturaKalemleri.Remove(faturaKalemleri);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaturaKalemleriExists(int id)
        {
          return (_db.FaturaKalemleri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
