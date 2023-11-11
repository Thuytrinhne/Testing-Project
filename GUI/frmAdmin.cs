using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using System.Globalization;
using System.Xml.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

namespace GUI
{
    public partial class frmAdmin : Form
    {
        public static DateTime start;
        public static DateTime end;
        int currentPage = 1;
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void hienThiComboStatus()
        {
            throw new NotImplementedException();
        }

        private void hienThiDanhSachTable()
        {
            TableBLL t = new TableBLL();
            DataTable l = t.HienThiTable();

            dtgBan.DataSource = l;
        }
        int kt = -1;

        private void frmAdmin_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyQuanCafeDataSet2.FOOD_CATEGORY' table. You can move, or remove it, as needed.
            this.fOOD_CATEGORYTableAdapter.Fill(this.quanLyQuanCafeDataSet2.FOOD_CATEGORY);
            hienThiDanhSachTable();
            hienThiDanhSachFoodCategory();
            hienThiDanhSachAccount();
            hienThiDanhSachFood();
            hienThiDoanhThu(currentPage, DateTime.Now, DateTime.Now);
            loadCmbLoaiMonAn();
            hienThiCmbAccountType();
            txtTimL_Leave( sender,  e);
            txtTimF_Leave(sender, e);
            txtTimTK_Leave(sender, e);
            txtTimB_Leave (sender, e);

        }
        private void hienThiCmbAccountType()
        {
            DataTable d = new DataTable();
            d.Columns.Add("id", typeof(int));
            d.Columns.Add("name", typeof(string));

            DataRow r = d.NewRow();

            r["name"] = "Staff";
            r["id"] = 0;
            d.Rows.Add(r);

            DataRow r1 = d.NewRow();
            r1["name"] = "Admin";
            r1["id"] = 1;
            d.Rows.Add(r1);

            cmbLoaiTK.DisplayMember = "name";
            cmbLoaiTK.ValueMember = "id";
            cmbLoaiTK.DataSource = d;

        }

        private void hienThiDanhSachFoodCategory()
        {
            Category_FoodBLL f = new Category_FoodBLL();
            DataTable table = f.hienThiDanhSachFoodCategory();
            dtgDoanhmuc.DataSource = table;
        }

        private void hienThiDanhSachFood()
        {
            FoodBLL f = new FoodBLL();
            DataTable table = f.hienThiDanhSachFood();









            dtgMonAn.DataSource = table;
        }

        private void hienThiDanhSachAccount()
        {

            AccountBLL t = new AccountBLL();
            DataTable table = t.hienThiDanhSachTaiKhoan();
            dtgTaiKhoan.DataSource = table;







        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            txtmaT.Text = "";
            txtTenT.Text = "";
            kt = -1;
        }

