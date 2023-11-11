using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{

    // login 

    public class TableBLL
    {
        public static int tableHeight = 100;
        public static int tableWidth = 100;
        public  DataTable HienThiTable()
        {
            return TableDAL.Instance.HienThiTable();
        }
        

        public bool themRow(TableDTO a)
        {
           if ( TableDAL.Instance.themRow(a))
            {
                return true;
            }    
           else
           
           return false;

        }
        public bool chinhSuaRow(TableDTO a)
        {
            if (TableDAL.Instance.chinhSuaRow(a))
            {
                return true;
            }
            else
                return false;

        }
        public bool xoaRow (int ma)
        {
            return TableDAL.Instance.xoaRow(ma) == true? true: false;

        }
        public bool xuLyThemTable (string name , ref string er)
        {
            if (name =="")
            {
                er = "Chưa nhập tên bàn";
                return false;
            }
            List<string> t = danhSachTable();

            foreach (string v in t)
            {
                string name1 = v.Trim();

                if (name  == name1)
                {
                    er = "Tên bàn ăn đã tồn tại trong danh sách";
                    return false;
                }

            }

            return true;
        }

        public bool xuLyChinhSuaTable(int id,  string name, ref string er)
        {
          
            if (name == "")
            {
                er = "Chưa nhập tên bàn";
                return false;
            }
            List<string> t = danhSachTable();

            string nameCu = TableDAL.Instance.timNameTableByIDTable(id);
            if (nameCu != name)
            {

                foreach (string v in t)
                {
                    if (name == v)
                    {
                        er = "Tên bàn ăn đã tồn tại trong danh sách";
                        return false;
                    }

                }
                return true;
            }

            return false;
        }
        public List<int> danhSachIDTable()
        {
            List <int> i = TableDAL.Instance.danhSachIDTable();
            return i;


        }
        public List<string> danhSachTable()
        {
            List<string> i = TableDAL.Instance.danhSachBanAn();
            return i;


        }
        public DataTable TimKiemBanAn (string n)
        {
            return TableDAL.Instance.timKiemTable(n);
        }
    }
}
