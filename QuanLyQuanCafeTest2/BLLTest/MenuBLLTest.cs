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
    public class MenuBLLTest
    {
        private Mock<MenuDAL> _mockMenuDAL;
        private MenuBLL _menuBLL;

        [SetUp]
        public void Init()
        {
            _mockMenuDAL = new Mock<MenuDAL>();

            _menuBLL = new MenuBLL();
            // mock singleton
            var instanceField = typeof(MenuDAL).GetField("Instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockMenuDAL.Object);
        }

        [Test]
        public void MenuBLL_hienThiMenu()
        {
            // setup method
            int idT = 1;
            int idBill = 0;
            DataTable expectedDataTable = new DataTable();
            _mockMenuDAL.Setup(m => m.hienThiMenu(idT, ref idBill)).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = _menuBLL.hienThiMenu(idT, ref idBill);
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
            Assert.AreEqual(0, idBill);
        }

        [Test]
        public void MenuBLL_hienThiMenuByIDBill()
        {
            // setup method
            int idBill = 1;
            DataTable expectedDataTable = new DataTable();
            _mockMenuDAL.Setup(m => m.hienThiMenuByIDBill(idBill)).Returns(expectedDataTable);
            // call action
            DataTable actualDataTable = _menuBLL.hienThiMenuByIDBill(idBill);
            // compare
            Assert.AreEqual(expectedDataTable, actualDataTable);
        }
    }
}
