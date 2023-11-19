using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cari.Data;
using Cari.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

                var FaturaDetay = from FD in _db.FaturaDetay
                                  join FK in _db.FaturaKalemleri
                                    on FD.FaturaKalemleriId equals FK.Id
                                  join B in _db.Birimler
                                    on FK.Birimi equals B.Id
                                  where FD.FaturaId == id
                                  select new {
                                      Id = FD.Id,
                                      Kalem = FK.Tanim,
                                      Ozelligi = FD.Ozelligi,
                                      Birimi = B.Tanim,
                                      Kdv = FD.Kdv,
                                      Miktar = FD.Miktar,
                                      BirimFiyat = FD.BirimFiyat,
                                      Tutar = FD.Tutar
                                  };

              ViewBag.FaturaDetay = FaturaDetay;
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

        public IActionResult CreateKalem(int id)
        {
            ViewBag.FaturaKalemleri = _db.FaturaKalemleri;
            ViewBag.Birimler = _db.Birimler;
            ViewBag.Id = id;
            return View();
        }

        [HttpPost("Fatura/CreateKalem/{FaturaId}")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateKalem(int FaturaId, FaturaDetay faturaDetay)
        {
            if (ModelState.IsValid)
            {
                _db.Add(faturaDetay);
                _db.SaveChanges();
                return RedirectToAction(nameof(Details), new {id = FaturaId});
            }
            return View();
        }

        public IActionResult EditKalem(int? id)
        {
            if (id == null || _db.FaturaDetay == null)
            {
                return NotFound();
            }
            var FaturaDetay = _db.FaturaDetay.Find(id);
            if (FaturaDetay == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.FaturaKalemleri = _db.FaturaKalemleri;
                ViewBag.Birimler = _db.Birimler;
            }
            return View(FaturaDetay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditKalem(int id, FaturaDetay faturaDetay)
        {
            if (id != faturaDetay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var UpdateFd = _db.FaturaDetay.Find(id);
                UpdateFd.FaturaKalemleriId = faturaDetay.FaturaKalemleriId;
                UpdateFd.Ozelligi = faturaDetay.Ozelligi;
                UpdateFd.BirimlerId = faturaDetay.BirimlerId;
                UpdateFd.Kdv = faturaDetay.Kdv;
                UpdateFd.Miktar = faturaDetay.Miktar;
                UpdateFd.BirimFiyat = faturaDetay.BirimFiyat;
                UpdateFd.Tutar = faturaDetay.Tutar;
                _db.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = UpdateFd.FaturaId });
            }
            return View(faturaDetay);
        }

        [HttpPost, ActionName("DeleteKalem")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteKalem(int id)
        {
            if (_db.FaturaDetay == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fatura'  is null.");
            }
            var faturaDetay = _db.FaturaDetay.Find(id);
            if (faturaDetay != null)
            {
                _db.FaturaDetay.Remove(faturaDetay);
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = faturaDetay.FaturaId });
        }

    }
}
