using System;
using System.Windows.Forms;
using MvLocalProject.Controller;

namespace MvLocalProject.Viewer
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 輸入ad帳號的登入密碼 + 認證
            bool result = false;
            string userName = txtAccount.Text.Trim();
            string passWord = txtPassWord.Text.Trim();
            string domainName = MvAdConnector.DomainOffice;

            if (userName.Length == 0)
            {
                MessageBox.Show("請輸入AD帳號");
                txtAccount.Focus();
                return;
            }

            if (passWord.Length == 0)
            {
                MessageBox.Show("請輸入AD密碼");
                txtPassWord.Focus();
                return;
            }
            
            // 連線至LDAP 確認使用者帳密是否合法
            result = MvAdConnector.validateUser(userName, passWord, domainName);
            if (result == false)
            {
                MessageBox.Show("帳號或密碼錯誤, 請重新輸入");
                txtAccount.Clear();
                txtPassWord.Clear();
                txtAccount.Focus();
                return;
            }

            // 連線至mvWorkFlow 確認使用者是否在職
            result = MvDbConnector.validateUserFromMvWorkFlow(userName, passWord);
            if (result == false)
            {
                MessageBox.Show("該帳號人員已離職, 請重新輸入");
                txtAccount.Clear();
                txtPassWord.Clear();
                txtAccount.Focus();
                return;
            }

            // 連線至ERP GP 確認使用者是否在職
            result = MvDbConnector.validateUserFromErpGP(userName, passWord);
            if (result == false)
            {
                MessageBox.Show("該帳號人員無ERP權限, 請重新輸入");
                txtAccount.Clear();
                txtPassWord.Clear();
                txtAccount.Focus();
                return;
            }

            // 進入Main頁面
            new frmMainDev().Show();
            this.Hide();
            // 這個部份有改過Program.cs, 才可以使用此方式
            // 否則會直接關掉Project
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ResourcePicture.MvPortal;
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtPassWord.Text == String.Empty) { return; }
            if (e.KeyCode == Keys.Enter) { btnLogin_Click(sender, e); }
        }
    }
}
