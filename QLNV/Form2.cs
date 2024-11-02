using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QLNV
{
    public partial class Form2 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DESKTOP-KTJAEDP\SQLEXPRESS; Initial Catalog=QLNV; User ID=sa; Password=sa";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        void LoadData()
        {
            try
            {
                command = new SqlCommand("SELECT * FROM ThongTinNhanVien", connection);
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                dataGridView.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView.ReadOnly = true;
            try
            {
                connection = new SqlConnection(str);
                connection.Open();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.ReadOnly = true;
            int i = dataGridView.CurrentRow.Index;
            txtMaNV.Text = dataGridView.Rows[i].Cells[0].Value.ToString();
            txtTenNV.Text = dataGridView.Rows[i].Cells[1].Value.ToString();
            dtNgaysinh.Text = dataGridView.Rows[i].Cells[2].Value.ToString();
            cbGioitinh.Text = dataGridView.Rows[i].Cells[3].Value.ToString();
            txtCV.Text = dataGridView.Rows[i].Cells[4].Value.ToString();
            txtTL.Text = dataGridView.Rows[i].Cells[5].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                   
                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM ThongTinNhanVien WHERE MaNV = @MaNV", connection);
                    checkCommand.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập mã khác.");
                        return;
                    }

                    command = new SqlCommand("INSERT INTO ThongTinNhanVien (MaNV, TenNV, NgaySinh, GioiTinh, ChucVu, TienLuong) VALUES (@MaNV, @TenNV, @NgaySinh, @GioiTinh, @ChucVu, @TienLuong)", connection);
                    command.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    command.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                    command.Parameters.AddWithValue("@NgaySinh", dtNgaysinh.Value);
                    command.Parameters.AddWithValue("@GioiTinh", cbGioitinh.SelectedItem);
                    command.Parameters.AddWithValue("@ChucVu", txtCV.Text);
                    command.Parameters.AddWithValue("@TienLuong", txtTL.Text);
                    command.ExecuteNonQuery();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
                }
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    command = new SqlCommand("UPDATE ThongTinNhanVien SET TenNV = @TenNV, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, ChucVu = @ChucVu, TienLuong = @TienLuong WHERE MaNV = @MaNV", connection);
                    command.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    command.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                    command.Parameters.AddWithValue("@NgaySinh", dtNgaysinh.Value);
                    command.Parameters.AddWithValue("@GioiTinh", cbGioitinh.SelectedItem);
                    command.Parameters.AddWithValue("@ChucVu", txtCV.Text);
                    command.Parameters.AddWithValue("@TienLuong", txtTL.Text);
                    command.ExecuteNonQuery();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa dữ liệu: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên trước khi xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    
                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM ThongTinNhanVien WHERE MaNV = @MaNV", connection);
                    checkCommand.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Không có nhân viên nào với mã này để xóa.");
                        return;
                    }

                   
                    command = new SqlCommand("DELETE FROM ThongTinNhanVien WHERE MaNV = @MaNV", connection);
                    command.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    command.ExecuteNonQuery();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
                }
            }
        }


        private void btnKhoitao_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn khởi tạo lại dữ liệu không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                txtMaNV.Clear();
                txtTenNV.Clear();

                
                dtNgaysinh.Value = DateTime.Now;

                
                cbGioitinh.SelectedIndex = -1;

               
                txtCV.Clear();
                txtTL.Clear();

               
                LoadData();

               
                txtMaNV.Enabled = true; 
                txtMaNV.Focus(); 
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) || txtMaNV.Text.Length != 10 || !int.TryParse(txtMaNV.Text, out _))
            {
                MessageBox.Show("Mã nhân viên phải là số và đủ 10 ký tự.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenNV.Text) || txtTenNV.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Tên nhân viên phải là chữ và không được để trống.");
                return false;
            }

            if (cbGioitinh.SelectedItem == null || (cbGioitinh.SelectedItem.ToString() != "Nam" && cbGioitinh.SelectedItem.ToString() != "Nữ"))
            {
                MessageBox.Show("Vui lòng chọn giới tính là 'Nam' hoặc 'Nữ'.");
                return false;
            }

            
            if (string.IsNullOrWhiteSpace(txtCV.Text) || txtCV.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Chức vụ không được để trống và không được có số.");
                return false;
            }

           
            if (string.IsNullOrWhiteSpace(txtTL.Text) || !decimal.TryParse(txtTL.Text, out _))
            {
                MessageBox.Show("Tiền lương phải là số và không được để trống.");
                return false;
            }


            DateTime ngaySinh = dtNgaysinh.Value;
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            if (ngaySinh > DateTime.Now.AddYears(-tuoi)) 
            {
                tuoi--;
            }

            if (tuoi < 18)
            {
                MessageBox.Show("Nhân viên phải từ 18 tuổi trở lên.");
                return false;
            }

            return true;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string timKiem = txtTimkiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(timKiem))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên hoặc tên nhân viên để tìm kiếm.");
                return;
            }

            try
            {
                command = connection.CreateCommand();

                
                if (timKiem.All(char.IsDigit))
                {
                    command.CommandText = "SELECT * FROM ThongTinNhanVien WHERE MaNV = @MaNV";
                    command.Parameters.AddWithValue("@MaNV", timKiem);
                }
            
                else
                {
                    command.CommandText = "SELECT * FROM ThongTinNhanVien WHERE TenNV LIKE @TenNV";
                    command.Parameters.AddWithValue("@TenNV", "%" + timKiem + "%");
                }

                adapter.SelectCommand = command;
                table.Clear();  
                adapter.Fill(table);  

            
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã hoặc tên đã nhập.");
                }
                else
                {
                    dataGridView.DataSource = table;  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (table.Rows.Count > 0)
                {
                   
                    DataView dataView = new DataView(table);
                    dataView.Sort = "TenNV ASC";
                    //dataView.Sort = "MaNV ASC"; 

                   
                    dataGridView.DataSource = dataView;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để sắp xếp.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sắp xếp: " + ex.Message);
            }
        }
    }
}
