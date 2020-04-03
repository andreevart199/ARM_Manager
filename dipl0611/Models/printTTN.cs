using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System;
using System.IO;
using System.Linq;
using NPOI.XSSF.UserModel;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Hosting;

namespace dipl0611.Models
{
    public class printTTN 
    {
      
        public static MemoryStream LoadFromDatabase(int id)
        {
            SqlCommand cmd;
            SqlCommand cmd2;
            SqlCommand cmd3;
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["dipl"].ConnectionString);
            
            con.Open();
            //запрос заполнение строк операций
            string query = $@"SELECT ROW_NUMBER() OVER (Order by oper.id) AS RowNumber , prod.name,oper.count, prod.price
                            FROM diplom.dbo.TTN as ttn
                            join diplom.dbo.operation as oper
                            ON ttn.id = oper.id_ttn
                            join diplom.dbo.products as prod
                            ON prod.id = oper.id_product
                            WHERE ttn.id = {id}";
            //запрос заполнение даты и номера
            string query2 = $@"Select nomer, date FROM diplom.dbo.TTN WHERE ttn.id = {id}";
            //запрос заполнение поле Поставщик
            string query3 = $@"Select name, telephone, adress,dogovor FROM diplom.dbo.TTN  as ttn 
                            join diplom.dbo.kontragents as kontr
                            on ttn.id_kontr = kontr.id
                            WHERE ttn.id = {id}";
            // todo
            cmd = new SqlCommand(query, con);
            cmd2 = new SqlCommand(query2, con);
            cmd3 = new SqlCommand(query3, con);
            //помещаем данные из SQL-запроса в переменную reader
            var reader = cmd.ExecuteReader();
            var reader2 = cmd2.ExecuteReader();
            var reader3 = cmd3.ExecuteReader();

            //массивы для заполнение нужных номеров ячеек в печатки ( 0 - это номер, 3 - наименование, 19 - цена и т.д.) 
            int[] mas = new int[4] { 0,3,19,32};
            int[] mas2 = new int[2] { 23,38 };
            int[] mas3 = new int[2] { 23, 38 };

            IWorkbook workbook = GetWorkbook();

            #region стили для ячеек(границы)
            ICellStyle styleBorder = workbook.CreateCellStyle();

            styleBorder.BorderLeft = BorderStyle.Thin;
            styleBorder.BorderTop = BorderStyle.Thin;
            styleBorder.BorderBottom = BorderStyle.Thin;
            #endregion

            #region заполняем основное тело(строки операций)
            int rowIndex = 26;
            while (reader.Read())
                {
                int cellIndex = 4;
                IRow row = workbook.GetSheet("ТОРГ12").GetRow(rowIndex);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                    row.CreateCell(mas[i]).CellStyle = styleBorder;  
                    row.GetCell(mas[i]).SetCellValue(reader[i].ToString());

                    //заполняем формулами поля НДС, Сумма, Сумма общая
                    row.CreateCell(37).SetCellFormula("18");
                    row.CreateCell(41).SetCellFormula($"AG{rowIndex+1}*1.18");
                    row.CreateCell(48).SetCellFormula($"AG{rowIndex + 1}*1.18*T{rowIndex + 1}");

                    //границы для этих полей
                    row.GetCell(41).CellStyle = styleBorder;
                    row.GetCell(37).CellStyle = styleBorder;
                    row.GetCell(48).CellStyle = styleBorder;

                    cellIndex++;
                }
                    rowIndex++;
                }
            #endregion 

            #region заполняем номер и дату
            IRow row2 = workbook.GetSheet("ТОРГ12").GetRow(22);
            while (reader2.Read())
            {
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    row2.CreateCell(mas2[i]).SetCellValue(reader2[i].ToString());
                }
            }
            #endregion

            #region заполняем грузополучателя и плательщика
            IRow []  arrRow = new IRow[2] { workbook.GetSheet("ТОРГ12").GetRow(8),
                                            workbook.GetSheet("ТОРГ12").GetRow(12)};

            while (reader3.Read())
            {
                foreach (IRow row in arrRow)
                {
                    row.CreateCell(8).SetCellValue(reader3[0].ToString().Trim() + ";"
                    + reader3[1].ToString().Trim() + ";"
                    + reader3[2].ToString().Trim() + ";"
                    + reader3[3].ToString().Trim()
                    );

                }
                
                
            }
            #endregion

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Position = 0;
            ms.Flush();
            return ms;
        }
        // Метод формирует файл на основе источника
        public static IWorkbook GetWorkbook()
        {
            IWorkbook workbook = new HSSFWorkbook();

            string pathToSource = HostingEnvironment.MapPath("~/Content/torg12.xls");
          

            using (FileStream file = new FileStream(pathToSource, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook = new HSSFWorkbook(file);
            }
            XSSFFormulaEvaluator evaluator = new XSSFFormulaEvaluator(workbook);
            return workbook;
        }
    }
}