        private void LuuBan_Click(object sender, EventArgs e)
        {
            TableBLL t = new TableBLL();
            string name = txtTenT.Text.Trim();
            string er = "";

            if (!t.xuLyThemTable(name, ref er))
            {
                MessageBox.Show(er);
                return;
            }
            else
            {
                TableDTO d = new TableDTO(0, name, "Trống");
                if (t.themRow(d))
                {
                    MessageBox.Show("Thêm thành công");
                    hienThiDanhSachTable();
                }
                else
                    MessageBox.Show("Thêm không thành công");


            }

        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            if (kt == -1)
            {
                MessageBox.Show("Chưa chọn dữ liệu");
                return;
            }


            DataGridViewRow r = dtgBan.Rows[kt];
            if (r.Cells["Mã bàn ăn"].Value == null)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn xóa?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                int ma;
                bool k = int.TryParse(r.Cells["Mã bàn ăn"].Value.ToString(), out ma);

                if (k == true)
                {


                    TableBLL t = new TableBLL();
                    if (t.xoaRow(ma))
                    {
                        hienThiDanhSachTable();
                        MessageBox.Show("Xóa thành công");
                        kt = -1;
                       
                    }
                    else
                        MessageBox.Show("Xóa không thành công");
                }

            }
        }

        private void btnSuaBan_Click(object sender, EventArgs e)
        {
            if (kt == -1)
            {
                MessageBox.Show("Chưa chọn dữ liệu");
                return;
            }

            DataGridViewRow r = dtgBan.Rows[kt];
            if (r.Cells["Mã bàn ăn"].Value == null)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }

            TableBLL t = new TableBLL();
            string id = txtmaT.Text.Trim();
            string name = txtTenT.Text.Trim();
            string er = "";

            if (!t.xuLyChinhSuaTable(int.Parse(id), name, ref er))
            {
                MessageBox.Show(er);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn chỉnh sửa?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                TableDTO d = new TableDTO(int.Parse(id), name, cmbTrangThai.SelectedItem.ToString());
                if (t.chinhSuaRow(d))
                {
                    MessageBox.Show("Chỉnh sửa thành công");
                    hienThiDanhSachTable();
                    kt = -1;
                }
                else
                    MessageBox.Show("Chỉnh sửa không thành công");




            }
        }



        private void dtgBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            DataGridViewRow row = new DataGridViewRow();
            row = dtgBan.Rows[e.RowIndex];
            txtmaT.Text = row.Cells["Mã bàn ăn"].Value.ToString();
            txtTenT.Text = row.Cells["Tên bàn ăn"].Value.ToString();
            cmbTrangThai.SelectedItem = row.Cells["Trạng thái"].Value.ToString();
            kt = e.RowIndex;


        }

        private void frmAdmin_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn thoát chương trình?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }




        private void btnTimB_Click(object sender, EventArgs e)
        {
            if (txtTimB.Text == "")
            {
                MessageBox.Show("Chưa nhập thông tin cần tìm");
                return;
            }
            txtmaT.ReadOnly = false;
            txtmaT.Text = "";
            txtTenT.Text = "";
            cmbTrangThai.SelectedIndex = -1;
            kt = -1;

            string name = txtTimB.Text.Trim();

            TableBLL tableBLL = new TableBLL();
            DataTable t = tableBLL.TimKiemBanAn(name);
            dtgBan.DataSource = null;
            dtgBan.Refresh();
            dtgBan.DataSource = t;





        }

        private void btnXemT_Click(object sender, EventArgs e)
        {
            hienThiDanhSachTable();
        }
        int ktAccount = -1;
        private void dtgTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

            txtMaTk.ReadOnly = true;
            if (e.RowIndex < 0)
                return;
            DataGridViewRow row = new DataGridViewRow();
            row = dtgTaiKhoan.Rows[e.RowIndex];
            if (frmLogin.account.UserName == row.Cells["Username"].Value.ToString())
            {
                cmbLoaiTK.Enabled = false;
            }
            else
                cmbLoaiTK.Enabled = true;
            txtMaTk.Text = row.Cells["Mã tài khoản"].Value.ToString();
            txtdisplayName.Text = row.Cells["Tên hiển thị"].Value.ToString();
            txtUserName.Text = row.Cells["Username"].Value.ToString();
            cmbLoaiTK.Text = row.Cells["Loại tài khoản"].Value.ToString();
            ktAccount = e.RowIndex;

        }

        private void dtgBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgMonAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int ktMonAn = -1;
        private void dtgMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtmaM.ReadOnly = true;
            if (e.RowIndex < 0)
                return;
            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            DataGridViewRow row = new DataGridViewRow();
            row = dtgMonAn.Rows[e.RowIndex];
            txtmaM.Text = row.Cells["Mã món ăn"].Value.ToString().Trim();
            txtTenM.Text = row.Cells["Tên món ăn"].Value.ToString().Trim();


            cmbLoaiM.Text = row.Cells["Loại"].Value.ToString().Trim();



            //double price = double.Parse(row.Cells["Giá"].Value.ToString().Trim());
            //txtGiaM.Text = price.ToString("c", cultureInfo);
            txtGiaM.Text = row.Cells["Giá"].Value.ToString().Trim();
            ktMonAn = e.RowIndex;

        }





        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (dtpEndDate.Value> DateTime.Now || dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Thời gian không hợp lệ");
                return;
            }
            DateTime dateStart = dtpStartDate.Value;

            

            DateTime dateEnd = dtpEndDate.Value;
           



            hienThiDoanhThu(currentPage,dateStart, dateEnd);


        }

        private void hienThiDoanhThu(int page, DateTime dateStart, DateTime dateEnd)
        {
          
            DataTable dt = BillBLL.HienThiDoanhThu(page, dateStart, dateEnd);
            dtgDoanhthu.DataSource = dt;
            int numPage = int.Parse(Math.Ceiling((double)BillBLL.getSizeOfBill(dateStart, dateEnd) / 10.0) + "");
            if (numPage == 0)
            {
                this.lblPage.Text = "0/0";
            }
            else
                this.lblPage.Text = this.currentPage + "/" + numPage;


            hienThiTongDanhThu();
        }

        private void hienThiTongDanhThu()
        {
            int total = 0;
            CultureInfo c = new CultureInfo("vi-VN");

            if (dtgDoanhthu.Rows.Count > 0)
            {
                total = BillBLL.hienThiTongDanhThu(dtpStartDate.Value, dtpEndDate.Value);
                txtDoanhThu.Text = total.ToString("c", c);

            }
            else
            {
                txtDoanhThu.Text = total.ToString("c", c);
            }

        }

        private void txtDoanhThu_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTongBill_Click(object sender, EventArgs e)
        {
            if (dtgDoanhthu.DataSource == null)
            {
                MessageBox.Show("Chưa có dữ liệu");
            }
            int sl = BillBLL.getSizeOfBill(dtpStartDate.Value, dtpEndDate.Value);
            MessageBox.Show("Tổng số lượng bill: " + sl);


        }



        private void button6_Click_1(object sender, EventArgs e)
        {
            hienThiDanhSachFood();
        }

        private void btnTimF_Click(object sender, EventArgs e)
        {
            if (txtTimF.Text == "")
            {
                MessageBox.Show("Chưa nhập thông tin cần tìm");
                return;
            }
            txtmaM.ReadOnly = false;
            txtmaM.Text = "";
            txtTenM.Text = "";
            txtGiaM.Text = "";
            cmbLoaiM.SelectedIndex = -1;
            ktMonAn = -1;

            string name = txtTimF.Text.Trim();

            FoodBLL tableBLL = new FoodBLL();
            DataTable t = tableBLL.TimKiemMonAn(name);
            dtgMonAn.DataSource = null;
            dtgMonAn.Refresh();
            dtgMonAn.DataSource = t;

        }
        private void button5_Click(object sender, EventArgs e)
        {

            txtTenM.Text = "";
            txtGiaM.Text = "";
            cmbLoaiM.SelectedIndex = 0;
            ktMonAn = -1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FoodBLL t = new FoodBLL();
            string id = txtmaM.Text.Trim();
            string name = txtTenM.Text.Trim();
            string price = txtGiaM.Text.Trim();
            int loai = int.Parse(cmbLoaiM.SelectedValue.ToString());
            string er = "";

            if (!t.xuLyThemFood( name, price, ref er))
            {
                MessageBox.Show(er);
                return;
            }
            else
            {
                FoodDTO d = new FoodDTO(0, name, double.Parse(price), loai);
                if (t.themRow(d))
                {
                    MessageBox.Show("Thêm thành công");
                    hienThiDanhSachFood();
                }
                else
                    MessageBox.Show("Thêm không thành công");


            }
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {

            if (ktMonAn == -1)
            {
                MessageBox.Show("Chưa chọn dữ liệu");
                return;
            }

            DataGridViewRow r = dtgMonAn.Rows[ktMonAn];
            if (r.Cells["Mã món ăn"].Value == null)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }

            FoodBLL t = new FoodBLL();
            string id = txtmaM.Text.Trim();
            string name = txtTenM.Text.Trim();
            string price = txtGiaM.Text.Trim();
            int loai = int.Parse(cmbLoaiM.SelectedValue.ToString());
            string er = "";

            if (!t.xuLyChinhSuaFood(int.Parse(id), name, price, ref er))
            {
                MessageBox.Show(er);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn chỉnh sửa?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {

                FoodDTO d = new FoodDTO(int.Parse(id), name, double.Parse(price), loai);
                if (t.chinhSuaRow(d))
                {
                    MessageBox.Show("Chỉnh sửa thành công");
                    hienThiDanhSachFood();
                    ktMonAn = -1;
                }
                else
                    MessageBox.Show("Chỉnh sửa không thành công");




            }
        }
        int ktLoaiMonAn = -1;

        private void btnThemL_Click(object sender, EventArgs e)
        {
            txtTenL.Text = "";
            ktLoaiMonAn = -1;

        }

        private void btnXemL_Click(object sender, EventArgs e)
        {
            hienThiDanhSachFoodCategory();
        }

        private void btnLuuL_Click(object sender, EventArgs e)
        {
            Category_FoodBLL t = new Category_FoodBLL();
            string name = txtTenL.Text.Trim();
            string er = "";

            if (!t.xuLyThemCategoryFood(name, ref er))
            {
                MessageBox.Show(er);
                return;
            }
            else
            {
                // tới đây rùi nè
                if (t.themRow(name))
                {
                    MessageBox.Show("Thêm thành công");
                    hienThiDanhSachFoodCategory();
                    loadCmbLoaiMonAn();

                }
                else
                    MessageBox.Show("Thêm không thành công");


            }
        }

        private void btnSuaL_Click(object sender, EventArgs e)
        {
            if (ktLoaiMonAn == -1)
            {
                MessageBox.Show("Chưa chọn dữ liệu");
                return;
            }

            DataGridViewRow r = dtgDoanhmuc.Rows[ktLoaiMonAn];
            if (r.Cells["Mã danh mục"].Value == null)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }

            Category_FoodBLL t = new Category_FoodBLL();
            string id = txtMaL.Text.Trim();
            string name = txtTenL.Text.Trim();

            string er = "";

            if (!t.xuLyChinhSuaCategoryFood(int.Parse(id), name, ref er))
            {
                MessageBox.Show(er);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn chỉnh sửa?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {

                CategoryDTO d = new CategoryDTO(int.Parse(id), name);
                if (t.chinhSuaRow(d))
                {
                    MessageBox.Show("Chỉnh sửa thành công");
                    hienThiDanhSachFoodCategory();
                    loadCmbLoaiMonAn();
                    ktLoaiMonAn = -1;
                }
                else
                    MessageBox.Show("Chỉnh sửa không thành công");




            }




        }
        private void loadCmbLoaiMonAn()
        {

            Category_FoodBLL b = new Category_FoodBLL();
            DataTable t = b.hienThiDanhSachFoodCategory();
            cmbLoaiM.DisplayMember = "Tên danh mục";
            cmbLoaiM.ValueMember = "Mã danh mục";
            cmbLoaiM.DataSource = t;



        }

        private void dtgDoanhmuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            DataGridViewRow row = new DataGridViewRow();
            row = dtgDoanhmuc.Rows[e.RowIndex];
            txtMaL.Text = row.Cells["Mã danh mục"].Value.ToString();
            txtTenL.Text = row.Cells["Tên danh mục"].Value.ToString();
            ktLoaiMonAn = e.RowIndex;
        }

        private void cmbLoaiM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTimL_Click(object sender, EventArgs e)
        {
            if (txtTimL.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập thông tin cần tìm");
                return;
            }
            txtMaL.Text = "";
            txtTenL.Text = "";

            ktLoaiMonAn = -1;

            string name = txtTimL.Text.Trim();

            Category_FoodBLL tableBLL = new Category_FoodBLL();
            DataTable t = tableBLL.TimKiemLoaiMonAn(name);
            dtgDoanhmuc.DataSource = null;
            dtgDoanhmuc.Refresh();
            dtgDoanhmuc.DataSource = t;





        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int lastPage = int.Parse(Math.Ceiling((double)BillBLL.getSizeOfBill(this.dtpStartDate.Value, this.dtpEndDate.Value) / 10.0) + ""); ;
            if (currentPage - 1 >= 1)
            {
                hienThiDoanhThu(--currentPage, this.dtpStartDate.Value, this.dtpEndDate.Value);
            }
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            currentPage = 1;
            hienThiDoanhThu(1, this.dtpStartDate.Value, this.dtpEndDate.Value);



        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (dtgDoanhthu == null)
            {
                MessageBox.Show("Chưa có dữ liệu");
                return;
            }
          
            int numPage = int.Parse(Math.Ceiling((double)BillBLL.getSizeOfBill(this.dtpStartDate.Value, this.dtpEndDate.Value) / 10.0) + "");
            if (numPage > 0)
            {
                currentPage = numPage;
                hienThiDoanhThu(numPage, dtpStartDate.Value, dtpEndDate.Value);
            }


        }

        private void btnNextP_Click(object sender, EventArgs e)
        {
            int lastPage = int.Parse(Math.Ceiling((double)BillBLL.getSizeOfBill(this.dtpStartDate.Value, this.dtpEndDate.Value) / 10.0) + ""); ;
            if (currentPage + 1 <= lastPage)
            {
                hienThiDoanhThu(++currentPage, this.dtpStartDate.Value, this.dtpEndDate.Value);
            }
          

        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (txtmaM.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn món");
                return;
            }
            DialogResult r =
            MessageBox.Show("Bạn có thực sự muốn xóa?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                if (!FoodBLL.xoaMonAn(int.Parse(txtmaM.Text.Trim())))
                    MessageBox.Show("Món đã tồn tại trong bill trước đó");
                else
                {
                    MessageBox.Show("Xóa thành công");
                    hienThiDanhSachFood();
                    ktMonAn = -1;
                }


            }


        }

        private void btnXoaL_Click(object sender, EventArgs e)
        {
            if (txtMaL.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn danh mục");
                return;
            }
            DialogResult r =
            MessageBox.Show("Bạn có thực sự muốn xóa?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                if (!Category_FoodBLL.xoaLoaiMonAn(int.Parse(txtMaL.Text.Trim())))
                    MessageBox.Show("Danh mục đã tồn tại trong bill trước đó");
                else
                {
                    MessageBox.Show("Xóa thành công");
                    hienThiDanhSachFoodCategory();
                    loadCmbLoaiMonAn();
                    ktLoaiMonAn = -1;

                }


            }
        }
        private void button27_Click(object sender, EventArgs e)
        {
            cmbLoaiTK.Enabled = true;
            txtdisplayName.Text = "";
            txtUserName.Text = "";
            cmbLoaiTK.Text = "Staff";
            ktAccount = -1;
        }

        private void btnLuuTK_Click(object sender, EventArgs e)
        {
            AccountBLL a = new AccountBLL();
            string name = txtdisplayName.Text.Trim();
            string user = txtUserName.Text.Trim();
            string er = "";

            if (!AccountBLL.xuLyThemAccount(user, name, ref er))
            {
                MessageBox.Show(er);
                return;
            }
            else
            {
                Account d = new Account(user, "0", name,int.Parse( cmbLoaiTK.SelectedValue.ToString()));
                if (a.themRow(d))
                {
                    MessageBox.Show("Thêm thành công");
                    hienThiDanhSachAccount();
                }
                else
                    MessageBox.Show("Thêm không thành công");

            }
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            if (ktAccount == -1)
            {
                MessageBox.Show("Chưa chọn dữ liệu");
                return;
            }


            DataGridViewRow r =dtgTaiKhoan.Rows[ktAccount];
            if (r.Cells["Mã tài khoản"].Value == null)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            int idA = frmLogin.account.ID;

            if (int.Parse(r.Cells["Mã tài khoản"].Value.ToString()) ==idA )
            {
                MessageBox.Show("Bạn không được xóa chính mình chứ");
                return;
            }    
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn xóa?", "Cảnh báo", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                int ma = int.Parse(r.Cells["Mã tài khoản"].Value.ToString());
                    if (AccountBLL.xoaRow(ma))
                    {
                        hienThiDanhSachAccount();
                        MessageBox.Show("Xóa thành công");
                        ktAccount = -1;
                    }
                    else
                        MessageBox.Show("Xóa không thành công");
               

            }
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            if (ktAccount == -1)
            {
                MessageBox.Show("Chưa chọn dữ liệu");
                return;
            }

            DataGridViewRow r = dtgTaiKhoan.Rows[ktAccount];
            if (r.Cells["Mã tài khoản"].Value == null)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
               

            int  id =int.Parse(txtMaTk.Text.Trim());
            string username = txtUserName.Text.Trim();
            string displayname = txtdisplayName.Text.Trim();
            string er = "";
            if (!AccountBLL.xuLyChinhSuaAccount(id,username, displayname,  ref er))
            {
                MessageBox.Show(er);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn chỉnh sửa?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Account d = new Account(id, username, "0", displayname, int.Parse(cmbLoaiTK.SelectedValue.ToString()));

                if (AccountBLL.chinhSuaRow(d))
                {
                    MessageBox.Show("Chỉnh sửa thành công");
                    if (int.Parse(r.Cells["Mã tài khoản"].Value.ToString()) == frmLogin.account.ID)
                    {
                       //frmLogin mainForm = new frmLogin();
                       // mainForm.Show();
                       // this.Close();
                    }    
                    hienThiDanhSachAccount();
                    ktAccount = -1;


                }
                else
                    MessageBox.Show("Chỉnh sửa không thành công");




            }
        }

        private void btnTimTK_Click(object sender, EventArgs e)
        {
            if (txtTimTK.Text == "")
            {
                MessageBox.Show("Chưa nhập thông tin cần tìm");
                return;
            }
            txtMaTk.Text = "";
            txtUserName.Text = "";
           txtdisplayName.Text = "";
            cmbLoaiTK.SelectedIndex = 0;
            ktAccount= -1;

            string name = txtTimTK.Text.Trim();

            TableBLL tableBLL = new TableBLL();
            DataTable t = AccountBLL.TimKiemTaiKhoan(name);
            dtgTaiKhoan.DataSource = null;
            dtgTaiKhoan.Refresh();
            dtgTaiKhoan.DataSource = t;


        }

        private void txtTimB_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXemTK_Click(object sender, EventArgs e)
        {
            hienThiDanhSachAccount();
        }

       

        private void btnReport_Click(object sender, EventArgs e)
        {
            DateTime s = dtpStartDate.Value;
            DateTime s1 = dtpEndDate.Value;

            start = s;
            end = s1;
            frmBaoCaoDoanhThu a = new frmBaoCaoDoanhThu();
            a.ShowDialog();
         
        }

        private void txtTimF_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimL_Leave(object sender, EventArgs e)
        {
            if (txtTimL.Text =="")
            {
                txtTimL.Text = "Nhập tên danh mục để tìm kiếm...";
                txtTimL.ForeColor = System.Drawing.Color.DarkGray;
            }    
        }

        private void txtTimL_Enter(object sender, EventArgs e)
        {

            if (txtTimL.Text == "Nhập tên danh mục để tìm kiếm...")
            {
                txtTimL.Text = "";
                txtTimL.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimL_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimF_Leave(object sender, EventArgs e)
        {
            if (txtTimF.Text == "")
            {
                txtTimF.Text = "Nhập tên món ăn để tìm kiếm...";
                txtTimF.ForeColor = System.Drawing.Color.DarkGray;
            }
        }

        private void txtTimF_Enter(object sender, EventArgs e)
        {
            if (txtTimF.Text == "Nhập tên món ăn để tìm kiếm...")
            {
                txtTimF.Text = "";
                txtTimF.ForeColor = System.Drawing.Color.Black;
            }

        }

        private void txtTimB_Leave(object sender, EventArgs e)
        {
            if (txtTimB.Text == "")
            {
                txtTimB.Text = "Nhập tên bàn ăn để tìm kiếm...";
                txtTimB.ForeColor = System.Drawing.Color.DarkGray;
            }
        }

        private void txtTimB_Enter(object sender, EventArgs e)
        {
            if (txtTimB.Text == "Nhập tên bàn ăn để tìm kiếm...")
            {
                txtTimB.Text = "";
                txtTimB.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimTK_Leave(object sender, EventArgs e)
        {
            if (txtTimTK.Text == "")
            {
                txtTimTK.Text = "Nhập tên hiển thị để tìm kiếm...";
                txtTimTK.ForeColor = System.Drawing.Color.DarkGray;
            }
        }

        private void txtTimTK_Enter(object sender, EventArgs e)
        {
            if (txtTimTK.Text == "Nhập tên hiển thị để tìm kiếm...")
            {
                txtTimTK.Text = "";
                txtTimTK.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimTK_TextChanged(object sender, EventArgs e)
        {

        }
    }
}