using DAL;
using DAL.DataProviders;
using DTO;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace QuanLyQuanCafeTest.DALTest
{
    public class FoodDALTest
    {
        private Mock<FoodDataProvider> _mockDataProvider;

        [SetUp]
        public void Init()
        {
            _mockDataProvider = new Mock<FoodDataProvider>();
            mockSingleTon();
        }

        private void mockSingleTon()
        {
            var instanceField = typeof(FoodDataProvider).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockDataProvider.Object);
        }
        private static IEnumerable<TestCaseData> FoodDTOTestCases
        {
            get
            {
                yield return new TestCaseData(new FoodDTO() { TenMon = "Mì xào", Gia = 50000, Loai = 1 }, true);
                yield return new TestCaseData(new FoodDTO() { TenMon = "Nước coke", Gia = 50000, Loai = 1 }, false);

                // Thêm các trường hợp kiểm tra khác tại đây
            }
        }
        #region testGetFoodByCategory
        [Test] 
        [TestCase(1,2)]
        public void testGetFoodByCategory(int idCategory, int expected)
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("price", typeof(double));
            dataTable.Columns.Add("idCategory", typeof(int));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = 1;
            row1["name"] = "Mì xào";
            row1["price"] = 50000;
            row1["idCategory"] = idCategory;
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();

            row2["id"] = 2;
            row2["name"] = "Mì xào";
            row2["price"] = 50000;
            row2["idCategory"] = idCategory;
            dataTable.Rows.Add(row2);

            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            List<FoodDTO> actual = FoodDAL.Instance.getFoodByCateGory(idCategory);

            //compare
            Assert.AreEqual(expected, actual.Count);
        }
        #endregion
        #region HienThiDanhSachFood
        [Test] 
        public void testHienThiDanhSachFood()
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã món ăn", typeof(int));
            dataTable.Columns.Add("Tên món ăn", typeof(string));
            dataTable.Columns.Add("Giá", typeof(double));
            dataTable.Columns.Add("Loại", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["Mã món ăn"] = 1;
            row1["Tên món ăn"] = "Mì xào";
            row1["Giá"] = 50000;
            row1["Loại"] = "Mì";
            dataTable.Rows.Add(row1);

            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            DataTable actual = FoodDAL.Instance.hienThiDanhSachFood();

            //compare
            Assert.AreEqual(1, actual.Rows.Count);
        }
        #endregion
        #region themRow
        [Test, TestCaseSource(nameof(FoodDTOTestCases))]
        public void testThemRow(FoodDTO foodDTO, bool expected)
        {
            // setup method
            _mockDataProvider.Setup(m => m.executeInsertQuery(It.IsAny<string>(), It.IsAny<FoodDTO>())).Returns(expected);

            // call action
            bool actual = FoodDAL.Instance.themRow(foodDTO);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region DanhSachFood    
        [Test]
        public void testDanhSachFood()
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("price", typeof(double));
            dataTable.Columns.Add("idCategory", typeof(int));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = 1;
            row1["name"] = "Mì xào";
            row1["price"] = 50000;
            row1["idCategory"] = 1;
            dataTable.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            List<string> actual = FoodDAL.Instance.danhSachFood();

            //compare
            Assert.AreEqual(1, actual.Count);
        }
        #endregion
        #region ChinhSuaRow
        [Test, TestCaseSource(nameof(FoodDTOTestCases))]
        public void testChinhSuaRow(FoodDTO foodDTO, bool expected)
        {
            // setup method
            _mockDataProvider.Setup(m => m.executeUpdateQuery(It.IsAny<string>(), foodDTO)).Returns(expected);

            // call action
            bool actual = FoodDAL.Instance.chinhSuaRow(foodDTO);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region TimNameFoodByIDFood
        [Test]
        [TestCase(1)]
        public void testTimNameFoodByIDFood(int id)
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("name", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = id;
            row1["name"] = "Mì xào";
            dataTable.Rows.Add(row1);

            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            string actual = FoodDAL.Instance.timNameFoodByIDFood(id);

            //compare
            Assert.AreEqual("Mì xào", actual);
        }
        #endregion
        #region TimKiemMonAn
        [Test]
        [TestCase("Mì xào", 1)]
        public void testTimKiemMonAn(string name,int expected)
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã món ăn", typeof(int));
            dataTable.Columns.Add("Tên món ăn", typeof(string));
            dataTable.Columns.Add("Giá", typeof(double));
            dataTable.Columns.Add("Loại", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["Mã món ăn"] = 1;
            row1["Tên món ăn"] = name;
            row1["Giá"] = 50000;
            row1["Loại"] = "Mì";
            dataTable.Rows.Add(row1);
            
            _mockDataProvider.Setup(m => m.executeSearchStoreProcedure(It.IsAny<string>())).Returns(dataTable);

            // call action
            DataTable actual = FoodDAL.Instance.TimKiemMonAn("Mì xào");

            //compare
            Assert.AreEqual(expected, actual.Rows.Count);
        }
        #endregion
        #region XoaMonAn
        [Test]
        [TestCase(1,true)]
        [TestCase(2, false)]

        public void testXoaMonAn(int id, bool expected)
        {
            // setup method
            _mockDataProvider.Setup(m => m.executeDeleteQuery(It.IsAny<string>(), id)).Returns(expected);

            // call action
            bool actual = FoodDAL.xoaMonAn(id);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}