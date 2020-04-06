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
        public ActionResult addROW()
        {
            int id_kontr = (int) TempData["id_kontr"] ;
            int idType = (int) TempData["idType"];
            ViewBag.countRow = TempData["countRow"];
            ViewBag.idTTN = TempData["idTTN"].ToString();

            ViewBag.id_product1 = idType == 7 // либо 
                ?  new SelectList(db.products.Where(x => x.id_kontr == id_kontr), "id", "name")
                :  new SelectList(db.products, "id", "name");
            return View();
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


        // POST: TTNs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,date,nomer,id_type,id_kontr, countRow")] TTN tTN, int countRow)
        {
            ViewBag.id_TTN = tTN.id;
            if (ModelState.IsValid)
            {
                db.TTN.Add(tTN);
                db.SaveChanges();
                TempData["id_kontr"] = tTN.id_kontr;
                TempData["countRow"] = countRow;
                TempData["idTTN"] = tTN.id;
                TempData["idType"] = tTN.id_type;
                return RedirectToAction("addROW");
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
