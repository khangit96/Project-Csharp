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
    public partial class frLopHoc : Form
    {
        public frLopHoc()
        {
            InitializeComponent();
        }
        SqlDataAdapter daLopHoc;
        SqlDataAdapter daKhoi;
        SqlDataAdapter daNamHoc;
        DataSet ds;
        DataView dv;
        int maLopCuoiCung;

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //Hàm load các thông tin cần thiết
        private void LoadThongTin()
        {   
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string query_selectLop = @"Select * From Lop";
            daLopHoc = new SqlDataAdapter(query_selectLop, sChuoiKetNoi);
            //Khởi tạo Dataset
            ds = new DataSet("DsQuanLyHocSinh");
            //Đổ dữ liệu vào 1 bảng trong dataset
            daLopHoc.Fill(ds, "tblLopHoc");
            dv = new DataView(ds.Tables["tblLopHoc"]);
            //dgvDanhSachHocSinh.DataSource = ds.Tables["tblHocSinh"];
            dgvLopHoc.DataSource = dv;
            dgvLopHoc.Columns["MaLop"].HeaderText = "Mã lớp";
            dgvLopHoc.Columns["TenLop"].HeaderText = "Tên lớp";
            dgvLopHoc.Columns["SiSo"].HeaderText = "Sĩ số";
            dgvLopHoc.Columns["MaNamHoc"].Width = 120;
            dgvLopHoc.Columns["MaKhoi"].Width = 120;
            //Tạo đối tượng kết nối đến Database để Insert/Update/Delete
            SqlConnection con = new SqlConnection(sChuoiKetNoi);
            //Tạo đối tượng command thực thi câu lệnh truy vấn insert
            string sThemLop = @"Insert into Lop(TenLop,SiSo,MaNamHoc,MaKhoi) values(@TenLop,@SiSo,@MaNamHoc,@MaKhoi)";
            SqlCommand cmThemLop = new SqlCommand(sThemLop, con);
            cmThemLop.Parameters.Add("@TenLop", SqlDbType.NVarChar, 50, "TenLop");
            cmThemLop.Parameters.Add("@SiSo", SqlDbType.Int, 5, "SiSo");
            cmThemLop.Parameters.Add("@MaNamHoc",SqlDbType.Int, 5, "MaNamHoc");
            cmThemLop.Parameters.Add("@MaKhoi", SqlDbType.Int, 5, "MaKhoi");
            daLopHoc.InsertCommand = cmThemLop;

            //Tạo đối tượng command thực thi câu lệnh truy vấn update
            string sSuaLop = @"Update Lop set TenLop=@TenLop,SiSo=@SiSo,MaNamHoc=@MaNamHoc,MaKhoi=@MaKhoi where MaLop=@MaLop";
            SqlCommand cmSuLop = new SqlCommand(sSuaLop, con);
            cmSuLop.Parameters.Add("@TenLop", SqlDbType.NVarChar, 50, "TenLop");
            cmSuLop.Parameters.Add("@SiSo", SqlDbType.Int,5, "SiSo");
            cmSuLop.Parameters.Add("@MaNamHoc", SqlDbType.Int, 5, "MaNamHoc");
            cmSuLop.Parameters.Add("@MaKhoi", SqlDbType.Int, 5, "MaKhoi");
            cmSuLop.Parameters.Add("@MaLop", SqlDbType.Int, 5, "MaLop");
            daLopHoc.UpdateCommand = cmSuLop;
         

            //Tạo đối tượng command xóa dữ liệu 
            string sXoaLopHoc = @"Delete From Lop where MaLop=@MaLop";
            SqlCommand cmXoaLopHoc = new SqlCommand(sXoaLopHoc, con);
            cmXoaLopHoc.Parameters.Add("@MaLop", SqlDbType.Int, 5, "MaLop");
            daLopHoc.DeleteCommand = cmXoaLopHoc;
            dgvLopHoc.Columns["MaKhoi"].Visible = false;
            dgvLopHoc.Columns["MaNamHoc"].Visible = false;
             
         
         
        }
        private void frLopHoc_Load(object sender, EventArgs e)
        {
            LoadThongTin();
            LoadKhoi();
            LoadNamHoc();
            TaoCot();
        }
        private void TaoCot()
        {

            //Tạo ra cột TenNamHoc,TenKhoi
             DataGridViewColumn clTenNamHoc = new DataGridViewColumn();
             DataGridViewColumn clTenKhoi = new DataGridViewColumn();
             DataGridViewCell cell = new DataGridViewTextBoxCell();
             clTenNamHoc.CellTemplate = cell;
             clTenNamHoc.Name = "TenNamHoc";
             clTenNamHoc.HeaderText = "Tên năm học";
             dgvLopHoc.Columns.Add(clTenNamHoc);
             //clTenKhoi.CellTemplate = cell1;
             clTenKhoi.CellTemplate = cell;
             clTenKhoi.Name = "TenKhoi";
             clTenKhoi.HeaderText = "Tên khối";
             dgvLopHoc.Columns.Add(clTenKhoi);
            

             for (int i = 0; i < dgvLopHoc.RowCount; i++)
             {
                 string tenNamHoc = LayTenNamHoc(Int16.Parse(dgvLopHoc.Rows[i].Cells["MaNamHoc"].Value.ToString()));
                string tenKhoi= LayTenKhoi(Int16.Parse(dgvLopHoc.Rows[i].Cells["MaKhoi"].Value.ToString()));
                 dgvLopHoc.Rows[i].Cells["TenNamHoc"].Value = tenNamHoc;
                 dgvLopHoc.Rows[i].Cells["TenKhoi"].Value = tenKhoi;

             }
            //Hide MaNamHoc,MaKhoi
           
        }
        //Hàm load thông tin khối
        private void LoadKhoi()
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelectKhoi = @"Select * From Khoi";
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daKhoi = new SqlDataAdapter(sSelectKhoi, sChuoiKetNoi);
            //Đổ dữ liệu vào 1 bảng trong dataset
            daKhoi.Fill(ds, "tblKhoi");
            cbKhoi.DataSource = ds.Tables["tblKhoi"];
            cbKhoi.ValueMember = "MaKhoi";
            cbKhoi.DisplayMember = "TenKhoi";

        }
        
        //Hàm load thông tin năm học
        private void LoadNamHoc()
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelectNamHoc = @"Select * From NienKhoa";
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daNamHoc = new SqlDataAdapter(sSelectNamHoc, sChuoiKetNoi);
            //Đổ dữ liệu vào 1 bảng trong dataset
            daNamHoc.Fill(ds, "tblNienKhoa");
            cbNamHoc.DataSource = ds.Tables["tblNienKhoa"];
            cbNamHoc.ValueMember = "MaNamHoc";
            cbNamHoc.DisplayMember = "TenNamHoc";
           
        }

        private void dgvLopHoc_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dr = dgvLopHoc.SelectedRows[0];
                txtMaLop.Text = dr.Cells["MaLop"].Value.ToString();
                txtSiSo.Text = dr.Cells["SiSo"].Value.ToString();
                txtTenLop.Text = dr.Cells["TenLop"].Value.ToString();
                int Makhoi = Int16.Parse(dr.Cells["MaKhoi"].Value.ToString());
               // int MaNamHoc = Int16.Parse(dr.Cells["MaNamHoc"].Value.ToString());
                string TenNamHoc = LayTenNamHoc( Int16.Parse(dr.Cells["MaNamHoc"].Value.ToString()));
                cbKhoi.SelectedIndex = Makhoi - 1;
                cbNamHoc.Text = TenNamHoc;
            }
            catch (Exception ex)
            {
             
                return;
            }
        }
          

        private void btThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
            return;
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiểm tra tính hợp lệ đầu vào dữ liệu
                if (txtSiSo.Text == "" || txtTenLop.Text == "")
                {
                    MessageBox.Show("Bạn cần phải nhập đầy đủ thông tin!",
                        "Thông báo");
                    return;
                }
                //Thêm 1 dòng vào tblNienKhoa
                DataRow r = ds.Tables["tblLopHoc"].NewRow();
                //Nhập giá trị vào các trường tương ứng trên Datarow
                r["TenLop"] = txtTenLop.Text;
                r["Siso"] =Int16.Parse(txtSiSo.Text);
                r["MaKhoi"] = Int16.Parse(cbKhoi.SelectedValue.ToString());
                r["MaNamHoc"] = Int16.Parse(cbNamHoc.SelectedValue.ToString());

                  if (maLopCuoiCung == 0)
                  {
                      MaLopCuoiCungTruocKhiThem();
                  }
                  maLopCuoiCung++;
                  r["MaLop"] = maLopCuoiCung;           
                ds.Tables["tblLopHoc"].Rows.Add(r);
                dgvLopHoc.Rows[dgvLopHoc.RowCount-2].Cells["TenNamHoc"].Value = LayTenNamHoc(Int16.Parse(cbNamHoc.SelectedValue.ToString()));
                dgvLopHoc.Rows[dgvLopHoc.RowCount-2].Cells["TenKhoi"].Value = LayTenKhoi(Int16.Parse(cbKhoi.SelectedValue.ToString()));
              
            }
            catch (Exception ex)
            {
                return;
            }
            try
            {
                daLopHoc.Update(ds, "tblLopHoc");
                MessageBox.Show("Thêm thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm thất bại!", "Thông báo");
                return;
            }
            
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dr = dgvLopHoc.SelectedRows[0];
                dgvLopHoc.Rows.Remove(dr);
                MessageBox.Show("Xóa thành công", "Thông báo");
                try
                {
                    daLopHoc.Update(ds, "tblLopHoc");
                }
                catch (Exception ex)
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa thất bại", "Thông báo");
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daLopHoc.Update(ds, "tblLopHoc");
                MessageBox.Show("Lưu thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            ds.Tables["tblLopHoc"].RejectChanges();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgvLopHoc.SelectedRows[0];
            dgvLopHoc.BeginEdit(true);
            dr.Cells["MaLop"].Value = txtMaLop.Text;
            dr.Cells["TenLop"].Value = txtTenLop.Text;
            dr.Cells["SiSo"].Value = Int16.Parse(txtSiSo.Text.ToString());
            dr.Cells["MaKhoi"].Value = Int16.Parse(cbKhoi.SelectedValue.ToString());
            dr.Cells["MaNamHoc"].Value = Int16.Parse(cbNamHoc.SelectedValue.ToString());
            dr.Cells["TenNamHoc"].Value = LayTenNamHoc(Int16.Parse(cbNamHoc.SelectedValue.ToString()));
            dr.Cells["TenKhoi"].Value = LayTenKhoi(Int16.Parse(cbKhoi.SelectedValue.ToString()));
              
            dgvLopHoc.EndEdit();
            MessageBox.Show("Cập nhật dữ liệu thành công !", "Thông báo");
        }
        public void MaLopCuoiCungTruocKhiThem()
        {

            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sMaLopCuoiCung = @"Select MaLop From Lop";
            SqlDataAdapter daTenLop = new SqlDataAdapter(sMaLopCuoiCung, sChuoiKetNoi);
            DataTable dt = new DataTable();
            daTenLop.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                // Lấy chỉ số phần tử dòng cuối cùng 
                int iDongCuoi = dt.Rows.Count - 1;
                maLopCuoiCung = int.Parse(dt.Rows[iDongCuoi][0].ToString());
            }
        }
        //Hàm lấy tên năm học
        private string LayTenNamHoc(int MaNamHoc)
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sTenNamHoc = @"Select TenNamHoc From NienKhoa Where MaNamHoc="+MaNamHoc;
            SqlDataAdapter daTenNamHoc = new SqlDataAdapter(sTenNamHoc, sChuoiKetNoi);
            DataTable dt = new DataTable();
            daTenNamHoc.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        //Hàm lấy tên khối
        private string LayTenKhoi(int MaKhoi)
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sTenKhoi = @"Select TenKhoi From Khoi Where MaKhoi=" +MaKhoi;
            SqlDataAdapter daTenKhoi = new SqlDataAdapter(sTenKhoi, sChuoiKetNoi);
            DataTable dt = new DataTable();
            daTenKhoi.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
    }
}
