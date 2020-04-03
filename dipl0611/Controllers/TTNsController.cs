using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dipl0611.Models;

namespace dipl0611.Controllers
{
    public class TTNsController : Controller
    {
        private Model db = new Model();

        // GET: TTNs
        public ActionResult Index()
        {
            var tTN = db.TTN.Include(t => t.kontragents).Include(t => t.type_TTN);
            return View(tTN.ToList());
        }
        public ActionResult addROW(int? id, int? id_kontr)
        {
            ViewBag.id_kontr = id_kontr;
            ViewBag.idTTN = id;
            ViewBag.id_product = new SelectList(db.products.Where(x => x.id_kontr == id_kontr), "id", "name");
            // var operation = db.operation.Where(o => o.id_ttn == id);
            return View();
        }

        public ActionResult addROWRash(int? id)
        {
    
            ViewBag.idTTN = id;
            var operation = db.operation.Where(o => o.id_ttn == id);
            return View(operation.ToList());
        }

        public ActionResult watchTTN(int? id, int? id_kontr)
        {
            ViewBag.id_kontr = id_kontr;
            ViewBag.idTTN = id;
            var operation = db.operation.Where(o => o.id_ttn == id);
            return View(operation.ToList());
        }


        // GET: TTNs/Create
        public ActionResult CreatePrihod()
        {
            ViewBag.id_kontr = new SelectList(db.kontragents.Where(x=> x.type_kontr_id==1), "id", "name");
            ViewBag.id_type = new SelectList(db.type_TTN, "id", "name");
            return View();
        }

        // GET: TTNs/Create
        public ActionResult CreateRashod()
        {
            ViewBag.id_kontr = new SelectList(db.kontragents.Where(x => x.type_kontr_id==2), "id", "name");
            ViewBag.id_type = new SelectList(db.type_TTN, "id", "name");
            return View();
        }

        public ActionResult ModalFormAction(int Id, int Id_kontr)
        {
            ViewBag.Id = Id;
            ViewBag.Id_kontr = Id_kontr;
            //ViewBag.id_product = new SelectList(db.products.Where(x=>x.id_kontr == Id_kontr), "id", "name");
            return PartialView("ModalFormContent");
        }

        public ActionResult ModalFormActionRash(int Id)
        {
            ViewBag.Id = Id;
            ViewBag.id_product = new SelectList(db.products, "id", "name");
            return PartialView("ModalFormContent");
        }

        // POST: TTNs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePrihod([Bind(Include = "id,date,nomer,id_type,id_kontr")] TTN tTN)
        {
            ViewBag.id_TTN = tTN.id;
            if (ModelState.IsValid)
            {
                db.TTN.Add(tTN);
                db.SaveChanges();
                TempData["Message"] = tTN.id_kontr;
                return RedirectToAction("addROW", new {id = tTN.id, id_kontr = tTN.id_kontr });
            }
            ViewBag.id_kontr = new SelectList(db.kontragents, "id", "name", tTN.id_kontr);
            ViewBag.id_type = new SelectList(db.type_TTN, "id", "name", tTN.id_type);
            return View(tTN);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRashod([Bind(Include = "id,date,nomer,id_type,id_kontr")] TTN tTN)
        {
            ViewBag.id_TTN = tTN.id;
            if (ModelState.IsValid)
            {
                db.TTN.Add(tTN);
                db.SaveChanges();
                TempData["Message"] = tTN.id_kontr;
                return RedirectToAction("addROWRash", new { id = tTN.id });
            }
            ViewBag.id_kontr = new SelectList(db.kontragents, "id", "name", tTN.id_kontr);
            ViewBag.id_type = new SelectList(db.type_TTN, "id", "name", tTN.id_type);
            return View(tTN);
        }

        [HttpGet]
        public FileResult PrintTTN(int id)
        {
            MemoryStream ms = printTTN.LoadFromDatabase(id);
            string fileTtype = "application/vnd.ms-excel";
            string fileName = "торг12.xls";
            return File(ms, fileTtype, fileName);
        }
    }
}
