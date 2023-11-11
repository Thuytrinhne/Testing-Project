using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public  class Bill_InforBLL
    {
        public bool  themMonAnChoBill(int maBill, int maFood, int soLuong)
        {
            if (Bill_InforDAL.Instance.themMonAnChoBill(maBill, maFood, soLuong))
                return true;
            return false;


        }
        public DataTable getBill_Infor (int maBill)
        {
            DataTable dt = Bill_InforDAL.Instance.getBill_Infor(maBill);
            return dt;
        }
        public bool capNhapSoLuong(int maBill, int maMon, int soLuong)
        {
            return Bill_InforDAL.Instance.capNhapSoLuong(maBill, maMon, soLuong);
        }

    }
}
