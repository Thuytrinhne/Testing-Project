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
        public  int  hienThiTongDanhThu(DateTime dateStart,DateTime dateEnd)
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
            

            bool kq = BillDataProvider.Instance.executeDiscountQuery(maBill, discount);
            if (kq)
            {
                return true;
            }
            return false;


        }
        public  int getSizeOfBill(DateTime dateStart, DateTime dateEnd)
        {
            String dateStartString = dateStart.ToString("yyyy-MM-dd");
            String dateEndString = dateEnd.ToString("yyyy-MM-dd") + " 23:59:59";
            string q = "select count(id) from bill where (dateCheckIn between '" + dateStartString + "' and '" + dateEndString + "') and status = 1";
            return BillDataProvider.Instance.executeScalar(q);
            

        }
        public int getDiscount (int maBill)
        {
       
 
            string q = "select discount from bill where id = " + maBill;
            return BillDataProvider.Instance.executeScalar(q);

        }
        public bool huyBill(int maBill)
        {
            // xóa bill infor
            string q = "delete from Bill where id = @ma" ;
            if (!xoaBill_Infor(maBill))
                return false;
            return BillDataProvider.Instance.executeDeleteQuery(q, maBill);



        }

        private bool xoaBill_Infor(int maBill)
        {

            string q = "delete from Bill_infor where idBill = @ma";
            return BillDataProvider.Instance.executeDeleteQuery(q, maBill);



        }
        public   DataTable HienThiDoanhThuForReport( DateTime dateStart, DateTime dateEnd)
        {
           return BillDataProvider.Instance.executeSalesReport(dateStart, dateEnd);


        }

    }
}
