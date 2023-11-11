using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public  class MenuBLL
    {
        public DataTable hienThiMenu(int idT, ref int idBill)
        {
            int idB=-1;

            DataTable dt = new DataTable();
            dt = MenuDAL.Instance.hienThiMenu(idT, ref idB);
            idBill = idB;
            return dt;

        }
        public DataTable hienThiMenuByIDBill(int idBill)
        {
      

            DataTable dt = new DataTable();
            dt = MenuDAL.Instance.hienThiMenuByIDBill(idBill);
            return dt;

        }
    }
}
