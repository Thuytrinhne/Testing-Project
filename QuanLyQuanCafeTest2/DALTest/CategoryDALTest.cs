using DAL;
using DAL.DataProviders;
using DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafeTest.DALTest
{
    public class CategoryDALTest
    {
        private Mock<CategoryDataProvider> _mockDataProvider;
        [SetUp]
        public void Init()
        {
            _mockDataProvider = new Mock<CategoryDataProvider>();
            // mock Singleton
            mockSingleTon();

        }

        private void mockSingleTon()
        {
            var instanceField = typeof(CategoryDataProvider).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockDataProvider.Object);

        }
        #region hienThiDanhSachFoodCategory
        [Test]
        public void testHienThiDanhSachFoodCategory()
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã danh mục", typeof(int));
            dataTable.Columns.Add("Tên danh mục", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["Mã danh mục"] = 1;
            row1["Tên danh mục"] = "Hải sản";
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["Mã danh mục"] = 2;
            row2["Tên danh mục"] = "Đồ nướng";
            dataTable.Rows.Add(row2);
            string q = "select id [Mã danh mục], name [Tên danh mục] from food_category";
            _mockDataProvider.Setup(m => m.excecuteQuerry(q)).Returns(dataTable);

            // call action
            DataTable actual = CategoryDAL.Instance.hienThiDanhSachFoodCategory();
            //compare
            Assert.AreEqual(2, actual.Rows.Count);
        }
        #endregion
        #region danhSachCategory
        [Test]
        public void testdanhSachCategory()
        {
            // setup method 
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("name", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = 1;
            row1["name"] = "Hải sản";
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["id"] = 2;
            row2["name"] = "Đồ nướng";
            dataTable.Rows.Add(row2);

            string q = "select * from food_category";
            _mockDataProvider.Setup(m => m.excecuteQuerry(q)).Returns(dataTable);

            // call action
            List<string> listTable = CategoryDAL.Instance.danhSachCategory();
            //compare
            Assert.AreEqual(2, listTable.Count);
        }
        #endregion
        #region themRow
        [Test]
        [TestCase("Hải sản", false)]
        [TestCase("ngọc", true)]
        public void testThemRow(string name, bool expected)
        {

            // setup method

            _mockDataProvider.Setup(m => m.executeInsertQuery(It.IsAny<string>(), It.IsAny<CategoryDTO>())).Returns(expected);

            // call action
            bool actual = CategoryDAL.Instance.themRow(name);
            //compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region TimNameCateGory_FoodByID
        [Test]
        [TestCase(1, "Hải sản")]
        public void testTimNameCateGory_FoodByID(int id, string expected)
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("name", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = 1;
            row1["name"] = "Hải sản";
            dataTable.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            string actual = CategoryDAL.Instance.timNameCateGory_FoodByID(id);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region chinhSuaRow
        [Test]
        [TestCase(1, "Hải sản", false)]
        [TestCase(2, "Nước nóng", true)]
        public void testchinhSuaRow(int id, string name, bool expected)
        {

            // setup method
            CategoryDTO categoryDTO = new CategoryDTO() {Id= id,  Name = name};
            _mockDataProvider.Setup(m => m.executeUpdateQuery(It.IsAny<string>(), It.IsAny<CategoryDTO>())).Returns(expected);

            // call action
            bool actual = CategoryDAL.Instance.chinhSuaRow(categoryDTO);
            //compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region TimKiemLoaiMonAn
        [Test]
        [TestCase("Nước ép")]
        public void testTimKiemLoaiMonAn(string name)
        {

            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã danh mục", typeof(int));
            dataTable.Columns.Add("Tên danh mục", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["Mã danh mục"] = 1;
            row1["Tên danh mục"] = "Nước ép";
            dataTable.Rows.Add(row1);
            

            _mockDataProvider.Setup(m => m.executeSearchStoreProcedure(name)).Returns(dataTable);

            // call action
            DataTable actual = CategoryDAL.Instance.TimKiemLoaiMonAn(name);
            //compare
            Assert.AreEqual(1, actual.Rows.Count);


        }
        #endregion
        #region xoaLoaiMonAn
        [Test]
        [TestCase(50, true)]
        [TestCase(1, false)]

        public void testxoaLoaiMonAn(int ma, bool expected)
        {
            // setup method
            string q = "delete from food_category where id = @ma";
            _mockDataProvider.Setup(m => m.executeDeleteQuery(It.IsAny<string>(), It.IsAny<int>())).Returns(expected);
            // call action 
            bool actual = CategoryDAL.xoaLoaiMonAn(ma);
            // compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
    }
}


