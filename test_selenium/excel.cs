using ClosedXML.Excel;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;


namespace test_selenium
{
    class excel
    {
        string path;
        DataSet ds = new DataSet();
        public excel(string path)
        {
            this.path = path;
            ds = new DataSet();
        }
        public void saveToExcelFile()
        {
           
            try
            {
                XLWorkbook xl = new XLWorkbook();
                xl.Worksheets.Add(ds);
                xl.SaveAs(this.path);
                Console.WriteLine("goood :)");
            }
            catch(Exception ex)
            {
                Console.WriteLine("error :(");
            }
            
        }

        public void AddToDataSet(List<product> list, string sheetName)
        {
            var newList = from p in list orderby p.Price ascending select p;
            list = newList.ToList();
            DataTable dt = new DataTable(sheetName);
            DataRow row;

            dt.Columns.Add("N° produit");
            dt.Columns.Add("title");
            dt.Columns.Add("price");
            dt.Columns.Add("shiping");
            dt.Columns.Add("numberBuyer");
            dt.Columns.Add("product link");
            string price = "";
            foreach (product item in list)
            {
                row = dt.NewRow();
                row[0] = item.Num;
                row[1] = item.Title;
                price = item.Price == 0 ? "None" : item.Price.ToString()+'$';
                row[2] = price;
                row[3] = item.Shiping;
                row[4] = item.Nbuyer;
                row[5] = item.ProductLink;

                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
        }
    }
}
