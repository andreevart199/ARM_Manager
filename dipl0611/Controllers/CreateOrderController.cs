using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using dipl0611.Models;
namespace dipl0611.Controllers
{
    public class CreateOrderController : Controller
    {
        private Model db = new Model();
        // GET: CreateOrder
        public ActionResult Index()
        {

            var list = db.kontragents.Where(x=>x.type_kontr_id == 1).ToList();
            return View(list);
        }

        public ActionResult generateOrder(int? id, string date1, string date2)
        {

            string s = ConfigurationManager.ConnectionStrings["dipl"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dipl"].ConnectionString))
            {
                
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand( $"SELECT prod.id as prodid, prod.name, prod.lowBorderOrder, " +
                                                        $"SUM(oper.count) as приход, (Select SUM(count) - SUM(oper.count)    " +
                                                        $"from dbo.operation as oper1    JOIN dbo.TTN ttn1 ON ttn1.id = oper1.id_ttn    " +
                                                        $"Where id_product = prod.id     " +
                                                        $"and ttn1.date between  {{ d '{date1}' }} and {{ d '{date2}'}}) as продажи, " +
                                                        $"(Select(Select SUM(count) From operation as operPrih    " +
                                                        $"JOIN TTN as ttnPrih    ON operPrih.id_ttn = ttnPrih.id    " +
                                                        $"Where id_product = prod.id and ttnPrih.id_type = 7) - " +
                                                        $"case when(Select SUM(count) From operation as operRash " +
                                                        $"JOIN TTN as ttnRash ON operRash.id_ttn = ttnRash.id     " +
                                                        $"Where id_product = prod.id and ttnRash.id_type = 8) is null then 0     " +
                                                        $"else (Select SUM(count) From operation as operRash    JOIN TTN as ttnRash    " +
                                                        $" ON operRash.id_ttn = ttnRash.id    " +
                                                        $" Where id_product = prod.id and ttnRash.id_type = 8  ) end ) as остатки" +
                                                        $" FROM dbo.operation as oper   Join dbo.products as prod   " +
                                                        $"ON prod.id = oper.id_product   JOIN dbo.TTN ttn   ON ttn.id = oper.id_ttn  " +
                                                        $" WHERE  ttn.id_type = 7   and ttn.date between  {{ d '{date1}'}} and {{d'{date2}'}} " +
                                                        $"and prod.id_kontr = {id} group by prod.id, prod.name, prod.lowBorderOrder"
                    , conn);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                
                var empList = dataSet.Tables[0].AsEnumerable()
                 .Select(dataRow => new orderList
                 {
                     prodid = dataRow.Field<int>("prodid"),
                     name = dataRow.Field<string>("name"),
                     lowBorderOrder = dataRow.Field<int?>("lowBorderOrder"),
                     приход = dataRow.Field<int>("приход"),
                     продажи = dataRow.Field<int>("продажи"),
                     остатки = dataRow.Field<int>("остатки")
                 }).ToList();
                adapter.Dispose();
                dataSet.Dispose();
                ViewBag.list1 = empList.ToList();
                
            }
            return View();
        }

       



    }
}