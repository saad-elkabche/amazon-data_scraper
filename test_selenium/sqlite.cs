using ConsoleApp1;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace test_selenium
{
    class sqlite
    {
        SQLiteConnection con = new SQLiteConnection(@$"data source={Environment.CurrentDirectory}\data\amazonDB.db");
        SQLiteCommand cmd;
        public sqlite()
        {
            cmd = new SQLiteCommand("delete from products", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            cmd = new SQLiteCommand("insert into products values(@num,@title,@price,@nbBuyers,@shiping,@link)", con);
        }
        public void addProducts(List<product> list)
        {
            foreach(product item in list)
            {
                cmd.Parameters.AddWithValue("@num", item.Num);
                cmd.Parameters.AddWithValue("@title", item.Title);
                cmd.Parameters.AddWithValue("@price", item.Price);
                cmd.Parameters.AddWithValue("@nbBuyers", item.Nbuyer);
                cmd.Parameters.AddWithValue("@shiping", item.Shiping);
                cmd.Parameters.AddWithValue("@link", item.ProductLink);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
