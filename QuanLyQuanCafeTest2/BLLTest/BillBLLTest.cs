using BLL;
using DAL;
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
    public class BillBLLTest
    {
        private Mock<BillDAL> _mockBillDAL;
        private BillBLL _billBLL;

        [SetUp]
        public void Init()
        {
            _mockBillDAL = new Mock<BillDAL>();

            _billBLL = new BillBLL();
            // mock singleton
            var instanceField = typeof(BillDAL).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockBillDAL.Object);
        }

        [Test]
        public void BillBLL_thucHienCheckOut()
        {
            // setup method
            int maBan = 1;
            _mockBillDAL.Setup(m => m.thucHienCheckOut(maBan)).Returns(true);
            // call action
            bool actual = _billBLL.thucHienCheckOut(maBan);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void BillBLL_getIdBillByIdTable()
        {
            // setup method
            int idTable = 1;
            int expected = 2;
            _mockBillDAL.Setup(m => m.getUncheckBillByTable(idTable)).Returns(expected);
            // call action
            int actual = _billBLL.getIdBillByIdTable(idTable);
            // compare
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BillBLL_themBill()
        {
            // setup method
            int maBan = 1;
            _mockBillDAL.Setup(m => m.themBill(maBan)).Returns(true);
            // call action
            bool actual = _billBLL.themBill(maBan);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void BillBLL_ChuyenBan()
        {
            // setup method
            int maBill = 1;
            int maBanNew = 2;
            _mockBillDAL.Setup(m => m.ChuyenBan(maBill, maBanNew)).Returns(true);
            // call action
            bool actual = _billBLL.ChuyenBan(maBill, maBanNew);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void BillBLL_HienThiDoanhThu()
        {
            // setup method
            int page = 1;
            DateTime dateStart = new DateTime(2022, 1, 1);
            DateTime dateEnd = new DateTime(2022, 1, 31);
            DataTable expectedDataTable = new DataTable();
            _mockBillDAL.Setup(m => m.HienThiDoanhThu(page, dateStart, dateEnd)).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = BillBLL.HienThiDoanhThu(page, dateStart, dateEnd);
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }

        [Test]
        public void BillBLL_capNhatDiscount()
        {
            // setup method
            int maBill = 1;
            decimal discount = 0.1m;
            _mockBillDAL.Setup(m => m.capNhatDiscount(maBill, discount)).Returns(true);
            // call action
            bool actual = _billBLL.capNhatDiscount(maBill, discount);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void BillBLL_getDiscount()
        {
            // setup method
            int maBill = 1;
            int expectedDiscount = 10;
            _mockBillDAL.Setup(m => m.getDiscount(maBill)).Returns(expectedDiscount);
            // call action
            int actualDiscount = _billBLL.getDiscount(maBill);
            // compare
            Assert.AreEqual(expectedDiscount, actualDiscount);
        }

        [Test]
        public void BillBLL_huyBill()
        {
            // setup method
            int maBill = 1;
            _mockBillDAL.Setup(m => m.huyBill(maBill)).Returns(true);
            // call action
            bool actual = _billBLL.huyBill(maBill);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void BillBLL_getSizeOfBill()
        {
            // setup method
            DateTime dateStart = new DateTime(2022, 1, 1);
            DateTime dateEnd = new DateTime(2022, 1, 31);
            int expectedSize = 10;
            _mockBillDAL.Setup(m => m.getSizeOfBill(dateStart, dateEnd)).Returns(expectedSize);
            // call action
            int actualSize = BillBLL.getSizeOfBill(dateStart, dateEnd);
            // compare
            Assert.AreEqual(expectedSize, actualSize);
        }

        [Test]
        public void BillBLL_hienThiTongDanhThu()
        {
            // setup method
            DateTime dateStart = new DateTime(2022, 1, 1);
            DateTime dateEnd = new DateTime(2022, 1, 31);
            int expectedTotalRevenue = 1000;
            _mockBillDAL.Setup(m => m.hienThiTongDanhThu(dateStart, dateEnd)).Returns(expectedTotalRevenue);
            // call action
            int actualTotalRevenue = BillBLL.hienThiTongDanhThu(dateStart, dateEnd);
            // compare
            Assert.AreEqual(expectedTotalRevenue, actualTotalRevenue);
        }

        [Test]
        public void BillBLL_HienThiDoanhThuForReport()
        {
            // setup method
            DateTime dateStart = new DateTime(2022, 1, 1);
            DateTime dateEnd = new DateTime(2022, 1, 31);
            DataTable expectedDataTable = new DataTable();
            _mockBillDAL.Setup(m => m.HienThiDoanhThuForReport(dateStart, dateEnd)).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = BillBLL.HienThiDoanhThuForReport(dateStart, dateEnd);
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }
    }
}
