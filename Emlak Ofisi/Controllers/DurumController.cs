﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Emlak_Ofisi.Models;

namespace Emlak_Ofisi.Controllers
{
    public class DurumController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Durum
        public ActionResult Index()
        {
            return View(db.Durums.ToList());
        }

        public PartialViewResult Durum()
        {
            var durum = db.Durums.ToList();
            return PartialView(durum);
        }

        // GET: Durum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Durum durum = db.Durums.Find(id);
            if (durum == null)
            {
                return HttpNotFound();
            }
            return View(durum);
        }

        // GET: Durum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Durum/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DurumId,DurumAd")] Durum durum)
        {
            if (ModelState.IsValid)
            {
                db.Durums.Add(durum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(durum);
        }

        // GET: Durum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Durum durum = db.Durums.Find(id);
            if (durum == null)
            {
                return HttpNotFound();
            }
            return View(durum);
        }

        // POST: Durum/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DurumId,DurumAd")] Durum durum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(durum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(durum);
        }

        // GET: Durum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Durum durum = db.Durums.Find(id);
            if (durum == null)
            {
                return HttpNotFound();
            }
            return View(durum);
        }

        // POST: Durum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Durum durum = db.Durums.Find(id);
            db.Durums.Remove(durum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
