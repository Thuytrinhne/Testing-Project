using DAL;
using DTO;
using Moq;
using NUnit.Framework.Constraints;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using DAL.DataProviders;

namespace QuanLyQuanCafe.DALTest
{

    public class TableDALTest
    {
        private Mock<TableDataProvider> _mockDataProvider;

        [SetUp]
        public void Init() {
            _mockDataProvider = new Mock<TableDataProvider>();
            // mock Singleton
            mockSingleTon();

        }

        private void mockSingleTon()
        {
            var instanceField = typeof(TableDataProvider).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockDataProvider.Object);

        }
        #region themRow
        [Test]
        [TestCase("Bàn 0", "Trống", false)]
        [TestCase("Bàn 207", "Trống", true)]
        public void testThemRow(string name, string status, bool expected)
        {

            // setup method
            TableDTO tableDTO = new TableDTO() { Name = name, Status = status };
            string q = "insert into table_food values ( @name, @status)";
            _mockDataProvider.Setup(m => m.executeInsertQuery(q, tableDTO)).Returns(expected);

            // call action
            bool actual = TableDAL.Instance.themRow(tableDTO);
            //compare
            Assert.AreEqual(expected, actual);

        }

        #endregion
        #region danhsachbanan
        [Test]
        public void testDanhSachBanAn()
        {
            // setup method 
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("status", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = 1;
            row1["name"] = "Bàn 0";
            row1["status"] = "Trống";
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["id"] = 2;
            row2["name"] = "Bàn 1";
            row2["status"] = "Trống";
            dataTable.Rows.Add(row2);

            string q = "select * from table_food";
            _mockDataProvider.Setup(m => m.excecuteQuerry(q)).Returns(dataTable);

            // call action
            List<string> listTable = TableDAL.Instance.danhSachBanAn();
            //compare
            Assert.AreEqual(2, listTable.Count);
        }
        #endregion
        #region danhSachIDTable
        [Test]
        public void testDanhSachIDTable()
        {
            // setup method 
            // setup method 
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = 1;

            dataTable.Rows.Add(row1);
            string q = "select id from table_food";
            _mockDataProvider.Setup(m => m.excecuteQuerry(q)).Returns(dataTable);

            // call action
            List<int> kq = TableDAL.Instance.danhSachIDTable();
            //compare
            Assert.IsNotNull(kq);
            Assert.AreEqual(1, kq.Count);
        }

        #endregion
        #region xoaRow
        [Test]
        [TestCase(20, true)]
        [TestCase(1, false)]

        public void testXoaRow(int ma, bool expected)
        {
            // setup method
            string q = "delete from Table_food where id = @ma";
            _mockDataProvider.Setup(m => m.executeDeleteQuery(q, ma)).Returns(expected);
            // call action 
            bool actual = TableDAL.Instance.xoaRow(ma);
            // compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region chinhSuaRow
        [Test]
        [TestCase(1, "Bàn 1", "Trống", false)]
        [TestCase(2, "Bàn 207", "Trống", true)]
        public void testchinhSuaRow(int id, string name, string status, bool expected)
        {

            // setup method
            TableDTO tableDTO = new TableDTO() { Name = name, Status = status };
            string q = "update Table_Food\r\nset \r\n\tname = @name,\r\n\tstatus = @status\r\nwhere id = @id";
            _mockDataProvider.Setup(m => m.executeUpdateQuery(q, tableDTO)).Returns(expected);

            // call action
            bool actual = TableDAL.Instance.chinhSuaRow(tableDTO);
            //compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region timKiemTable
        [Test]
        [TestCase("Bàn")]
        public void testTimKiemTable(string n)
        {
          
                // setup method
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Mã bàn ăn", typeof(int));
                dataTable.Columns.Add("Tên bàn ăn", typeof(string));
                dataTable.Columns.Add("Trạng thái", typeof(string));

                // Thêm dữ liệu vào DataTable
                DataRow row1 = dataTable.NewRow();
                row1["Mã bàn ăn"] = 1;
                row1["Tên bàn ăn"] = "Bàn 2";
                row1["Trạng thái"] = "Trống";
                dataTable.Rows.Add(row1);
                DataRow row2 = dataTable.NewRow();
                row2["Mã bàn ăn"] = 1;
                row2["Tên bàn ăn"] = "Bàn 2";
                row2["Trạng thái"] = "Trống";
                dataTable.Rows.Add(row2);

            string q = "select id as [Mã bàn ăn] , name as [Tên bàn ăn], status as [Trạng thái] from table_food where name like '%" + n + "%'";
                _mockDataProvider.Setup(m => m.excecuteQuerry(q)).Returns(dataTable);

                // call action
                DataTable actual  = TableDAL.Instance.timKiemTable(n);
                //compare
                Assert.AreEqual(2, actual.Rows.Count);

            
        }
        #endregion
        #region timNameTableByIDTable
        [Test]
        [TestCase("1")]

        public void testimNameTableByIDTable(int idTable)
        {

            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("status", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = 1;
            row1["name"] = "Bàn 1";
            row1["status"] = "Trống";
            dataTable.Rows.Add(row1);
            

            _mockDataProvider.Setup(m => m.excecuteQuerry("select * from Table_Food where id = " + idTable)).Returns(dataTable);

            // call action
           string actual = TableDAL.Instance.timNameTableByIDTable(idTable);
            //compare
            Assert.AreEqual("Bàn 1", actual);


        }
        #endregion
        #region HienThiTable
        [Test]
        public void testHienThiTable()
        {

            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã bàn ăn", typeof(int));
            dataTable.Columns.Add("Tên bàn ăn", typeof(string));
            dataTable.Columns.Add("Trạng thái", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["Mã bàn ăn"] = 1;
            row1["Tên bàn ăn"] = "Bàn 2";
            row1["Trạng thái"] = "Trống";
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["Mã bàn ăn"] = 2;
            row2["Tên bàn ăn"] = "Bàn 2";
            row2["Trạng thái"] = "Trống";
            dataTable.Rows.Add(row2);

            _mockDataProvider.Setup(m => m.excecuteQuerry("select id as [Mã bàn ăn] ,name as [Tên bàn ăn] ,status as [Trạng thái]from table_food")).Returns(dataTable);

            // call action
            DataTable actual = TableDAL.Instance.HienThiTable();
            //compare
            Assert.AreEqual(2, actual.Rows.Count);


        }
        #endregion
    }
}
