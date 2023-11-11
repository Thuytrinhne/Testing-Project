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
        int actual = BillDAL.hienThiTongDanhThu(DateTime.Now, DateTime.Now);

        //compare
        Assert.AreEqual(1000, actual);
    }
    #endregion

    // Tương tự, bạn có thể viết các hàm kiểm tra cho các phương thức khác của lớp BillDAL
    // như capNhatDiscount, getSizeOfBill, getDiscount, huyBill, xoaBill_Infor, HienThiDoanhThuForReport
}
