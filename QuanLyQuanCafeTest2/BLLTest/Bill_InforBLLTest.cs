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
    public class Bill_InforBLLTest
    {
        private Mock<Bill_InforDAL> _mockBill_InforDAL;
        private Bill_InforBLL _bill_InforBLL;

        [SetUp]
        public void Init()
        {
            _mockBill_InforDAL = new Mock<Bill_InforDAL>();

            _bill_InforBLL = new Bill_InforBLL();
            // mock singleton
            var instanceField = typeof(Bill_InforDAL).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockBill_InforDAL.Object);
        }

        [Test]
        public void Bill_InforBLL_themMonAnChoBill()
        {
            // setup method
            int maBill = 1;
            int maFood = 1;
            int soLuong = 2;
            _mockBill_InforDAL.Setup(m => m.themMonAnChoBill(maBill, maFood, soLuong)).Returns(true);
            // call action
            bool actual = _bill_InforBLL.themMonAnChoBill(maBill, maFood, soLuong);
            // compare
            Assert.IsTrue(actual);
        }

        [Test]
        public void Bill_InforBLL_getBill_Infor()
        {
            // setup method
            int maBill = 1;
            DataTable expectedDataTable = new DataTable();
            _mockBill_InforDAL.Setup(m => m.getBill_Infor(maBill)).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = _bill_InforBLL.getBill_Infor(maBill);
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }

        [Test]
        public void Bill_InforBLL_capNhapSoLuong()
        {
            // setup method
            int maBill = 1;
            int maMon = 1;
            int soLuong = 2;
            _mockBill_InforDAL.Setup(m => m.capNhapSoLuong(maBill, maMon, soLuong)).Returns(true);
            // call action
            bool actual = _bill_InforBLL.capNhapSoLuong(maBill, maMon, soLuong);
            // compare
            Assert.IsTrue(actual);
        }
    }
}
