using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System;
using System.IO;
using System.Linq;
using System.Configuration;

namespace dipl0611.Models
{
    public class ExportToExcel 
    {
        //FIXME перенести в отдельные методы
        public static MemoryStream LoadFromDatabase()
        {
            SqlCommand cmd;
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["dipl"].ConnectionString);
            
            con.Open();
            string query = @"SELECT kontr.name, prod.name ,(Select(Select SUM(count)
                            From operation as operPrih
                            JOIN TTN as ttnPrih
                            ON operPrih.id_ttn = ttnPrih.id
                            Where id_product = prod.id and ttnPrih.id_type = 1) -
                            case
                            when
                            (Select SUM(count) From operation as operRash JOIN TTN as ttnRash ON operRash.id_ttn = ttnRash.id
                            Where id_product = prod.id and ttnRash.id_type = 2) is null then 0
                            else (Select SUM(count) From operation as operRash
                            JOIN TTN as ttnRash
                            ON operRash.id_ttn = ttnRash.id
                            Where id_product = prod.id and ttnRash.id_type = 2 ) end
                            ) as остатки
                            FROM dbo.operation as oper
                            Join
                            dbo.products as prod
                            ON prod.id = oper.id_product
                            JOIN dbo.TTN ttn
                            ON ttn.id = oper.id_ttn
                            join dbo.kontragents as kontr
                            on ttn.id_kontr = kontr.id
                            Where kontr.id = 2
                            group by kontr.name, prod.id, prod.name";
            // todo
            cmd = new SqlCommand(query, con);
            //помещаем данные из SQL-запроса в переменную reader
            var reader = cmd.ExecuteReader();

            HSSFWorkbook workbook = new HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("таблица");

            IRow headerRow = sheet.CreateRow(0);

            string[] columnsHeader = { "Поставщик", "Наименование товара", "Остатки" };

            #region стиль для первой строки(цвет)
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LemonChiffon.Index;
            boldStyle.BorderBottom = BorderStyle.Medium;
            boldStyle.FillPattern = FillPattern.SolidForeground;
            #endregion

            //заполняем первую строку(Заголовки)
            for (int i = 0; i < columnsHeader.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(columnsHeader[i]);
                headerRow.Cells[i].CellStyle = boldStyle;
                sheet.AutoSizeColumn(i);
            }

            #region поля(границы) для ячеек
            ICellStyle styleBorder = workbook.CreateCellStyle();
            styleBorder.BorderRight = BorderStyle.Thin;
            styleBorder.BorderLeft = BorderStyle.Thin;
            styleBorder.BorderTop = BorderStyle.Thin;
            styleBorder.BorderBottom = BorderStyle.Thin;
            #endregion 

            int rowIndex = 1;
            while (reader.Read())
            {
                IRow row = workbook.GetSheet("таблица").CreateRow(rowIndex);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row.CreateCell(i).SetCellValue(reader[i].ToString());
                    row.Cells[i].CellStyle = styleBorder;
                }
                rowIndex++;
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Position = 0;
            ms.Flush();
            con.Close();
            return ms;
        }

        public static MemoryStream LoadFromDatabaseBorderMin()
        {
            SqlCommand cmd;
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Model"].ConnectionString);

            con.Open();
            string query = @"  SELECT kontr.name, prod.name,
                            (Select (Select SUM(count)
                            From operation as operPrih
                            JOIN TTN as ttnPrih
                            ON operPrih.id_ttn = ttnPrih.id
                            Where id_product = prod.id and ttnPrih.id_type = 1 ) -
                            case
                            when
                            (Select SUM(count) From operation as operRash
                            JOIN TTN as ttnRash
                            ON operRash.id_ttn = ttnRash.id
                            Where id_product = prod.id and ttnRash.id_type = 2 ) is null then 0
                            else (Select SUM(count) From operation as operRash
                            JOIN TTN as ttnRash
                            ON operRash.id_ttn = ttnRash.id
                            Where id_product = prod.id and ttnRash.id_type = 2 ) end
                            ) as остатки, prod.lowBorderOrder
                            FROM dbo.operation as oper
                            Join
                            dbo.products as prod
                            ON prod.id = oper.id_product
                            JOIN dbo.TTN ttn
                            ON ttn.id = oper.id_ttn
                            join dbo.kontragents as kontr
                            on ttn.id_kontr = kontr.id
                            Where ttn.id_kontr <> 3 and (Select (Select SUM(count)
                            From operation as operPrih
                            JOIN TTN as ttnPrih
                            ON operPrih.id_ttn = ttnPrih.id
                            Where id_product = prod.id and ttnPrih.id_type = 1 ) -
                            case
                            when
                            (Select SUM(count) From operation as operRash
                            JOIN TTN as ttnRash
                            ON operRash.id_ttn = ttnRash.id
                            Where id_product = prod.id and ttnRash.id_type = 2 ) is null then 0
                            else (Select SUM(count) From operation as operRash
                            JOIN TTN as ttnRash
                            ON operRash.id_ttn = ttnRash.id
                            Where id_product = prod.id and ttnRash.id_type = 2 ) end
                            ) < lowBorderOrder
                            group by kontr.name, prod.id, prod.name, prod.lowBorderOrder";
            // todo
            cmd = new SqlCommand(query, con);
            //помещаем данные из SQL-запроса в переменную reader
            var reader = cmd.ExecuteReader();

            HSSFWorkbook workbook = new HSSFWorkbook();

            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("таблица");

            IRow headerRow = sheet.CreateRow(0);

            string[] columnsHeader = { "Поставщик", "Наименование товара", "Остатки", "Минимальный порог остатка" };

            #region стиль для первой строки(цвет)
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LemonChiffon.Index;
            boldStyle.BorderBottom = BorderStyle.Medium;
            boldStyle.FillPattern = FillPattern.SolidForeground;
            #endregion

            //заполняем первую строку(Заголовки)
            for (int i = 0; i < columnsHeader.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(columnsHeader[i]);
                headerRow.Cells[i].CellStyle = boldStyle;
                sheet.AutoSizeColumn(i);
            }
            
            #region поля(границы) для ячеек
            ICellStyle styleBorder = workbook.CreateCellStyle();
            styleBorder.BorderRight = BorderStyle.Thin;
            styleBorder.BorderLeft = BorderStyle.Thin;
            styleBorder.BorderTop = BorderStyle.Thin;
            styleBorder.BorderBottom = BorderStyle.Thin;
            #endregion 

            int rowIndex = 1;
            while (reader.Read())
            {
                IRow row = workbook.GetSheet("таблица").CreateRow(rowIndex);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row.CreateCell(i).SetCellValue(reader[i].ToString());
                    row.Cells[i].CellStyle = styleBorder;
                }
                rowIndex++;
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Position = 0;
            ms.Flush();
            return ms;
        }



    }
}