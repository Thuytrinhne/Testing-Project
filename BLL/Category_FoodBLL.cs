using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;
using System.Diagnostics;
using DTO;

namespace BLL
{
    public  class Category_FoodBLL
    {
        public DataTable hienThiDanhSachFoodCategory()
        {
            return CategoryDAL.Instance.hienThiDanhSachFoodCategory();
        }
       public  bool xuLyThemCategoryFood(string name, ref string er)
        {
            if (name == "")
            {
                er = "Chưa nhập tên danh mục";
                return false;
            }
            List<string> t = danhSachTenDanhMuc();

            foreach (string v in t)
            {
                if (name == v)
                {
                    er = "Tên danh mục đã tồn tại trong danh sách";
                    return false;
                }

            }
            return true;

        }

        private List<string> danhSachTenDanhMuc()
        {

            return CategoryDAL.Instance.danhSachCategory();


        }
        public bool themRow(string name)
        {
            return CategoryDAL.Instance.themRow(name);
        }
        public bool xuLyChinhSuaCategoryFood(int id ,string name,  ref string er)
        {
            if (name == "")
            {
                er = "Chưa nhập tên loại món ăn";
                return false;
            }
            List<string> t = danhSachTenDanhMuc();
            // tìm name by idGia 
            string nameCu =CategoryDAL.Instance.timNameCateGory_FoodByID(id).Trim();
            if (nameCu != name)
            {

                foreach (string v in t)
                {
                    string name1 = v.Trim();
                    if (name == name1)
                    {
                        er = "Tên loại món ăn đã tồn tại trong danh sách";
                        return false;
                    }


                }
            }
          
            return true;
        }


        public bool chinhSuaRow(CategoryDTO a)
        {
            return CategoryDAL.Instance.chinhSuaRow(a);
        }
        public DataTable TimKiemLoaiMonAn(string n)
        {
            return CategoryDAL.Instance.TimKiemLoaiMonAn(n);


        }
        public static bool xoaLoaiMonAn(int id)
        {
            return CategoryDAL.xoaLoaiMonAn(id);
        }
    }
}
