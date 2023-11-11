using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public  class Bill_InforDAL
    {
        private Bill_InforDAL() { }
        private static Bill_InforDAL instance= null;
        public static Bill_InforDAL Instance
        {
            get { if (instance == null) instance = new Bill_InforDAL(); return instance; }

            set { instance = value; }   
        }
        public bool  themMonAnChoBill(int maBill, int maFood,int  soLuong)
        {
           
            Sql_Connection.Instance.connect();
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddBill_Infor";

            SqlParameter parIdB = new SqlParameter("@maBill", SqlDbType.Int);
            SqlParameter parIdF = new SqlParameter("@maFood", SqlDbType.Int);
            SqlParameter parCount = new SqlParameter("@count", SqlDbType.Int);
            parIdB.Value = maBill;
            parIdF.Value = maFood;
            parCount.Value = soLuong;

            cmd.Parameters.Add(parIdB);
            cmd.Parameters.Add(parIdF);
            cmd.Parameters.Add(parCount);


            cmd.Connection = Sql_Connection.Instance.sqlCon;
            if (cmd.ExecuteNonQuery() <0)
            {
                return false;
            }    
            return true;




        }

       public  DataTable getBill_Infor(int maBill)
        {
            DataTable dt = new DataTable();
            string q = "select * from bill_infor where idBill =  " + maBill;
            dt = DataProvider.Instance.excecuteQuerry(q);

            return dt;

        }
        public bool capNhapSoLuong (int maBill, int maMon, int soLuong)
        {
            Sql_Connection.Instance.connect();
            Sql_Connection.Instance.openCon();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "updateCount";
            SqlParameter parMaBill = new SqlParameter("@maBill", SqlDbType.Int);
            parMaBill.Value = maBill;
            SqlParameter parmaMon  = new SqlParameter("@maMon", SqlDbType.Int);
            parmaMon.Value = maMon;
            SqlParameter parSL = new SqlParameter("@soLuong", SqlDbType.Int);
            parSL.Value = soLuong;

            cmd.Parameters.Add(parMaBill);
            cmd.Parameters.Add(parmaMon);
            cmd.Parameters.Add(parSL);

            cmd.Connection = Sql_Connection.Instance.sqlCon;

            int k = cmd.ExecuteNonQuery();
            if (k<0)
                return false;
            return true;


        }


    }

}
