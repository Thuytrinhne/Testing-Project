using DAL.DataProviders;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public  class CategoryDAL
    {
        private CategoryDAL() { }
        private static CategoryDAL instance= null;
        public static CategoryDAL Instance { get { if (instance == null) instance = new CategoryDAL(); return instance; } 
        private set { instance = value; }
        
        }
        public DataTable hienThiDanhSachFoodCategory()
        {
            string q = "select id [Mã danh mục], name [Tên danh mục] from food_category";
            return CategoryDataProvider.Instance.excecuteQuerry(q);
        }
        public List<string> danhSachCategory()
        {
            string q = "select * from food_category";
            DataTable ds = CategoryDataProvider.Instance.excecuteQuerry(q);

            List<string> l = new List<string>();



            foreach (DataRow dr in ds.Rows)
            {
                l.Add(dr["name"].ToString().Trim());
            }



            return l;

        }

       public bool themRow(string name)
        {
            try
            {
               CategoryDTO categoryDTO  = new CategoryDTO() { Name= name};
                string q = "insert into food_category values ( @name)";
                bool kq = CategoryDataProvider.Instance.executeInsertQuery(q, categoryDTO);
                if (kq)
                    return true;
                else
                    return false;

               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
       public string timNameCateGory_FoodByID(int id)
        {
            DataTable dt = new DataTable();
            dt = CategoryDataProvider.Instance.excecuteQuerry("select * from food_category where id = " + id);
            DataRow dataRow = dt.Rows[0];
            return dataRow["name"].ToString().Trim();


        }
       public bool chinhSuaRow(CategoryDTO categoryDTO)
        {
            try
            {
                
                string q = "update  food_category\r\nset \r\n\tname = @name\r\nwhere id = @id";
                bool kq = CategoryDataProvider.Instance.executeUpdateQuery(q, categoryDTO);
                if (kq)
                    return true;
                else
                    return false;
            }
            catch { throw new Exception(); }

        }
       public DataTable TimKiemLoaiMonAn(string name)
        {
           return  CategoryDataProvider.Instance.executeSearchStoreProcedure(name);
        }
       public static bool xoaLoaiMonAn(int id)
        {
            try
            {
             

                string q = "delete from food_category where id = @ma";
                bool kq = CategoryDataProvider.Instance.executeDeleteQuery(q, id);


                if (kq)
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }




        }
    }
}
