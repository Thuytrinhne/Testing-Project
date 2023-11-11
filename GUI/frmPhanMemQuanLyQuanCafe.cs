using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUI
{
    public partial class frmPhanMemQuanLyQuanCafe : Form
    {
        public frmPhanMemQuanLyQuanCafe()
        {
            InitializeComponent();
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(sender, e);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result =
            MessageBox.Show ("Bạn có thực sự muốn đăng xuất?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

    

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccount f = new frmAccount();
            f.Show();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmLogin.account.TypeAccount == 1)
            {

                frmAdmin a = new frmAdmin();
                a.ShowDialog();
                hienThiTable();
                hienThiCategory();
                loadCmbTable();


            }
            else
                MessageBox.Show("Chế độ xem của Admin chỉ dành cho tài khoản admin");
        }

        private void frmPhanMemQuanLyQuanCafe_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyQuanCafeDataSet4.TABLE_FOOD' table. You can move, or remove it, as needed.
            this.tABLE_FOODTableAdapter.Fill(this.quanLyQuanCafeDataSet4.TABLE_FOOD);
            // TODO: This line of code loads data into the 'quanLyQuanCafeDataSet3.FOOD_CATEGORY' table. You can move, or remove it, as needed.
            this.fOOD_CATEGORYTableAdapter.Fill(this.quanLyQuanCafeDataSet3.FOOD_CATEGORY);
            
            hienThiTable();
            hienThiCategory();
            loadCmbTable();
            hienThicmbFood();
            hienThiTongTien();


        }

        private void hienThicmbFood()
        {
            if (cmbCategory.SelectedIndex == -1)
                return;

            int ma = int.Parse(cmbCategory.SelectedValue.ToString().Trim());


            LoadFood(ma);
        }

        private void loadCmbTable()
        {
            TableBLL t = new TableBLL();
            DataTable dataTable = new DataTable();
            dataTable = t.HienThiTable();
            cmbSwitchTable.DisplayMember = "Tên bàn ăn";
            cmbSwitchTable.ValueMember = "Mã bàn ăn";

            cmbSwitchTable.DataSource = dataTable;
        }
        private void hienThiCategory()
        {
            Category_FoodBLL f = new Category_FoodBLL();
            DataTable table = f.hienThiDanhSachFoodCategory();
            cmbCategory.DisplayMember = "Tên danh mục";
            cmbCategory.ValueMember = "Mã danh mục";
            cmbCategory.DataSource = table;

        }

        private void hienThiTable()
        {
            flpTable_Food.Controls.Clear();

            TableBLL tableBLL = new TableBLL();
            DataTable dataTable = new DataTable();
            dataTable = tableBLL.HienThiTable();
            
            foreach (DataRow row in dataTable.Rows) {

                Button button = new Button()
                { Width = TableBLL.tableWidth, Height = TableBLL.tableHeight};
                button.Text = row["Tên bàn ăn"].ToString()+ "\n" +row["Trạng thái"].ToString();
                TableDTO t = new TableDTO();
                t.Id =(int) row["Mã bàn ăn"];
                t.Name = row["Tên bàn ăn"].ToString();
                t.Status = row["Trạng thái"].ToString();

                button.Tag = t;

                if (row["Trạng thái"].ToString()== "Có người")
                {
                    button.BackColor = Color.LightPink;
                }   
                else
                {
                    button.BackColor = Color.White;

                }

                button.Click +=  button_Click;
                flpTable_Food.Controls.Add(button);


            }



        }
        // int kt = -1;
        int ktClickTable = -1;

        void button_Click (object sender, EventArgs e)
        {
           
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                ktClickTable = 1;

                TableDTO m = (TableDTO)((sender as Button).Tag);
                int maBanAn = m.Id;

                dtgHienThiMenu.Tag = m;
                showMenu(maBanAn);


                lblNameBan.Text = m.Name;
            }
        }

        private int  showMenu(int maBanAn)
        {
            // lấy ra mã bill
            // gọi hàm show menu trả về 1 datatable gán cho datagridview 
            MenuBLL menuBLL = new MenuBLL();
            DataTable t = new DataTable();
            decimal Discount=0;
            int maBill= -1;

            t =   menuBLL.hienThiMenu(maBanAn, ref maBill);
            if (maBill == -1)
            {
                dtgHienThiMenu.DataSource = null;
                nudDiscount.Value = 0;


            }
            else
            {
                

                dtgHienThiMenu.DataSource = t;
                // get discount raa
                BillBLL b = new BillBLL();
                 Discount = b.getDiscount(maBill);
                if (Discount == 0)
                {

                    nudDiscount.Value = 0;

                }
                else
                {
                    nudDiscount.Value = Discount;
                }
            }


            hienThiTongTien(Discount);
            return maBill;


        }

        private void hienThiTongTien(decimal discount = 0)
        {
            decimal  totalPrice = 0;

                foreach (DataGridViewRow i in dtgHienThiMenu.Rows)
                {

                    DataGridViewRow dr = i;
                    if (dr.Cells["Thành tiền"].Value !=null)
                    totalPrice += decimal.Parse(dr.Cells["Thành tiền"].Value.ToString().Trim()); 

                }

            //   Thread.CurrentThread.CurrentCulture = cultureInfo;
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
         
            totalPrice *= ((100-discount) / 100); 
            txtTotalPrice.Text = totalPrice.ToString("c", cultureInfo);

        }

        private void cmbFood_SelectedIndexChanged(object sender, EventArgs e)
        {
          



        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedIndex == -1)
            {
                
                return;
            }

          int  ma = int.Parse(cmbCategory.SelectedValue.ToString().Trim());


            LoadFood (ma);


            
        }

        private void LoadFood(int ma)
        {
            cmbFood.DataSource = null;
            FoodBLL f = new FoodBLL();
            List<FoodDTO> list = new List<FoodDTO>();
            list = f.getFoodByCateGory(ma);
            DataTable dt = new DataTable();


            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            for (int i = 0; i < list.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = list[i].Id.ToString();
                dr["name"] = list[i].TenMon.ToString().Trim();
                dt.Rows.Add(dr);

            }

            cmbFood.DisplayMember = "name";
            cmbFood.ValueMember = "id";
            cmbFood.DataSource = dt; // để ở cuối 


        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (dtgHienThiMenu== null)
            {
                MessageBox.Show("Chưa chọn bàn");
                return;
            }
            if (dtgHienThiMenu.DataSource == null)
            {
                MessageBox.Show("Vui lòng tạo bill");
                return;
            }    
            if (cmbFood.SelectedIndex == -1) {
                MessageBox.Show("Chưa chọn món");
                return; }



            

            Bill_InforBLL b = new Bill_InforBLL();
            BillBLL billBLL = new BillBLL();

            if (dtgHienThiMenu.DataSource == null)
                return;
            TableDTO m = dtgHienThiMenu.Tag as TableDTO;
            int maBan = m.Id;
            int maBill = billBLL.getIdBillByIdTable(maBan);
            if ( cmbFood.SelectedIndex != -1 && maBill != -1)
            {
                bool k = true;
                // có mã Bill thêm món 
                int maMon = int.Parse(cmbFood.SelectedValue.ToString());
                // kt xem món đó có chưa

                DataTable dt = new DataTable();
                dt = b.getBill_Infor(maBill);
                int soLuong = int.Parse(nudSoLuongMon.Value.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    if ((int)dr["idFood"] == maMon)
                    {
                        k = false;
                    }



                }


                if (k == true )
                {
                    if (soLuong > 0)
                    {
                        // kt là mã bill 
                        bool kq = b.themMonAnChoBill(maBill, maMon, soLuong);
                        if (kq)
                        {
                            // kt trường hợp thêm số lượng món? 
                            ShowMenuByIDbill(maBill);


                        }
                        else
                            MessageBox.Show("Thêm món không thành công");
                    }
                    else
                    {
                        MessageBox.Show("Số lượng > 0");
                        return;
                    }

                }
                else
                {
                    b.capNhapSoLuong(maBill, maMon, soLuong);
                    ShowMenuByIDbill(maBill);

                }

            }
            else
            {
                return;


            }    



        }

        private void ShowMenuByIDbill(int maBill)
        {
            MenuBLL menuBLL = new MenuBLL();
            DataTable t = new DataTable();

            t = menuBLL.hienThiMenuByIDBill(maBill);
            dtgHienThiMenu.DataSource = t;
            hienThiTongTien();






        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void flpTable_Food_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgHienThiMenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            // cập nhật ngày check out, đổi status của bill
            if ( dtgHienThiMenu.DataSource == null)
            {
                MessageBox.Show("Chưa chọn bàn ăn");
                return;
            }
            DataGridViewRow r = dtgHienThiMenu.Rows[0];


            if (r.Cells[0].Value ==null)
            {
                MessageBox.Show("Hóa đơn chưa có món");
                return;
            }


            DialogResult result =  MessageBox.Show("Bạn có chắc muốn thanh toán cho bàn " + lblNameBan.Text+ "?","Cảnh báo",  MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                BillBLL b = new BillBLL();
                TableDTO t = dtgHienThiMenu.Tag as TableDTO;
                int maBan =t.Id;


                bool kq = b.thucHienCheckOut(maBan);
                if (kq)
                {
                    MessageBox.Show("Thanh toán thành công");
                    dtgHienThiMenu.DataSource = null;
                    loadBan_Trong(maBan);
                    hienThiTongTien();
                }
                else
                    MessageBox.Show("Thanh toán không thành công");
            }


        

        }

        private void btnTaoBill_Click(object sender, EventArgs e)
        {
            if (dtgHienThiMenu ==null || ktClickTable ==-1)
            {
                MessageBox.Show("Vui lòng chọn bàn");
                return;
            }
            TableDTO m = dtgHienThiMenu.Tag as TableDTO;
            if (m.Status == "Có người")
            {
                MessageBox.Show("Bạn này đã có người");
                return;
            }
            BillBLL bill = new BillBLL();
            bool kq = bill.themBill(m.Id);
            if (kq)
            {

               int maBill =  bill.getIdBillByIdTable(m.Id);
                ShowMenuByIDbill(maBill);
            }
            loadBan_CoNguoi(m.Id);



          
        }

        private void loadBan_CoNguoi(int id)
        {
            foreach (var i in flpTable_Food.Controls)
            {
                Button b = i as Button;
                TableDTO t = b.Tag as TableDTO;
                if (t.Id == id)
                {

                    t.Status = "Có người";
                    b.Text = t.Name + "\n" + t.Status;
                    b.BackColor = Color.LightPink;

                    return;
                }


            }
        }
        private void loadBan_Trong(int id)
        {
            foreach (var i in flpTable_Food.Controls)
            {
                Button b = i as Button;
                TableDTO t = b.Tag as TableDTO;
                if (t.Id == id)
                {
                    t.Status = "Trống";
                    b.Text = t.Name + "\n" + t.Status;
                    b.BackColor = Color.White;
                    dtgHienThiMenu.DataSource = null;

                    return;
                }


            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            if (dtgHienThiMenu.DataSource == null || cmbSwitchTable.SelectedIndex == -1) 
            {
                MessageBox.Show("Chưa chọn bàn cần chuyển hoặc bàn chuyển tới");
                return;
            }
            int maBanB = int.Parse(cmbSwitchTable.SelectedValue.ToString().Trim());
            string s = (((DataRowView)cmbSwitchTable.Items[cmbSwitchTable.SelectedIndex])["Tên bàn ăn"]).ToString();
            if (!ktBanTrong (maBanB))
             {

                MessageBox.Show( s + " đã có người");
                return;
             }

            TableDTO TableOld = dtgHienThiMenu.Tag as TableDTO;
            int maBanA = TableOld.Id;
            BillBLL b = new BillBLL();
            int maBill = b.getIdBillByIdTable(maBanA);

            string ques = "Bạn có chắc muốn chuyển từ bàn " + TableOld.Name + " sang " + s + "?";
            DialogResult result =  MessageBox.Show(ques,"Cảnh báo", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                
                // sửa id table của bill 
                // còn việc cập nhật trạng thái của bàn thì có trigger lo 
                bool kq = b.ChuyenBan(maBill, maBanB);
                if (kq == true)
                {
                    MessageBox.Show("Chuyển bàn thành công");
                    loadBan_Trong (maBanA);
                    loadBan_CoNguoi (maBanB);
                }
                else
                    MessageBox.Show("Chuyển bàn không thành công");

            }

        }
        private bool ktBanTrong (int maBanB)
        {
            foreach (var i in flpTable_Food.Controls)
            {
                Button b = i as Button;
                TableDTO t = b.Tag as TableDTO;
                if (t.Id == maBanB && t.Status == "Có người")
                {

                    return false;
                }

            }

            return true;
        }

 

        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            if (dtgHienThiMenu.DataSource == null && nudDiscount.Value == 0)
            {
                return;
            }
            if (dtgHienThiMenu.DataSource == null)
            {
                nudDiscount.Value = 0;
                return;
            }
             
            DataGridViewRow r = dtgHienThiMenu.Rows[0];
            if (nudDiscount.Value ==0 && r.Cells[0].Value == null)
            {
                return;
            }    
            if (r.Cells[0].Value == null)
            {
                MessageBox.Show("Hóa đơn chưa có món");
                nudDiscount.Value = 0;
                return;
            }


            decimal discount = nudDiscount.Value;

          

            // cập nhật discount của bill (mã bill, discount)

            if (discount==0)
            {
                thucHienDiscount(discount);
                hienThiTongTien(discount);

                return; 
            }

            bool kq = thucHienDiscount(discount);
            if (kq == true)
            {
                hienThiTongTien(discount);
                MessageBox.Show("Đã áp dụng giảm giá");
            }
            else
                MessageBox.Show("Áp dụng giảm giá không thành công");





        }
        public bool thucHienDiscount(decimal discount)
        {
            TableDTO tableDTO = dtgHienThiMenu.Tag as TableDTO;
            int maBan = tableDTO.Id;
            BillBLL b = new BillBLL();

            int maBill = b.getIdBillByIdTable(maBan);
            return b.capNhatDiscount(maBill, discount);


        }

        private void btnHuyBill_Click(object sender, EventArgs e)
        {
            if (dtgHienThiMenu.DataSource== null)
            {
                MessageBox.Show("Chưa chọn bàn hoặc chưa có bill");
                return;
            }
            TableDTO t = dtgHienThiMenu.Tag as TableDTO;
            DialogResult result =   MessageBox.Show("Bạn có thực sự muốn hủy bill???", "Cảnh báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {


                BillBLL bill = new BillBLL();
                int maBill = bill.getIdBillByIdTable(t.Id);


                if (bill.huyBill(maBill))
                {
                    MessageBox.Show("Hủy bill thành công");
                    loadBan_Trong(t.Id);
                }
                else
                {

                    MessageBox.Show("Hủy bill không thành công");

                }

            }
           

        }

        private void mnuSwitchTable_Click(object sender, EventArgs e)
        {
            btnSwitchTable_Click(sender, e);
        }

        private void mnuCheckOut_Click(object sender, EventArgs e)
        {
            btnCheckOut_Click(sender, e);
        }

        private void tạpBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnTaoBill_Click(sender, e);
        }

        private void hùyBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnHuyBill_Click(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
