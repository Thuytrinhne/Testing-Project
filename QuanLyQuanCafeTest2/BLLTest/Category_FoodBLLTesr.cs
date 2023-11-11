using BLL;
using DAL;
using DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafeTest.BLLTest
{
    [TestFixture]
    public class Category_FoodBLLTest
    {
        private Mock<CategoryDAL> _mockCategoryDAL;
        private Category_FoodBLL _category_FoodBLL;

        [SetUp]
        public void Init()
        {
            _mockCategoryDAL = new Mock<CategoryDAL>();

            _category_FoodBLL = new Category_FoodBLL();
            // mock singleton
            var instanceField = typeof(CategoryDAL).GetField("Instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockCategoryDAL.Object);
        }

        [Test]
        public void Category_FoodBLL_hienThiDanhSachFoodCategory()
        {
            // setup method
            DataTable expectedDataTable = new DataTable();
            _mockCategoryDAL.Setup(m => m.hienThiDanhSachFoodCategory()).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = _category_FoodBLL.hienThiDanhSachFoodCategory();
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }

        [Test]
        public void Category_FoodBLL_xuLyThemCategoryFood()
        {
            // setup method
            string name = "Category Name";
            string er = "";
            // call action
            bool actual = _category_FoodBLL.xuLyThemCategoryFood(name, ref er);
            // compare
            Assert.IsTrue(actual);
            Assert.AreEqual("", er);
        }

        [Test]
        public void Category_FoodBLL_themRow()
        {
            // setup method
            string name = "Category Name";
            _mockCategoryDAL.Setup(m => m.themRow(name)).Returns(true);
            // call action
            bool actual = _category_FoodBLL.themRow(name);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void Category_FoodBLL_xuLyChinhSuaCategoryFood()
        {
            // setup method
            int id = 1;
            string name = "Category Name";
            string er = "";
            // call action
            bool actual = _category_FoodBLL.xuLyChinhSuaCategoryFood(id, name, ref er);
            // compare
            Assert.IsTrue(actual);
            Assert.AreEqual("", er);
        }

        [Test]
        public void Category_FoodBLL_chinhSuaRow()
        {
            // setup method
            CategoryDTO categoryDTO = new CategoryDTO();
            _mockCategoryDAL.Setup(m => m.chinhSuaRow(categoryDTO)).Returns(true);
            // call action
            bool actual = _category_FoodBLL.chinhSuaRow(categoryDTO);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void Category_FoodBLL_TimKiemLoaiMonAn()
        {
            // setup method
            string n = "Search Query";
            DataTable expectedDataTable = new DataTable();
            _mockCategoryDAL.Setup(m => m.TimKiemLoaiMonAn(n)).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = _category_FoodBLL.TimKiemLoaiMonAn(n);
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }

        [Test]
        public void Category_FoodBLL_xoaLoaiMonAn()
        {
            // setup method
            int id = 1;
            _mockCategoryDAL.Setup(m => m.xoaLoaiMonAn(id)).Returns(true);
            // call action
            bool actual = Category_FoodBLL.xoaLoaiMonAn(id);
            // compare
            Assert.IsTrue(actual);
        }
    }
}
