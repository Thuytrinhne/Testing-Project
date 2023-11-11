using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class frmAccount : Form
    {
        public frmAccount()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult re =
            MessageBox.Show("Bạn có thực sự muốn thoát?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes)
                this.Close();

         
        }

  



        private void frmAccount_Load(object sender, EventArgs e)
        {
            txtUsername.Text = frmLogin.account.UserName;
            txtDisplayName.Text = frmLogin.account.DisplayName;
            if (frmLogin.account.TypeAccount == 1)
                txtLoaiTK.Text = "Admin";
            else
                txtLoaiTK.Text = "Staff";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool kt = kiemTraLoi();
            if (kt )
            {
                if (AccountBLL.DoiPassword(txtUsername.Text.Trim(), txtNewPass.Text.Trim()))

                {
                    MessageBox.Show("Đổi mật khẩu thành công");
                    this.Close();
                }
                else
                    MessageBox.Show("Đổi mật khẩu không thành công");



            }


        }

        private bool kiemTraLoi()
        {

            errorProvider1.Clear();
               
            if (txtCurPass.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtCurPass, "Dữ liệu không được rỗng");
                txtCurPass.Focus();
                return false;
            }
            if (txtNewPass.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNewPass, "Dữ liệu không được rỗng");
                txtNewPass.Focus();
                return false;
            }
            if (txtNewPassAgain.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNewPassAgain, "Dữ liệu không được rỗng");
                txtNewPassAgain.Focus();
                return false;
            }
            if (txtNewPass.Text != txtNewPassAgain.Text)
            {

                errorProvider1.SetError(txtNewPassAgain, "Mật khẩu mới không khớp");
                txtNewPassAgain.Focus();
                return false;
            }
            if (!AccountBLL.ktCurPass( txtUsername.Text.Trim(),txtCurPass.Text.Trim()))
            { 
                errorProvider1.SetError(txtCurPass, "Mật khẩu hiện tại không chính xác");
                txtCurPass.Focus();
                return false;

            }
            return true;
        }
    }
}
