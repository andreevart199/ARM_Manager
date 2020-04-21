using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dipl0611.Models;

namespace dipl0611.Controllers
{
    public class kontragentsController : Controller
    {
        private Model db = new Model();

        // GET: kontragents
        public ActionResult Index()
        {
            //Model model = new Model();
            //var context = model.kontragents.AsEnumerable();

            return View(db.kontragents.ToList());

            
        }


        // GET: kontragents/Create
        public ActionResult Create()
        {
            ViewBag.type_kontr_id = new SelectList(db.type_kontr, "id", "name_type");
            return View();
        }

        // POST: kontragents/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(kontragents kontragents)
        {
            if (ModelState.IsValid)
            {
                db.kontragents.Add(kontragents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.type_kontr_id = new SelectList(db.type_kontr, "id", "name_type");
            return View(kontragents);
        }

        // GET: kontragents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kontragents kontragents = db.kontragents.Find(id);
            if (kontragents == null)
            {
                return HttpNotFound();
            }
            return View(kontragents);
        }

        // POST: kontragents/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(kontragents kontragents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kontragents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kontragents);
        }

        // GET: kontragents/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: kontragents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kontragents kontragents = db.kontragents.Find(id);
            db.kontragents.Remove(kontragents);
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
