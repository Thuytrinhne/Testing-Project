using BLL;
using DAL;
using DTO;
using Moq;
using System.Data;
using System.Reflection;
using System.Xml.Linq;

namespace QuanLyQuanCafeTest.BLLTest
{
    public class TableBLLTest
    {
        private Mock<TableDAL> _mockTableDAL;
        private TableBLL _tableBLL;

        [SetUp]
        public void Init()
        {
            _mockTableDAL = new Mock<TableDAL>();

            _tableBLL = new TableBLL();
            // mock singleton
            var instanceField = typeof(TableDAL).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockTableDAL.Object);
        }
        #region danhSachBanAn
        [Test]
        public void TableBLL_danhSachBanAn()
        {
            // setup method 

            List<string> _listTable = new List<string>()
            {
                "Bàn 1", "Bàn 2","Bàn 3"
            };
            _mockTableDAL.Setup(m => m.danhSachBanAn()).Returns(_listTable);

            // call action
            var actual = _tableBLL.danhSachTable();
            // compare
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count);

        }
        #endregion
        #region themRow
        [Test]
        [TestCase("Bàn 200", "Trống")]
        public void TableBLL_themRow_ShouldReturnTrueForDistinguishTable(string name, string status)
        {
            // setup method
            TableDTO tableDTO = new TableDTO();
            tableDTO.Name = name;
            tableDTO.Status = status;
            _mockTableDAL.Setup(m => m.themRow(tableDTO)).Returns(true);
            // call action
            var actual = _tableBLL.themRow(tableDTO);
            //compare
            Assert.AreEqual(true, actual);


        }
        [Test]
        [TestCase("Bàn 2", "Trống")]
        public void TableBLL_themRow_ShouldReturnTrueForNotDistinguishTable(string name, string status)
        {
            // setup method
            TableDTO tableDTO = new TableDTO();
            tableDTO.Name = name;
            tableDTO.Status = status;
            _mockTableDAL.Setup(m => m.themRow(tableDTO)).Returns(false);
            // call action
            var actual = _tableBLL.themRow(tableDTO);
            //compare
            Assert.AreEqual(false, actual);


        }
        #endregion
        #region chinhSuaRow
        [Test]
        [TestCase("Bàn 170", "Trống")]
        public void TableBLL_chinhSuaRow_ShouldReturnTrue(string name, string status)
        {
            // setup method
            TableDTO tableDTO = new TableDTO();
            tableDTO.Name = name;
            tableDTO.Status = status;
            _mockTableDAL.Setup(m => m.chinhSuaRow(tableDTO)).Returns(true);
            // call action
            var actual = _tableBLL.chinhSuaRow(tableDTO);
            //compare
            Assert.AreEqual(true, actual);
        }
        [Test]
        [TestCase("Bài 0", "Trống")]
        public void TableBLL_chinhSuaRow_ShouldReturnTr(string name, string status)
        {
            // setup method
            TableDTO tableDTO = new TableDTO();
            tableDTO.Name = name;
            tableDTO.Status = status;
            _mockTableDAL.Setup(m => m.chinhSuaRow(tableDTO)).Returns(false);
            // call action
            var actual = _tableBLL.chinhSuaRow(tableDTO);
            //compare
            Assert.AreEqual(false, actual);
        }
        #endregion
        #region xoaRow
        [Test]
        [TestCase(28)]
        public void TableBLL_xoaRow_ShouldReturnTrue(int ma)
        {
            // setup method

            _mockTableDAL.Setup(m => m.xoaRow(ma)).Returns(true);
            // call action
            var actual = _tableBLL.xoaRow(ma);
            //compare
            Assert.AreEqual(true, actual);
        }
        [Test]
        [TestCase(0)]
        public void TableBLL_xoaRow_ShouldReturnFalse(int ma)
        {
            // setup method
            _mockTableDAL.Setup(m => m.xoaRow(ma)).Returns(false);
            // call action
            var actual = _tableBLL.xoaRow(ma);
            //compare
            Assert.AreEqual(false, actual);
        }
        #endregion
        #region xuLyThemTable
        [Test]
        [TestCase("", false)]
        [TestCase("Bàn 1", false)]
        [TestCase("Bàn 5", true)]
        public void TableBLL_xuLyThemTable(string name, bool expected)
        {
            // setup method
            List<string> _listTable = new List<string>()
            {
                "Bàn 1", "Bàn 2","Bàn 3"
            };
            _mockTableDAL.Setup(m => m.danhSachBanAn()).Returns(_listTable);
            // call action
            string error = null;
            bool actual = _tableBLL.xuLyThemTable(name, ref error);

            // compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region xuLyChinhSuaTable
        [Test]
        [TestCase(1, "", false)]
        [TestCase(1, "Bàn 1", false)]
        [TestCase(1, "Bàn 2", false)]
        [TestCase(1, "Bàn 5", true)]

        public void TableBLL_xuLyChinhSuaTable(int id, string name, bool expectd)
        {
            // setup method
            _mockTableDAL.Setup(m => m.timNameTableByIDTable(1)).Returns("Bàn 1");
            List<string> _listTable = new List<string>()
            {
                "Bàn 1", "Bàn 2","Bàn 3"
            };
            _mockTableDAL.Setup(m => m.danhSachBanAn()).Returns(_listTable);
            // call action
            string error = null;
            bool actual = _tableBLL.xuLyChinhSuaTable(id, name, ref error);
            // compare
            Assert.AreEqual(expectd, actual);
        }
        #endregion
        #region  danhSachIDTable
        [Test]
        public void TableBLL_danhSachIDTable()
        {
            // setup method
            List<int> listIDTable = new List<int>()
            {
                1,2,3,4,5,6,7,
            };
            _mockTableDAL.Setup(m => m.danhSachIDTable()).Returns(listIDTable);
            // call action 
            List<int> list = _tableBLL.danhSachIDTable();
            // compare
            Assert.AreEqual(7, list.Count);
        }
        #endregion
        #region TimKiemBanAn
        [Test]
        [TestCase("Bàn")]
        public void TableBLL_TimKiemBanAn(string keyword)
        {
            // setup method
            // setup method 

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

            _mockTableDAL.Setup(m => m.timKiemTable(keyword)).Returns(dataTable);
            // call action 
            int actual = _tableBLL.TimKiemBanAn(keyword).Rows.Count;
            // compare
            Assert.AreEqual(2, actual);
        }
        #endregion
        #region HienThiTable
        [Test]
        public void TableBLL_HienThiTable()
        {

            // setup method 
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã bàn ăn", typeof(int));
            dataTable.Columns.Add("Tên bàn ăn", typeof(string));
            dataTable.Columns.Add("Trạng thái", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["Mã bàn ăn"] = 1;
            row1["Tên bàn ăn"] = "Bàn 0";
            row1["Trạng thái"] = "Trống";
            dataTable.Rows.Add(row1);
         
            _mockTableDAL.Setup(m => m.HienThiTable()).Returns(dataTable);

            // call action
            var actual = _tableBLL.HienThiTable();
            // compare
            
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Rows.Count);
        }
        #endregion
    }
}
