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
    public partial class frmNienkhoa : Form
    {
        public frmNienkhoa()
        {
            InitializeComponent();
        }
        SqlDataAdapter daNienKhoa;
        DataSet ds;
        DataView dv;
        int maNienKhoaCuoiCung;

        private void frmNienkhoa_Load(object sender, EventArgs e)
        {   //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string query_selectNienKhoa = @"Select * From NienKhoa";
            daNienKhoa = new SqlDataAdapter(query_selectNienKhoa,sChuoiKetNoi);
            //Khởi tạo Dataset
            ds = new DataSet("DsQuanLyHocSinh");
            //Đổ dữ liệu vào 1 bảng trong dataset
            daNienKhoa.Fill(ds, "tblNienKhoa");
            dv = new DataView(ds.Tables["tblNienKhoa"]);
            //dgvDanhSachHocSinh.DataSource = ds.Tables["tblHocSinh"];
            dgvNienKhoa.DataSource = dv;
            dgvNienKhoa.Columns["MaNamHoc"].HeaderText = "Mã năm học";
            dgvNienKhoa.Columns["TenNamHoc"].HeaderText = "Tên năm học";
            dgvNienKhoa.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvNienKhoa.Columns["GhiChu"].Width = 120;

            //Tạo đối tượng kết nối đến Database để Insert/Update/Delete
            SqlConnection con = new SqlConnection(sChuoiKetNoi);
            //Tạo đối tượng command thực thi câu lệnh truy vấn insert
            string sThemNienKhoa = @"Insert into NienKhoa(TenNamHoc,GhiChu) values(@TenNamHoc,@GhiChu)";
            SqlCommand cmThemNienKhoa = new SqlCommand(sThemNienKhoa, con);
            cmThemNienKhoa.Parameters.Add("@TenNamHoc", SqlDbType.NVarChar, 50, "TenNamHoc");
            cmThemNienKhoa.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 50, "GhiChu");
            daNienKhoa.InsertCommand = cmThemNienKhoa;

            //Tạo đối tượng command thực thi câu lệnh truy vấn update
            string sSuaNienKhoa = @"Update NienKhoa set TenNamHoc=@TenNamHoc,GhiChu=@GhiChu where MaNamHoc=@MaNamHoc";
            SqlCommand cmSuNienKhoa = new SqlCommand(sSuaNienKhoa, con);
            cmSuNienKhoa.Parameters.Add("@TenNamHoc", SqlDbType.NVarChar, 50, "TenNamHoc");
            cmSuNienKhoa.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 50, "GhiChu");
            cmSuNienKhoa.Parameters.Add("@MaNamHoc", SqlDbType.Int, 5, "MaNamHoc");
            daNienKhoa.UpdateCommand = cmSuNienKhoa;

            //Tạo đối tượng command xóa dữ liệu 
            string sXoaNienKhoa = @"Delete From NienKhoa where MaNamHoc=@MaNamHoc";
            SqlCommand cmXoaNienKhoa = new SqlCommand(sXoaNienKhoa, con);
            cmXoaNienKhoa.Parameters.Add("@MaNamHoc", SqlDbType.Int, 5, "MaNamHoc");
            daNienKhoa.DeleteCommand = cmXoaNienKhoa;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dr = dgvNienKhoa.SelectedRows[0];
                dgvNienKhoa.Rows.Remove(dr);
                MessageBox.Show("Xóa thành công", "Thông báo");
                try
                {
                    daNienKhoa.Update(ds, "tblNienKhoa");
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

        private void btThem_Click(object sender, EventArgs e)
        {
            //Kiểm tra tính hợp lệ đầu vào dữ liệu
            if (txtNienKhoa.Text == "")
            {
                MessageBox.Show("Bạn cần phải nhập đầy đủ thông tin!",
                    "Thông báo");
                return;
            }
            //Thêm 1 dòng vào tblNienKhoa
            DataRow r = ds.Tables["tblNienKhoa"].NewRow();
            //Nhập giá trị vào các trường tương ứng trên Datarow
            r["TenNamHoc"] = txtNienKhoa.Text;
            if (txtGhiChu.Text != "")
            {
                r["GhiChu"] = txtGhiChu.Text;
            }
            else
            {

                r["GhiChu"] = "Chuaw có thông tin";

            }
            if (maNienKhoaCuoiCung == 0)
            {
                MaHSCuoiCungTruocKhiThem();
            }
            maNienKhoaCuoiCung++;
            r["MaNamHoc"] = maNienKhoaCuoiCung;
            ds.Tables["tblNienKhoa"].Rows.Add(r);
           try
            {
                daNienKhoa.Update(ds, "tblNienKhoa");
                MessageBox.Show("Thêm thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                return;
            }
            
           
        }
        public void MaHSCuoiCungTruocKhiThem()
        {

            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sMaHSCuoiCung = @"Select MaNamHoc From NienKhoa";
            SqlDataAdapter daTenLop = new SqlDataAdapter(sMaHSCuoiCung, sChuoiKetNoi);
            DataTable dt = new DataTable();
            daTenLop.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                // Lấy chỉ số phần tử dòng cuối cùng 
                int iDongCuoi = dt.Rows.Count - 1;
                maNienKhoaCuoiCung = int.Parse(dt.Rows[iDongCuoi][0].ToString());
            }

        }

        private void btSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgvNienKhoa.SelectedRows[0];
            dgvNienKhoa.BeginEdit(true);
            dr.Cells["MaNamHoc"].Value = txtMaNienKhoa.Text;
            dr.Cells["TenNamHoc"].Value = txtNienKhoa.Text;
            dr.Cells["GhiChu"].Value = txtGhiChu.Text;
            dgvNienKhoa.EndEdit();
            MessageBox.Show("Cập nhật dữ liệu thành công !", "Thông báo");
           
            
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
            return;
        }

        private void dgvNienKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvNienKhoa_Click(object sender, EventArgs e)
        {

            DataGridViewRow dr = dgvNienKhoa.SelectedRows[0];
            txtMaNienKhoa.Text = dr.Cells["MaNamHoc"].Value.ToString();
            txtNienKhoa.Text = dr.Cells["TenNamHoc"].Value.ToString();
            txtGhiChu.Text = dr.Cells["GhiChu"].Value.ToString();
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                daNienKhoa.Update(ds, "tblNienKhoa");
                MessageBox.Show("Lưu thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds.Tables["tblNienKhoa"].RejectChanges();
        }
    }
}
