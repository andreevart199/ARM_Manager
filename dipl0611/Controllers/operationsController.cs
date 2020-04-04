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
    public class operationsController : Controller
    {
        private Model db = new Model();
        // GET: operations
        public ActionResult Index()
        {
            var operation = db.operation.Include(o => o.products).Include(o => o.TTN);
            return View(operation.ToList());
        }
        // GET: operations/Create
        public ActionResult Create()
        {
            ViewBag.id_product = new SelectList(db.products, "id", "name");
            ViewBag.id_ttn = new SelectList(db.TTN, "id", "nomer");
            return View();
        }
        // POST: operations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойстC:\Users\Администратор\source\repos\dipl0611\dipl0611\Controllers\operationsController.csва, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<operation>  operations)
        {


            IList<operation> _TableForm = new List<operation>();

            //Loop through the forms
            for (int i = 0; i <= Request.Form.Count; i++)
            {
                var ClientSampleID = Request.Form["ClientSampleID[" + i + "]"];
                var additionalComments = Request.Form["AdditionalComments[" + i + "]"];
                var acidStables = Request.Form["AcidStables[" + i + "]"];

                if (!String.IsNullOrEmpty(ClientSampleID))
                {
                    _TableForm.Add(new operation {  });
                }
            }
            if (ModelState.IsValid)
            {
                foreach (var operation in operations)
                {
                    db.operation.Add(operation);
                    db.SaveChanges();
                }

                //ViewBag.id_ttn = operation.id_ttn;
                //try
                //{
                //    ViewBag.id_kontr = Int32.Parse(Request.Params["Id_kontr"]);
                //}
                //catch
                //{
                //    return PartialView("CloseModal");
                //}
                
                //return PartialView("CloseModal");
            }
            //ViewBag.id_product = new SelectList(db.products, "id", "name", operation.id_product);
            //ViewBag.id_ttn = new SelectList(db.TTN, "id", "nomer", operation.id_ttn);
            return RedirectToAction("Index", "kontragents"); ;
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            operation operation = db.operation.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }
   
        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            operation operation = db.operation.Find(id);
            int id_TTNs = operation.TTN.id;
            int id_kontr = operation.TTN.id_kontr;

            db.operation.Remove(operation);
            db.SaveChanges(); 
            return RedirectToAction("addROW", "TTNs", new { id = id_TTNs, id_kontr = id_kontr });
        }

    }
}
