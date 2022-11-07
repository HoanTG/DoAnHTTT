using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DoAnHTTT
{
    public class Database
    {
        static string duongdan = "Data Source=HOAN;Initial Catalog=QLQA;Persist Security Info=True;User ID=httt;Password=httt";
        SqlConnection conn = null;

        public void ThemThanhPho(string id, string ten)
        {
            string query = "insert into THANHPHO (MSTP, TENTP) values ('" + id + "','" + ten + "')";
            conn = new SqlConnection(duongdan);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}
