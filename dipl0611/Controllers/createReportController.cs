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
    public class createReportController : Controller
    {
        // GET: createReport
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileResult GetFile()
        {
            MemoryStream ms =  ExportToExcel.LoadFromDatabase();
            string fileTtype = "application/vnd.ms-excel";
            string fileName = "Отчет об остатках.xls";
            return File(ms, fileTtype, fileName);
        }

    }
}