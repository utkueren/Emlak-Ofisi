using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Emlak_Ofisi.Models;

namespace Emlak_Ofisi.Controllers
{
    public class İlanController : Controller
    {
        private DataContext db = new DataContext();

        // GET: İlan
        public ActionResult Index()
        {
            var username = User.Identity.Name;

            var İlans = db.İlans.Where(i=>i.UserName==username).Include(İ => İ.Mahalle).Include(İ => İ.Tip);
            return View(İlans.ToList());
        }
        public List<Sehir> SehirGetir()
        {
            List<Sehir> sehirler = db.Sehirs.ToList();
            return sehirler;
        }
        public ActionResult SemtGetir (int SehirId)
        {
            List<Semt> semtlist = db.Semts.Where(x => x.SehirId == SehirId).ToList();
            ViewBag.semtlistesi = new SelectList(semtlist, "SemtId", "SemtAd");

            
            return PartialView("SemtPartial");
        }
        public ActionResult MahalleGetir(int SemtId)
        {
            List<Mahalle> mahallelist = db.Mahalles.Where(x => x.SemtId == SemtId).ToList();
            ViewBag.mahallelistesi = new SelectList(mahallelist, "MahalleId", "MahalleAd");
            return PartialView("MahallePartial");
        }

        public List<Durum> DurumGetir()
        {
            List<Durum> durumlar = db.Durums.ToList();
            return durumlar;
        }
        public ActionResult TipGetir(int DurumId)
        {
            List<Tip> tiplist = db.Tips.Where(x => x.DurumId == DurumId).ToList();
            ViewBag.tiplistesi = new SelectList(tiplist, "TipId", "TipAd");
            return PartialView("TipPartial");
        }
        public ActionResult Images(int id)
        {
            var ilan = db.İlans.Where(i => i.İlanId == id).ToList();
            var rsml = db.Resims.Where(i => i.İlanId == id).ToList();
            ViewBag.rsml = rsml;
            ViewBag.ilan = ilan;
            return View();
        }
        [HttpPost]
        public ActionResult Images(int id,HttpPostedFileBase file)
        {
            string path = Path.Combine("/images/" + file.FileName);
            file.SaveAs(Server.MapPath(path));
            Resim rsm = new Resim();
            rsm.ResimAd = file.FileName.ToString();
            rsm.İlanId = id;
            db.Resims.Add(rsm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: İlan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            İlan İlan = db.İlans.Find(id);
            if (İlan == null)
            {
                return HttpNotFound();
            }
            return View(İlan);
        }

        // GET: İlan/Create
        public ActionResult Create()
        {
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd");
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            return View();
        }

        // POST: İlan/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "İlanId,Açıklama,Fiyat,OdaSayisi,BanyoSayisi,Kredi,Alan,Kat,Telefon,Adres,UserName,SehirId,SemtId,DurumId,MahalleId,TipId")] İlan İlan)
        {
            if (ModelState.IsValid)
            {
                İlan.UserName = User.Identity.Name;
                db.İlans.Add(İlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd");
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            return View(İlan);
        }

        // GET: İlan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            İlan İlan = db.İlans.Find(id);
            if (İlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd");
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            ViewBag.SemtId = new SelectList(db.Semts, "SemtId", "SemtAd", İlan.SemtId);
            ViewBag.MahalleId = new SelectList(db.Mahalles, "MahalleId", "MahalleAd", İlan.MahalleId);
            ViewBag.TipId = new SelectList(db.Tips, "TipId", "TipAd", İlan.TipId);
            return View(İlan);
        }

        // POST: İlan/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "İlanId,Açıklama,Fiyat,OdaSayisi,BanyoSayisi,Kredi,Alan,Kat,Telefon,Adres,UserName,SehirId,SemtId,DurumId,MahalleId,TipId")] İlan İlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(İlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirId", "SehirAd");
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumId", "DurumAd");
            return View(İlan);
        }

        // GET: İlan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            İlan İlan = db.İlans.Find(id);
            if (İlan == null)
            {
                return HttpNotFound();
            }
            return View(İlan);
        }

        // POST: İlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            İlan İlan = db.İlans.Find(id);
            db.İlans.Remove(İlan);
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
