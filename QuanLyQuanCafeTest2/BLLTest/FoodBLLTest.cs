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
    public class FoodBLLTest
    {
        private Mock<FoodDAL> _mockFoodDAL;
        private FoodBLL _foodBLL;

        [SetUp]
        public void Init()
        {
            _mockFoodDAL = new Mock<FoodDAL>();

            _foodBLL = new FoodBLL();
            // mock singleton
            var instanceField = typeof(FoodDAL).GetField("Instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockFoodDAL.Object);
        }

        [Test]
        public void FoodBLL_getFoodByCateGory()
        {
            // setup method
            int ma = 1;
            List<FoodDTO> expectedFoodList = new List<FoodDTO>();
            _mockFoodDAL.Setup(m => m.getFoodByCateGory(ma)).Returns(expectedFoodList);
            // call action
            List<FoodDTO> actualFoodList = _foodBLL.getFoodByCateGory(ma);
            // compare
            Assert.AreEqual(expectedFoodList, actualFoodList);
        }

        [Test]
        public void FoodBLL_hienThiDanhSachFood()
        {
            // setup method
            DataTable expectedDataTable = new DataTable();
            _mockFoodDAL.Setup(m => m.hienThiDanhSachFood()).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = _foodBLL.hienThiDanhSachFood();
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }

        [Test]
        public void FoodBLL_xuLyThemFood()
        {
            // setup method
            string name = "Food Name";
            string price = "10.5";
            string er = "";
            // call action
            bool actual = _foodBLL.xuLyThemFood(name, price, ref er);
            // compare
            Assert.IsTrue(actual);
            Assert.AreEqual("", er);
        }

        [Test]
        public void FoodBLL_themRow()
        {
            // setup method
            FoodDTO foodDTO = new FoodDTO();
            _mockFoodDAL.Setup(m => m.themRow(foodDTO)).Returns(true);
            // call action
            // call action
            bool actual = _foodBLL.themRow(foodDTO);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void FoodBLL_xuLyChinhSuaFood()
        {
            // setup method
            int id = 1;
            string name = "Food Name";
            string price = "10.5";
            string er = "";
            // call action
            bool actual = _foodBLL.xuLyChinhSuaFood(id, name, price, ref er);
            // compare
            Assert.IsTrue(actual);
            Assert.AreEqual("", er);
        }

        [Test]
        public void FoodBLL_chinhSuaRow()
        {
            // setup method
            FoodDTO foodDTO = new FoodDTO();
            _mockFoodDAL.Setup(m => m.chinhSuaRow(foodDTO)).Returns(true);
            // call action
            bool actual = _foodBLL.chinhSuaRow(foodDTO);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void FoodBLL_TimKiemMonAn()
        {
            // setup method
            string n = "Search Query";
            DataTable expectedDataTable = new DataTable();
            _mockFoodDAL.Setup(m => m.TimKiemMonAn(n)).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = _foodBLL.TimKiemMonAn(n);
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }

        [Test]
        public void FoodBLL_xoaMonAn()
        {
            // setup method
            int id = 1;
            _mockFoodDAL.Setup(m => m.xoaMonAn(id)).Returns(true);
            // call action
            bool actual = FoodBLL.xoaMonAn(id);
            // compare
            Assert.IsTrue(actual);
        }
    }
}
