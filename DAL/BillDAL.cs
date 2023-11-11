using DAL.DataProviders;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public  class BillDAL
    {
        protected BillDAL() { }
        private static BillDAL instance = null;
        

        public static BillDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAL();
                }
                return instance;
            }
            private set
            { BillDAL.instance = value; }
        }


        // tìm mã bill chưa thanh toán khi biết bàn 

        public virtual int  getUncheckBillByTable (int id)
        {
            int ma;
            DataTable t = BillDataProvider.Instance.executeSearchStoreProcedure(id);
            // kiểm tra liệu có null 

            if (t.Rows.Count > 0)
            {
                DataRow dr = t.Rows[0];
                ma = (int)dr["id"];
                return ma;
            }

            return -1;
        }
       public bool thucHienCheckOut(int maBan)
        {
            int maBill = getUncheckBillByTable(maBan);
            if (maBill ==-1) { return false; }
            return BillDataProvider.Instance.executeCheckoutStoreProcedure(maBill);

        }
       public bool themBill(int maBan)
        {
            try
            {
                return BillDataProvider.Instance.executeInsertQuery(maBan);
            } catch
            {
                return false;
            }

        }
        public bool ChuyenBan(int maBill, int maBanNew )
        {
            return BillDataProvider.Instance.executeMoveTableQuery(maBill, maBanNew);
        }

        public DataTable HienThiDoanhThu(int page, DateTime dateStart, DateTime dateEnd)
        {
           
            return BillDataProvider.Instance.executeReportPaginateQuery(page, dateStart, dateEnd);

        }
        public static int  hienThiTongDanhThu(DateTime dateStart,DateTime dateEnd)
        {

            DataTable d = BillDataProvider.Instance.executeTotalReport(dateStart, dateEnd);

            DataRow dr = d.Rows[0];
            if ( dr.IsNull("Tổng")  )
            {
                return 0;
            }

            return int.Parse(dr["Tổng"].ToString());



        }




        public bool capNhatDiscount(int maBill,decimal  discount)
        {
            Sql_Connection.Instance.openCon();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "capNhatDiscount";

            SqlParameter parid = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter parDis = new SqlParameter("@discount", SqlDbType.Int);
            parid.Value = maBill;
            parDis.Value =(int) discount;

            cmd.Parameters.Add(parid);
            cmd.Parameters.Add(parDis);
            cmd.Connection= Sql_Connection.Instance.sqlCon;

            int kq = cmd.ExecuteNonQuery();
            if (kq<0)
            {
                return false;
            }
            return true;


        }
        public static int getSizeOfBill(DateTime dateStart, DateTime dateEnd)
        {
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            String dateStartString = dateStart.ToString("yyyy-MM-dd");
            String dateEndString = dateEnd.ToString("yyyy-MM-dd")+" 23:59:59";
            //string query = string.Format("select count(id) from bill where dateCheckIn between {0} and {1}", dateStart, dateEnd);
            cmd.CommandText = "select count(id) from bill where (dateCheckIn between '" + dateStartString  + "' and '" + dateEndString+"') and status = 1";
            
            cmd.Connection = Sql_Connection.Instance.sqlCon;

           int kq = (int)cmd.ExecuteScalar();
           return kq;

        }
        public int getDiscount (int maBill)
        {
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select discount from bill where id = " + maBill;
            cmd.Connection = Sql_Connection.Instance.sqlCon;


            int kq =int.Parse(cmd.ExecuteScalar().ToString().Trim());

            return kq;


        }
        public bool huyBill(int maBill)
        {
            // xóa bill infor
            



            if (!xoaBill_Infor(maBill))
                return false;
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Bill where id = " + maBill;
            cmd.Connection = Sql_Connection.Instance.sqlCon;

            int kq = cmd.ExecuteNonQuery();
            if (kq < 0) { return false; }
            return true;



        }

        private bool xoaBill_Infor(int maBill)
        {
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Bill_infor where idBill = " + maBill;
            cmd.Connection = Sql_Connection.Instance.sqlCon;

            int kq = cmd.ExecuteNonQuery();
           if (kq<0) { return false; }
            return true;


        }
        public static  DataTable HienThiDoanhThuForReport( DateTime dateStart, DateTime dateEnd)
        {
            DataTable dataTable = new DataTable();
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HienThiDoanhThuForReport";
            SqlParameter parD1 = new SqlParameter("@dateStart", SqlDbType.Date);
            SqlParameter parD2 = new SqlParameter("@dateEnd", SqlDbType.Date);

            parD1.Value = dateStart;
            parD2.Value = dateEnd;
            cmd.Parameters.Add(parD1);
            cmd.Parameters.Add(parD2);

            cmd.Connection = Sql_Connection.Instance.sqlCon;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            return dataTable;


        }

    }
}
