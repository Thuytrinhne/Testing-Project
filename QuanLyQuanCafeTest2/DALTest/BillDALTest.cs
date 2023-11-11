using DAL.DataProviders;
using DAL;
using Moq;
using System.Data;
using System.Reflection;
using QuanLyQuanCafeTest.Help;

public class BillDALTest
{
    private Mock<BillDataProvider> _mockDataProvider;

    [SetUp]
    public void Init()
    {
        _mockDataProvider = new Mock<BillDataProvider>();
        mockSingleTon();
    }

    private void mockSingleTon()
    {
      
        var instanceField = typeof(BillDataProvider).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
        instanceField.SetValue(null, _mockDataProvider.Object);
    }

    #region GetUncheckBillByTable
    [Test]
    [TestCase(1, 1)]
    [TestCase(2, -1)]
    public void testGetUncheckBillByTable(int id, int expected)
    {
        // setup method
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("id", typeof(int));
        if(expected == 1)
        {
            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = id;
            dataTable.Rows.Add(row1);
        }
        

        _mockDataProvider.Setup(m => m.executeSearchStoreProcedure(id)).Returns(dataTable);

        // call action
        int actual = BillDAL.Instance.getUncheckBillByTable(id);

        //compare
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region ThucHienCheckOut
    [Test]
    //[TestCase(1, true)]
    [TestCase(2, false)]
    public void testThucHienCheckOut(int maBan, bool expected)
    {
        // setup method
        _mockDataProvider.Setup(m => m.executeCheckoutStoreProcedure(maBan)).Returns(expected);

        TestableBillDAL testableBillDAL = new TestableBillDAL();

        if (expected)
        {
            testableBillDAL.ReturnValueForGetUncheckBillByTable = 1;
        }
        else
        {
            testableBillDAL.ReturnValueForGetUncheckBillByTable = 1;
        }

        // call action
        bool actual = BillDAL.Instance.thucHienCheckOut(maBan);

        //compare
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region ThemBill
    [Test]
    [TestCase(1, true)]
    [TestCase(2, false)]
    public void testThemBill(int maBan, bool expected)
    {
        // setup method
        _mockDataProvider.Setup(m => m.executeInsertQuery(It.IsAny<int>())).Returns(expected);
      
        // call action
        bool actual = BillDAL.Instance.themBill(maBan);

        //compare
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region ChuyenBan
    [Test]
    [TestCase(1, 2, true)]
    [TestCase(2, 3, false)]
    public void testChuyenBan(int maBill, int maBanNew, bool expected)
    {
        // setup method
        _mockDataProvider.Setup(m => m.executeMoveTableQuery(It.IsAny<int>(), It.IsAny<int>())).Returns(expected);

        // call action
        bool actual = BillDAL.Instance.ChuyenBan(maBill, maBanNew);

        //compare
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region HienThiDoanhThu
    [Test]
    public void testHienThiDoanhThu()
    {
        // setup method
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("dateCheckIn", typeof(DateTime));
        dataTable.Columns.Add("dateCheckOut", typeof(DateTime));
        dataTable.Columns.Add("status", typeof(int));
        dataTable.Columns.Add("discount", typeof(float));

        // Thêm dữ liệu vào DataTable
        DataRow row1 = dataTable.NewRow();
        row1["id"] = 1;
        row1["id"] = 1;
        row1["dateCheckIn"] = DateTime.Now;
        row1["dateCheckOut"] = DateTime.Now;
        row1["status"] = 1;
        row1["discount"] = 0.1f;
        dataTable.Rows.Add(row1);

        _mockDataProvider.Setup(m => m.executeReportPaginateQuery(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(dataTable);

        // call action
        DataTable actual = BillDAL.Instance.HienThiDoanhThu(1, DateTime.Now, DateTime.Now);

        //compare
        Assert.AreEqual(1, actual.Rows.Count);
    }
    #endregion

    #region HienThiTongDanhThu
    [Test]
    public void testHienThiTongDanhThu()
    {
        // setup method
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Tổng", typeof(int));

        // Thêm dữ liệu vào DataTable
        DataRow row1 = dataTable.NewRow();
        row1["Tổng"] = 1000;
        dataTable.Rows.Add(row1);

        _mockDataProvider.Setup(m => m.executeTotalReport(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(dataTable);

        // call action
        int actual = BillDAL.Instance.hienThiTongDanhThu(DateTime.Now, DateTime.Now);

        //compare
        Assert.AreEqual(1000, actual);
    }
    #endregion
    #region CapNhatDiscount
    [Test]
    [TestCase(1, 0.1, true)]
    [TestCase(2, 0.2, false)]
    public void TestCapNhatDiscount(int maBill, decimal discount, bool expected)
    {
        // setup method
        _mockDataProvider.Setup(m => m.executeDiscountQuery (maBill, discount)).Returns(expected);

        // call action
        bool actual = BillDAL.Instance.capNhatDiscount(maBill, discount);

        // compare
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetSizeOfBill
    [Test]
    public void TestGetSizeOfBill_ReturnsCountOfBills_WhenExecuteScalarReturnsCount()
    {
        // setup method
        DateTime dateStart = new DateTime(2022, 1, 1);
        DateTime dateEnd = new DateTime(2022, 1, 31);
        string expectedQuery = "select count(id) from bill where (dateCheckIn between '2022-01-01' and '2022-01-31 23:59:59') and status = 1";
        int expectedCount = 10;
        _mockDataProvider.Setup(m => m.executeScalar(expectedQuery)).Returns(expectedCount);

        // call action
        int actual = BillDAL.Instance.getSizeOfBill(dateStart, dateEnd);

        // compare
        Assert.AreEqual(expectedCount, actual);
    }
    #endregion

    #region GetDiscount
    [Test]
    public void TestGetDiscount_ReturnsDiscount_WhenExecuteScalarReturnsDiscount()
    {
        // setup method
        int maBill = 1;
        string expectedQuery = "select discount from bill where id = 1";
        int expectedDiscount = 4;
        _mockDataProvider.Setup(m => m.executeScalar(expectedQuery)).Returns(expectedDiscount);

        // call action
        int actual = BillDAL.Instance.getDiscount(maBill);

        // compare
        Assert.AreEqual(expectedDiscount, actual);
    }
    #endregion

    #region HuyBill
    [Test]
    public void TestHuyBill_ReturnsFalse_WhenXoaBill_InforReturnsFalse()
    {
        // setup method
        int maBill = 1;
        string expectedDeleteQuery = "delete from Bill where id = @ma";
        _mockDataProvider.Setup(m => m.executeDeleteQuery(expectedDeleteQuery, maBill)).Returns(true);
        _mockDataProvider.Setup(m => m.executeDeleteQuery(It.IsAny<string>(), maBill)).Returns(false);

        // call action
        bool actual = BillDAL.Instance.huyBill(maBill);

        // compare
        Assert.IsFalse(actual);
    }
    #endregion

    #region HienThiDoanhThuForReport
    [Test]
    public void TestHienThiDoanhThuForReport_ReturnsDataTable_WhenExecuteSalesReportReturnsDataTable()
    {
        // setup method
        DateTime dateStart = new DateTime(2022, 1, 1);
        DateTime dateEnd = new DateTime(2022, 1, 31);
        DataTable expectedDataTable = new DataTable();
        _mockDataProvider.Setup(m => m.executeSalesReport(dateStart, dateEnd)).Returns(expectedDataTable);

        // call action
        DataTable actual = BillDAL.Instance.HienThiDoanhThuForReport(dateStart, dateEnd);

        // compare
        Assert.AreEqual(expectedDataTable, actual);
    }
    #endregion

}
