using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyHocSinh
{
    public partial class frLogin: Form
    {
        public frLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        //hàm kiểm tra đăng nhập 
        public bool checkLogin(string USERNAME,string PASSWORD)
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sMaHSCuoiCung = @"Select * From Users";
            SqlDataAdapter daTenLop = new SqlDataAdapter(sMaHSCuoiCung, sChuoiKetNoi);
            DataTable dt = new DataTable();
            daTenLop.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string username = dt.Rows[i][1].ToString();
                    string password = dt.Rows[i][2].ToString();
                    if (username == USERNAME && password == PASSWORD)
                    {
                        return true;
                    }         
                }
                return false;
               
        }

        //khi click button Đăng nhập
        private void btLogin_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text == ""&&tbPassword.Text=="")
            {
                MessageBox.Show("Vui lòng nhập username và password");
                tbUsername.Focus();
            }
            else if (tbUsername.Text == "" || tbPassword.Text == "")
            {
                if (tbUsername.Text != "")
                {
                    MessageBox.Show("Vui lòng nhập password");
                    tbPassword.Focus();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập username");
                    tbUsername.Focus();
                }
            }
            else
            {

                if (checkLogin(tbUsername.Text, tbPassword.Text) == true)
                {
                    frmMain frMain = new frmMain();
                    this.Hide();
                    frMain.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng");
                }
            }
        }

        private void frLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
