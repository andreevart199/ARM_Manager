using dipl0611.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dipl0611.Controllers
{
    public class reportByMinBorderController : Controller
    {
        // GET: reportByMinBorder
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileResult GetFile()
        {
            MemoryStream ms = ExportToExcel.LoadFromDatabaseBorderMin();
            string fileTtype = "application/vnd.ms-excel";
            string fileName = "Товар с критичными остатком.xls";
            return File(ms, fileTtype, fileName);
        }


    }
}