using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public  class MenuDAL
    {
        private static MenuDAL  instance = null;
        public static MenuDAL Instance
        { get { if (instance == null) instance = new MenuDAL(); return instance; } 
            private set { instance = value; }
        }   
        private MenuDAL() { }

        public DataTable hienThiMenu (int idT, ref int idBill)
        {
            int m = idT;
            int maBill = BillDAL.Instance.getUncheckBillByTable(idT);

            DataTable table = new DataTable(); //mở từ lúc được tạo 
            Sql_Connection.Instance.connect();
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HienThiMenu";

            SqlParameter pa = new SqlParameter("@ma", SqlDbType.Int);
            pa.Value = maBill;
            cmd.Parameters.Add(pa); 

            cmd.Connection = Sql_Connection.Instance.sqlCon;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

            idBill = maBill;
            return table;
        }

        public DataTable hienThiMenuByIDBill(int maBill)
        {
          

            DataTable table = new DataTable(); //mở từ lúc được tạo 
            Sql_Connection.Instance.connect();
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HienThiMenu";

            SqlParameter pa = new SqlParameter("@ma", SqlDbType.Int);
            pa.Value = maBill;
            cmd.Parameters.Add(pa);

            cmd.Connection = Sql_Connection.Instance.sqlCon;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

           
            return table;
        }






    }
}
