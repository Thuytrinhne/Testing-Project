using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using DAL.DataProviders;
using DTO;



namespace DAL
{
    public class TableDAL 
    {

        protected TableDAL()
        {
            // Khởi tạo logic
        }
        private static TableDAL instance = null;
        public static TableDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableDAL();
                }
                return instance;
            }
            private set
            { TableDAL.instance = value; }
        }

        public virtual List<int> danhSachIDTable()
        {
            string q = "select id from table_food";
            DataTable ds = DataProvider.Instance.excecuteQuerry(q);

            List<int> l = new List<int>();
            foreach (DataRow dr in ds.Rows)
            {
                l.Add(int.Parse(dr["id"].ToString()));
            }



            return l;

        }
        public virtual List<string> danhSachBanAn ()
        {
            string q = "select * from table_food";
            DataTable ds =DataProvider.Instance.excecuteQuerry(q);
            List<string> l = new List<string>();
            foreach (DataRow dr in ds.Rows)
            {
                l.Add(dr["name"].ToString().Trim());
            }
            return l;

        }
        public virtual bool themRow(TableDTO tableDTO)
        {
            try
            {
               
                string q = "insert into table_food values ( @name, @status)";
                bool kq = TableDataProvider.Instance.executeInsertQuery(q, tableDTO);
                if (kq)
                    return true;
                else
                    return false;
                
            }catch(Exception ex) {

                throw new Exception();
            }


        }
        public virtual bool xoaRow(int ma)
        {
            try
            {
               
                string q = "delete from Table_food where id = @ma";
                bool kq = TableDataProvider.Instance.executeDeleteQuery(q, ma);


                if (kq)
                    return true;
                else
                    return false;

            }
            catch
            {
                throw new Exception();
            } 
            

        }
        public virtual bool chinhSuaRow (TableDTO tableDTO)
        {
            try
            {
                string q = "update Table_Food\r\nset \r\n\tname = @name,\r\n\tstatus = @status\r\nwhere id = @id";
                bool kq = TableDataProvider.Instance.executeUpdateQuery(q, tableDTO);
                if (kq)
                    return true;
                else
                    return false;
               
            } catch { throw new Exception(); }


        }

        public virtual DataTable timKiemTable (string n)
        {

           string q = "select id as [Mã bàn ăn] , name as [Tên bàn ăn], status as [Trạng thái] from table_food where name like '%" + n + "%'";
           DataTable data = TableDataProvider.Instance.excecuteQuerry(q);
            return data;
        }

        public virtual string timNameTableByIDTable(int idTable)
        {
            DataTable dt = new DataTable();
            dt = TableDataProvider.Instance.excecuteQuerry("select * from Table_Food where id = " + idTable );
            DataRow dataRow = dt.Rows[0];
            return dataRow["name"].ToString().Trim();

        }
        public virtual DataTable HienThiTable()
        {
            DataTable dt = new DataTable();
            dt = TableDataProvider.Instance.excecuteQuerry("select id as [Mã bàn ăn] ,name as [Tên bàn ăn] ,status as [Trạng thái]from table_food");
            return dt;

        }

    }
}
