﻿using Cari.Data;
using Cari.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cari.Controllers
{
    public class CariHareketController : Controller
    {
        private readonly ILogger<CariHareketController> _logger;
        private readonly ApplicationDbContext _db;

        public CariHareketController(ApplicationDbContext db, ILogger<CariHareketController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET: CariHareketController
        public ActionResult Index()
        {
            IEnumerable<CariHareket> objCariHareket = _db.CariHareket;
            CariHareket cariHareket = new CariHareket();
            ViewBag.IslemTuru = cariHareket.IslemTuruArray;
            return View(objCariHareket);
        }

        // GET: CariHareketController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CariHareketController/Create
        public ActionResult Create()
        {
            ViewBag.Customers = _db.Customer.ToList();
            CariHareket cariHareket = new CariHareket();
            return View(cariHareket);
        }

        // POST: CariHareketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CariHareket obj)
        {
            try
            {
                _db.CariHareket.Add(obj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CariHareketController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _db.CariHareket.Find(id);
            return View(result);
        }

        // POST: CariHareketController/Edit/5
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

        // GET: CariHareketController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CariHareketController/Delete/5
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