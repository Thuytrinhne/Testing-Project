using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class BillBLL
    {
        public bool thucHienCheckOut(int maBan)
        {
            return BillDAL.Instance.thucHienCheckOut(maBan);
        }
        public int getIdBillByIdTable (int idTable)
        {

            return BillDAL.Instance.getUncheckBillByTable(idTable);

        }
        public bool themBill(int maBan)
        {
            return BillDAL.Instance.themBill(maBan);
        }
       public bool ChuyenBan(int maBill,int  maBanNew )
        {
            return BillDAL.Instance.ChuyenBan(maBill,maBanNew);

        }
        public static  DataTable HienThiDoanhThu(int page, DateTime dateStart, DateTime dateEnd)
        {
            return BillDAL.Instance.HienThiDoanhThu(page, dateStart,dateEnd);

        }
        public bool capNhatDiscount(int maBill, decimal discount)
        {
            return BillDAL.Instance.capNhatDiscount(maBill,discount);
        }
        public int getDiscount(int maBill)
        {
            return BillDAL.Instance.getDiscount(maBill);
        }
        public bool huyBill(int maBill)
        {
            return BillDAL.Instance.huyBill(maBill);
        }
        public static int getSizeOfBill(DateTime dateStart, DateTime dateEnd)
        {
            return BillDAL.getSizeOfBill(dateStart, dateEnd);
        }
        public static int hienThiTongDanhThu(DateTime dateStart, DateTime dateEnd)
        {
            return BillDAL.hienThiTongDanhThu(dateStart, dateEnd);
        }
        public static  DataTable HienThiDoanhThuForReport(DateTime dateStart, DateTime dateEnd)
        {
            return BillDAL.HienThiDoanhThuForReport(dateStart, dateEnd);
        }
    }
}
