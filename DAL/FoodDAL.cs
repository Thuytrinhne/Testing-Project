using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.DataProviders;
using DTO;

namespace DAL
{
    public  class FoodDAL
    {
        private FoodDAL() { }   
        private static FoodDAL instance= null;
        public static FoodDAL Instance
        {  get { if (instance == null) instance = new FoodDAL(); return instance; } 
           set { instance = value; } 
        }   

        public List <FoodDTO> getFoodByCateGory (int ma)
        {
            List <FoodDTO> l = new List <FoodDTO>();
            DataTable dataTable = new DataTable();
            string q = "select * from food where idCategory = " + ma;
            dataTable = FoodDataProvider.Instance.excecuteQuerry(q); ;
            foreach (DataRow row in dataTable.Rows)
            {
                DataRow dr = row;
                FoodDTO f = new FoodDTO(row);
                l.Add(f);

            }

            return l;

        }
        public DataTable hienThiDanhSachFood()
        {
            string q = "select f.id [Mã món ăn], f.name [Tên món ăn], f.price [Giá], c.name [Loại] from food f\r\ninner join FOOD_CATEGORY c\r\non c.id = f.idCategory";
            return FoodDataProvider.Instance.excecuteQuerry(q);

        }
        public bool themRow(FoodDTO foodDTO)
        {
            try
            {
                string q =  "insert into food values ( @name, @price, @loai)";
                return FoodDataProvider.Instance.executeInsertQuery(q, foodDTO);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }




        }
       public  List <string> danhSachFood()
        {
            string q = "select * from food";
            DataTable ds = FoodDataProvider.Instance.excecuteQuerry(q);

            List<string> l = new List<string>();
            foreach (DataRow dr in ds.Rows)
            {
                l.Add(dr["name"].ToString().Trim());
            }
            return l;


        }
        public bool chinhSuaRow(FoodDTO foodDTO)
        {
            try
            {
                string q = "update Food\r\nset \r\n\tname = @name,\r\n\tprice = @price ,\r\n\tidCategory = @idLoai \r\nwhere id = @id";
                // kt naem. status trc khi truyen vao 
                return FoodDataProvider.Instance.executeUpdateQuery(q, foodDTO);
            }
            catch { throw new Exception(); }


        }

        public string timNameFoodByIDFood (int idFood) {
            DataTable dt = new DataTable();
            dt = FoodDataProvider.Instance.excecuteQuerry("select * from Food where id = "+ idFood);
            DataRow dataRow = dt.Rows[0];
            return dataRow["name"].ToString().Trim();   
        
        }
        public DataTable TimKiemMonAn(string n)
        {
            
            return FoodDataProvider.Instance.executeSearchStoreProcedure(n);


        }
        public static bool xoaMonAn( int id)
        {
            try
            {
                string q = "delete from food where id = @id" ;
                return FoodDataProvider.Instance.executeDeleteQuery(q, id);
            }
            catch
            {
                return false;
            }




        }
    }
}
