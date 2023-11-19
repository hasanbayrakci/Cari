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
using DinkToPdf;

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

        public IActionResult Pdf(int id)
        {
            var converter = new BasicConverter(new PdfTools());
            //var fatura = _db.Fatura.Find(id);

            var htmlContent = GetHtmlContent(id);
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 10 },
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                    },
                },
            };

            byte[] pdf = converter.Convert(doc);
            return File(pdf, "application/pdf", "output.pdf");
        }


        private string GetHtmlContent(int id)
        {

            var Fatura = _db.Fatura.Find(id);
            var Customer = _db.Customer.Find(Fatura.CustomerId);
            var FaturaDetay = from FD in _db.FaturaDetay
                              join FK in _db.FaturaKalemleri
                                on FD.FaturaKalemleriId equals FK.Id
                              join B in _db.Birimler
                                on FK.Birimi equals B.Id
                              where FD.FaturaId == id
                              select new
                              {
                                  Id = FD.Id,
                                  Kalem = FK.Tanim,
                                  Ozelligi = FD.Ozelligi,
                                  Birimi = B.Tanim,
                                  Kdv = FD.Kdv,
                                  Miktar = FD.Miktar,
                                  BirimFiyat = FD.BirimFiyat,
                                  Tutar = FD.Tutar
                              };


            var html = $@"<div style='position:absolute;top: 157px;right:55px;width: 100px'>{string.Format("{0:dd/MM/yyyy}", Fatura.FaturaTarihi) }</div>";
            html += $@"<div style='position:absolute;top: 320px'><div style='margin-left: 130px'>{ Customer.Unvan }</div><div style='margin-left: 130px;margin-top: 2px'>{ Customer.Adres }</div></div>";
            html += $@"<div style='position: absolute;top:390px;left:170px;'>{ Customer.VergiDairesi }</div><div style='position: absolute;top:390px;left:505px;'>{ Customer.VergiNo }</div>";

            html += "<div style='position: absolute;top:490px;left:70px'>";
            decimal Toplam = 0;
            decimal GenelToplam = 0;
            decimal Kdv = 0;
            foreach(var item in FaturaDetay)
            {
                Toplam += item.Tutar;
                Kdv = item.Kdv;
                html += $@"<div>
                <div style='float: left;width: 390px;'>{ item.Kalem }</div>
                <div style='float: left;width: 52px;text-align: center;'>{ item.Miktar }</div>
                <div style='float: left;width: 100px;text-align: center;'>{ item.BirimFiyat }</div>
                <div style='float: left;width: 125px;text-align: center;'>{ item.Tutar }</div>
            </div>
            <div style='clear: both'></div>";
            }
            Kdv = Toplam * (Kdv / 100);
            GenelToplam = Toplam + Kdv;
            html += "</div>";

            html += "<div style='position: absolute;width:350px;top:974px;left:120px;'>{ $textToplam }</div>";

            html += $@"<div style='position: absolute;width:110px;top:967px;right:75px'>
        <div style='float: right;width: 110px;'>
            <div style='width: 110px;text-align: right;'>{ Toplam }</div>
            <div style='width: 110px;margin-top: 5px;text-align: right;'>{ Kdv }</div>
            <div style='width: 110px;text-align: right;margin-top: 5px;'>{ GenelToplam }</div></div></div>";

            return html;
        }

    }
}
