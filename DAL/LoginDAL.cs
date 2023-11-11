using DAL.DataProviders;
using DTO;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginDAL
    {
        private LoginDAL() { }
        private static LoginDAL instance = null;
        public static LoginDAL Instance
        {


            get { if (instance == null)
                    instance = new LoginDAL();
                return instance; }
            private set { instance = value; }
        }
        public static  string maHoaMatKhau(string password)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";
            foreach (byte b in hashData)
            {
                hasPass += b;
            }
            return hasPass;
        }
      
        public static  bool Login(string username,  string password, ref Account a)
        {
            #region ma hoa mat khau 
            string hasPass=maHoaMatKhau(password);

            #endregion
            string q = "select * from account  where userName = @user and passWord = @pass";

            DataTable dt = LoginDataProvider.Instance.executeLoginQuery(q, username, hasPass);


            if (dt.Rows.Count == 0) {

                return false;
                

            }
            DataRow dr = dt.Rows[0];
            if (dr["userName"].ToString().Trim() == username && dr["passWord"].ToString().Trim() == hasPass) {
                a.ID = (int)dr["id"];
                a.UserName = dr["userName"].ToString();
                a.DisplayName = dr["displayName"].ToString();
                a.TypeAccount = (int)dr["typeAccount"];


                return true;
            }
            return false;


        }
        public static bool ktCurPass(string username, string password)
        {
            string hasPass = maHoaMatKhau(password);

            string q = "select * from account  where userName = @user and passWord = @pass";

            DataTable t = LoginDataProvider.Instance.executeLoginQuery(q, username, hasPass);
            if (t.Rows.Count == 0)
            {
                return false;
            }
            return true;

        }
        public DataTable hienThiDanhSachTaiKhoan()
        {
            DataTable dt = new DataTable();
            string q = "select id [Mã tài khoản], displayName [Tên hiển thị], userName [Username] ,typeAccount [Loại tài khoản] from account";
            dt = LoginDataProvider.Instance.excecuteQuerry(q);
            return dt;
        }
        public static List <string> danhSachUserName()
        {
          string q = "select * from account";
          DataTable ds = LoginDataProvider.Instance.excecuteQuerry(q);

        List<string> l = new List<string>();


            foreach (DataRow dr in ds.Rows)
            {
                l.Add(dr["username"].ToString().Trim());
            }



            return l;

        }

        public static bool themRow(Account account)
        {
            try
            {
                string pass= maHoaMatKhau("0");
                account.Password = pass;
                string q =  "insert into account values (@user,@display,@pass,@type)";
                return LoginDataProvider.Instance.executeInsertQuery(q, account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }
        public static bool xoaRow(int ma)
        {
            try
            {
                string q = "delete from account where id = @ma";
                return LoginDataProvider.Instance.executeDeleteQuery(q,ma);
            }
            catch
            {
                throw new Exception();
            }


        }
        public static  string timUserNameByIDAccount(int id)
        {
            DataTable dt = new DataTable();
            dt = LoginDataProvider.Instance.excecuteQuerry("select * from account where id = " + id);
            DataRow dataRow = dt.Rows[0];
            return dataRow["username"].ToString().Trim();

        }
        public static bool chinhSuaRow(Account account)
        {
            try
            {
                string q =  "update account \r\nset \r\n\tusername = @name,\r\n\tdisplayname = @dis ,\r\n\ttypeAccount = @type \r\nwhere id = @id";
                return LoginDataProvider.Instance.executeUpdateQuery(q, account);
                
            }
            catch { throw new Exception(); }



        }
        public  static DataTable timKiemTaiKhoan(string displayName)
        {
            string q = "select id [Mã tài khoản], displayName [Tên hiển thị], userName [Username] ,typeAccount [Loại tài khoản] from account where displayName like '%" + displayName + "%'";
            return LoginDataProvider.Instance.excecuteQuerry(q); 

        }
        public static bool DoiPassword(string username, string password)
        {
            string hasPass = maHoaMatKhau(password);
            try
            {
                string q = "update account \r\nset \r\n\tpassword = @pass\r\nwhere username = @username";
                return LoginDataProvider.Instance.executeUpdateQuery(q, username, hasPass);
            }
            catch { throw new Exception(); }

        }

    }
}
