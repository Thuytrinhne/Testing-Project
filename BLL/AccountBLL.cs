using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Xml.Linq;

namespace BLL
{
    public class AccountBLL
    {
       public  bool xuLyLogin(string username, string password)
        {
            if (username.Trim().Length==0 || password.Trim().Length ==  0) return false;
            return true;
        }
        public bool Login (string username,  string password, ref Account a) { 
        
        return LoginDAL.Login(username, password,ref a);
        }
        public DataTable hienThiDanhSachTaiKhoan()
        {
            DataTable kq = new DataTable();
            kq.Columns.Add("Mã tài khoản", typeof(Int32));
            kq.Columns.Add("Tên hiển thị", typeof(string));
            kq.Columns.Add("Username", typeof(string));
            kq.Columns.Add("Loại tài khoản", typeof(string));
            


            DataTable dt = LoginDAL.Instance.hienThiDanhSachTaiKhoan();
            for (int i = 0;i< dt.Rows.Count;i++)
            {
              DataRow dr = dt.Rows[i];
                DataRow k = kq.NewRow();
                k["Mã tài khoản"] = dr[0];
                k["Tên hiển thị"] = dr[1];
                k["Username"] = dr[2];
                if ((int)dr["Loại tài khoản"]==1 )
                {
                 
                    k["Loại tài khoản"] = "Admin";


                }    
                else
                {
                    k["Loại tài khoản"] = "Staff";
                }    
                kq.Rows.Add(k);

            }
            return kq;



        }
        public static bool xuLyThemAccount(string username,string displayName,  ref string er)
        {
            if (displayName == "")
            {
                er = "Chưa nhập tên hiển thị";
                return false;
            }


            if (username == "")
            {
                er = "Chưa nhập username";
                return false;
            }
            List<string> t = danhSachUserName();

            foreach (string v in t)
            {
                if (username == v)
                {
                    er = "Username đã tồn tại trong danh sách";
                    return false;
                }

            }
            



            return true;
        }

        public static  List<string> danhSachUserName()
        {
            return LoginDAL.danhSachUserName();


        }
        public  bool themRow(Account d)
        {
            return LoginDAL.themRow(d);
        }
       public static  bool  xoaRow(int ma)
        {

            return LoginDAL.xoaRow(ma);

        }
       public  static bool xuLyChinhSuaAccount(int id,string  username,string  displayname, ref string  er)
        {

            if (username == "")
            {
                er = "Chưa nhập username";
                return false;
            }
            List<string> t = danhSachUserName();

            string nameCu = LoginDAL.timUserNameByIDAccount(id);
            if (nameCu != username)
            {

                foreach (string v in t)
                {
                    if (username == v)
                    {
                        er = "Tên username đã tồn tại trong danh sách";
                        return false;
                    }

                }
            }
            if (displayname == "")
            {
                er = "Chưa nhập tên hiển thị";
                return false;
            }
            return true;
        }
       public static bool  chinhSuaRow(Account d)
        {
            return LoginDAL.chinhSuaRow(d);

        }
       public static DataTable TimKiemTaiKhoan(string name)
        {

           return  LoginDAL.timKiemTaiKhoan(name);

        }
        public static bool ktCurPass(string username, string password)
        {
           return  LoginDAL.ktCurPass(username, password);
        }
        public static bool DoiPassword( string username, string password)
        {

            return LoginDAL.DoiPassword(username, password);


        }
    }
}
