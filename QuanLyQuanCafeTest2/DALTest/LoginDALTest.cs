using DAL.DataProviders;
using DAL;
using DTO;
using Moq;
using System.Data;
using System.Reflection;

public class LoginDALTest
{
    private Mock<LoginDataProvider> _mockDataProvider;

    [SetUp]
    public void Init()
    {
        _mockDataProvider = new Mock<LoginDataProvider>();
        mockSingleTon();
    }

    private void mockSingleTon()
    {
        var instanceField = typeof(LoginDataProvider).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
        instanceField.SetValue(null, _mockDataProvider.Object);
    }

    #region Login
    [Test]
    [TestCase("username1", "password1", true)]
    [TestCase("username2", "password2", false)]
    public void testLogin(string username, string password, bool expected)
    {
        // setup method
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("id", typeof(int));
        dataTable.Columns.Add("userName", typeof(string));
        dataTable.Columns.Add("passWord", typeof(string));
        dataTable.Columns.Add("displayName", typeof(string));
        dataTable.Columns.Add("typeAccount", typeof(int));


        if (expected)
        {
            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["id"] = 1;
            row1["userName"] = username;
            row1["passWord"] = LoginDAL.maHoaMatKhau(password);
            row1["displayName"] = "Nguyễn Thùy Trinh";
            row1["typeAccount"] = 1;

            dataTable.Rows.Add(row1);
        }

        _mockDataProvider.Setup(m => m.executeLoginQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(dataTable);

        // call action
        Account a = new Account();
        bool actual = LoginDAL.Login(username, password, ref a);

        //compare
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region KtCurPass
    [Test]
    [TestCase("username1", "password1", true)]
    [TestCase("username2", "password2", false)]
    public void testKtCurPass(string username, string password, bool expected)
    {
        // setup method

        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("userName", typeof(string));
        dataTable.Columns.Add("passWord", typeof(string));
        if (expected)
        {
            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["userName"] = username;
            row1["passWord"] = LoginDAL.maHoaMatKhau(password);
            dataTable.Rows.Add(row1);
        }
        _mockDataProvider.Setup(m => m.executeLoginQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(dataTable);

        // call action
        bool actual = LoginDAL.ktCurPass(username, password);

        //compare
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #region HienThiDanhSachTaiKhoan
    [Test]
    public void testHienThiDanhSachTaiKhoan()
    {
        // setup method
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Mã tài khoản", typeof(int));
        dataTable.Columns.Add("Tên hiển thị", typeof(string));
        dataTable.Columns.Add("Username", typeof(string));
        dataTable.Columns.Add("Loại tài khoản", typeof(int));

        // Thêm dữ liệu vào DataTable
        DataRow row1 = dataTable.NewRow();
        row1["Mã tài khoản"] = 1;
        row1["Tên hiển thị"] = "Người dùng 1";
        row1["Username"] = "username1";
        row1["Loại tài khoản"] = 1;
        dataTable.Rows.Add(row1);

        _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

        // call action
        DataTable actual = LoginDAL.Instance.hienThiDanhSachTaiKhoan();

        //compare
        Assert.AreEqual(1, actual.Rows.Count);
    }
    #endregion

    #region DanhSachUserName
    [Test]
    public void testDanhSachUserName()
    {
        // setup method
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("username", typeof(string));

        // Thêm dữ liệu vào DataTable
        DataRow row1 = dataTable.NewRow();
        row1["username"] = "username1";
        dataTable.Rows.Add(row1);

        _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

        // call action
        List<string> actual = LoginDAL.danhSachUserName();

        //compare
        Assert.AreEqual(1, actual.Count);
    }
    #endregion

    #region ThemRow
    [Test]
    public void testThemRow()
    {
        // setup method
        Account account = new Account() { UserName = "username1", DisplayName = "Người dùng 1", Password = "password1", TypeAccount = 1 };
        _mockDataProvider.Setup(m => m.executeInsertQuery(It.IsAny<string>(), account)).Returns(true);

        // call action
        bool actual = LoginDAL.themRow(account);

        //compare
        Assert.AreEqual(true, actual);
    }
    #endregion

    #region XoaRow
    [Test]
    public void testXoaRow()
    {
        // setup method
        _mockDataProvider.Setup(m => m.executeDeleteQuery(It.IsAny<string>(), 1)).Returns(true);

        // call action
        bool actual = LoginDAL.xoaRow(1);

        //compare
        Assert.AreEqual(true, actual);
    }
    #endregion

    #region TimUserNameByIDAccount
    [Test]
    public void testTimUserNameByIDAccount()
    {
        // setup method
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("username", typeof(string));

        // Thêm dữ liệu vào DataTable
        DataRow row1 = dataTable.NewRow();
        row1["username"] = "username1";
        dataTable.Rows.Add(row1);

        _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

        // call action
        string actual = LoginDAL.timUserNameByIDAccount(1);

        //compare
        Assert.AreEqual("username1", actual);
    }
    #endregion

    #region ChinhSuaRow
    [Test]
    public void testChinhSuaRow()
    {
        // setup method
        Account account = new Account() { ID = 1, UserName = "username1", DisplayName = "Người dùng 1", Password = "password1", TypeAccount = 1 };
        _mockDataProvider.Setup(m => m.executeUpdateQuery(It.IsAny<string>(), account)).Returns(true);

        // call action
        bool actual = LoginDAL.chinhSuaRow(account);

        //compare
        Assert.AreEqual(true, actual);
    }
    #endregion

    #region TimKiemTaiKhoan
    [Test]
    public void testTimKiemTaiKhoan()
    {
        // setup method
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Mã tài khoản", typeof(int));
        dataTable.Columns.Add("Tên hiển thị", typeof(string));
        dataTable.Columns.Add("Username", typeof(string));
        dataTable.Columns.Add("Loại tài khoản", typeof(int));

        // Thêm dữ liệu vào DataTable
        DataRow row1 = dataTable.NewRow();
        row1["Mã tài khoản"] = 1;
        row1["Tên hiển thị"] = "Người dùng 1";
        row1["Username"] = "username1";
        row1["Loại tài khoản"] = 1;
        dataTable.Rows.Add(row1);

        _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

        // call action
        DataTable actual = LoginDAL.timKiemTaiKhoan("Người dùng 1");

        //compare
        Assert.AreEqual(1, actual.Rows.Count);
    }
    #endregion

    #region DoiPassword
    [Test]
    public void testDoiPassword()
    {
        // setup method
        _mockDataProvider.Setup(m => m.executeUpdateQuery(It.IsAny<string>(), "username1", LoginDAL.maHoaMatKhau("password1"))).Returns(true);

        // call action
        bool actual = LoginDAL.DoiPassword("username1", "password1");

        //compare
        Assert.AreEqual(true, actual);
    }
    #endregion

}