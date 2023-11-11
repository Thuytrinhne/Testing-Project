using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace GUI
{
    public partial class frmLogin : Form
    {
        public static Account account = new Account();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.ToString().Trim();
            string password = txtPassWord.Text.ToString().Trim();
            AccountBLL a= new AccountBLL();
            if (a.xuLyLogin(username, password))
            {
                if (Login(username, password))
                {
                    frmPhanMemQuanLyQuanCafe f2 = new frmPhanMemQuanLyQuanCafe();
                    this.Hide();
                    f2.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu chưa chính xác!!!");

                }
            }
            else
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin tài khoản");
            }    
        }

        private bool Login(string username,  string password)
        {
            AccountBLL b = new AccountBLL();
            return b.Login(username,  password, ref account);


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn thoát?","Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                // đóng kết nối
                DataBLL data = new DataBLL();
                data.closeCon();
              
                e.Cancel = false;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text =="Nhập tên đăng nhập...")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }    
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Text = "Nhập tên đăng nhập...";
                txtUsername.ForeColor = Color.Gray;
                
            }
        }

        private void txtPassWord_Leave(object sender, EventArgs e)
        {
            if (txtPassWord.Text == "")
            {
                txtPassWord.Text = "Nhập mật khẩu...";
                txtPassWord.ForeColor = Color.Gray;
                if ( txtPassWord.PasswordChar == '*')
                {

                    txtPassWord.PasswordChar = '\0';
                }
               
            }
        }

        private void txtPassWord_Enter(object sender, EventArgs e)
        {
            if (txtPassWord.Text == "Nhập mật khẩu...")
            {
                txtPassWord.Text = "";
                txtPassWord.ForeColor = Color.Black;
            }
            if (txtPassWord.PasswordChar == '\0')
            {

                txtPassWord.PasswordChar = '*';
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
