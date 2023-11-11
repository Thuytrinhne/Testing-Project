using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL
{
    public class FoodBLL
    {
        public List<FoodDTO> getFoodByCateGory(int ma)
        {
            return FoodDAL.Instance.getFoodByCateGory(ma);


        }
        public DataTable hienThiDanhSachFood()
        {
            return FoodDAL.Instance.hienThiDanhSachFood();
       }
       public bool  xuLyThemFood( string  name, string price , ref string  er)
        {
           
            if (name=="")
            {
                er = "Chưa nhập tên món ăn";
                return false;
            }
            List<string> t = danhSachFood();
            // tìm name by idGia 
            

                foreach (string v in t)
                {
                    string name1 = v.Trim();

                    if (name == name1)
                    {
                        er = "Tên bàn ăn đã tồn tại trong danh sách";
                        return false;
                    }


                }
            
            double p;
            if (!double.TryParse(price, out p ))
            {
                er = "Giá không đúng định dạng";
                return false;
            }    
            if (p < 0)
            {
                er = "Giá không được nhỏ hơn 0";
                return false;

            }
            return true;
        }
        public bool themRow(FoodDTO a)
        {
            return FoodDAL.Instance.themRow(a);


        }
        public bool xuLyChinhSuaFood(int id ,string name , string  price, ref string  er)
        {
            if (name == "")
            {
                er = "Chưa nhập tên món ăn";
                return false;
            }
            List<string> t = danhSachFood();
            // tìm name by idGia 
            string nameCu  = FoodDAL.Instance.timNameFoodByIDFood(id).Trim();
            if (nameCu != name)
            {

                foreach (string v in t)
                {
                    string name1 = v.Trim();

                    if (name == name1)
                    {
                        er = "Tên bàn ăn đã tồn tại trong danh sách";
                        return false;
                    }


                }
            }
            double p;



            if (!double.TryParse(price , out p))
            {
                er = "Giá chưa đúng định dạng";
                return false;
            }    
            if (p<0)
            {
                er = "Giá không được nhỏ hơn 0";
                return false;
            }    

            return true;

        }
        public List<string> danhSachFood()
        {
            List<string> i = FoodDAL.Instance.danhSachFood();
            return i;


        }
        public bool chinhSuaRow(FoodDTO a)
        {
            return FoodDAL.Instance.chinhSuaRow(a);
       }
        public DataTable TimKiemMonAn(string n)
        {
            return FoodDAL.Instance.TimKiemMonAn(n);

        }
        public static bool xoaMonAn(int id)
        {
            return FoodDAL.xoaMonAn(id);
        }
    }
}
