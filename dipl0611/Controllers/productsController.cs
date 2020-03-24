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
    public class productsController : Controller
    {
        private Model db = new Model();

        // GET: products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.name);
            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.id_kontr = new SelectList(db.kontragents.Where(x => x.type_kontr_id == 1), "id", "name");
            return View();
        }

        // POST: products/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,id_kontr,lowBorderOrder")] products products)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_kontr = new SelectList(db.kontragents, "id", "name", products.id_kontr);
            return View(products);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_kontr = new SelectList(db.kontragents, "id", "name", products.id_kontr);
            return View(products);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price,id_kontr,lowBorderOrder")] products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_kontr = new SelectList(db.kontragents, "id", "name", products.id_kontr);
            return View(products);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            products products = db.products.Find(id);
            db.products.Remove(products);
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
