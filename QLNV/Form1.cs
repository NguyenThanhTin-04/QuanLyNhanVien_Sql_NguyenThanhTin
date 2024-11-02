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
using System.Data;

namespace QLNV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
           
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KTJAEDP\SQLEXPRESS; Initial Catalog=QLNV; User ID=sa; Password=sa");

            try
            {
                con.Open();
                string tk = txtTk.Text;
                string mk = txtMk.Text;
                string sql = "select * from Nguoidung where Taikhoan ='" + tk + "' and Matkhau = '" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dta = cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    Form2 fr01 = new Form2();
                    fr01.Show();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Lỗi kết nối");
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtMk_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTk_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
