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
    public partial class frMonHoc : Form
    {
        public frMonHoc()
        {
            InitializeComponent();
        }
        SqlDataAdapter daMonHoc;
        SqlDataAdapter daKhoi;
        SqlDataAdapter daHocKy;
        DataSet ds;
        DataView dv;
        int maMonHocCuoiCung;
        //Hàm load các thông tin cần thiết
        private void frMonHoc_Load(object sender, EventArgs e)
        {
            LoadThongTin();
            LoadKhoi();
            LoadHocKy();
            TaoCot();

        }
        private void LoadThongTin()
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string query_selectLop = @"Select * From MonHoc";
            daMonHoc = new SqlDataAdapter(query_selectLop, sChuoiKetNoi);
            //Khởi tạo Dataset
            ds = new DataSet("DsQuanLyHocSinh");
            //Đổ dữ liệu vào 1 bảng trong dataset
            daMonHoc.Fill(ds, "tblMonHoc");
            dv = new DataView(ds.Tables["tblMonHoc"]);
            dgvMonHoc.DataSource = dv;
            dgvMonHoc.Columns["MaMH"].HeaderText = "Mã môn học";
            dgvMonHoc.Columns["TenMH"].HeaderText = "Tên môn học";
            dgvMonHoc.Columns["MaHK"].HeaderText = "Học kỳ";
            dgvMonHoc.Columns["MaKhoi"].HeaderText = "Khối";
            dgvMonHoc.Columns["SoTiet"].Visible=false;

            //Tạo đối tượng kết nối đến Database để Insert/Update/Delete
            SqlConnection con = new SqlConnection(sChuoiKetNoi);
            //Tạo đối tượng command thực thi câu lệnh truy vấn insert
            string sThemMonHoc = @"Insert into MonHoc(TenMH,SoTiet,MaHK,MaKhoi) values(@TenMH,@SoTiet,@MaHK,@MaKhoi)";
            SqlCommand cmThemMonHoc = new SqlCommand(sThemMonHoc, con);
            cmThemMonHoc.Parameters.Add("@TenMH", SqlDbType.NVarChar, 50, "TenMH");
            cmThemMonHoc.Parameters.Add("@SoTiet", SqlDbType.Int, 5, "SoTiet");
            cmThemMonHoc.Parameters.Add("@MaHK", SqlDbType.Int, 5, "MaHK");
            cmThemMonHoc.Parameters.Add("@MaKhoi", SqlDbType.Int, 5, "MaKhoi");
            daMonHoc.InsertCommand = cmThemMonHoc;

            //Tạo đối tượng command thực thi câu lệnh truy vấn update
            string sSuaMonHoc = @"Update MonHoc set TenMH=@TenMH,SoTiet=@SoTiet,MaHK=@MaHK,MaKhoi=@MaKhoi where MaMH=@MaMH";
            SqlCommand cmSuMonHoc = new SqlCommand(sSuaMonHoc, con);
            cmSuMonHoc.Parameters.Add("@TenMH", SqlDbType.NVarChar, 50, "TenMH");
            cmSuMonHoc.Parameters.Add("@SoTiet", SqlDbType.Int, 5, "SoTiet");
            cmSuMonHoc.Parameters.Add("@MaHK", SqlDbType.Int, 5, "MaHK");
            cmSuMonHoc.Parameters.Add("@MaKhoi", SqlDbType.Int, 5, "MaKhoi");
            cmSuMonHoc.Parameters.Add("@MaMH", SqlDbType.Int, 5, "MaMH");
            daMonHoc.UpdateCommand = cmSuMonHoc;

            //Tạo đối tượng command xóa dữ liệu 
            string sXoaMonHoc = @"Delete From MonHoc where MaMH=@MaMH";
            SqlCommand cmXoaMonHoc = new SqlCommand(sXoaMonHoc, con);
            cmXoaMonHoc.Parameters.Add("@MaMH", SqlDbType.Int, 5, "MaMH");
            daMonHoc.DeleteCommand = cmXoaMonHoc;
              dgvMonHoc.Columns["MaKhoi"].Visible = false;
            dgvMonHoc.Columns["MaHK"].Visible = false;


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

        //Hàm load thông tin Học kỳ
        private void LoadHocKy()
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelectKhoi = @"Select * From HocKy";
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daHocKy = new SqlDataAdapter(sSelectKhoi, sChuoiKetNoi);
            //Đổ dữ liệu vào 1 bảng trong dataset
            daHocKy.Fill(ds, "tblHocKy");
            cbHocKy.DataSource = ds.Tables["tblHocKy"];
            cbHocKy.ValueMember = "MaHK";
            cbHocKy.DisplayMember = "TenHK";

        }
        //Hàm lấy tên học kỳ
        private string LayTenHK(int MaHK)
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sTenHK = @"Select TenHK From HocKy Where MaHK=" + MaHK;
            SqlDataAdapter daTenHK = new SqlDataAdapter(sTenHK, sChuoiKetNoi);
            DataTable dt = new DataTable();
            daTenHK.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        private void TaoCot()
        {
            //Add columns TenHK,TenKhoi
            //Tạo ra cột TenNamHoc,TenKhoi
            DataGridViewColumn clTenHK = new DataGridViewColumn();
            DataGridViewColumn clTenKhoi = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            clTenHK.CellTemplate = cell;
            clTenHK.Name = "TenHK";
            clTenHK.HeaderText = "Tên học kỳ";
            dgvMonHoc.Columns.Add(clTenHK);
            //clTenKhoi.CellTemplate = cell1;
            clTenKhoi.CellTemplate = cell;
            clTenKhoi.Name = "TenKhoi";
            clTenKhoi.HeaderText = "Tên khối";
            dgvMonHoc.Columns.Add(clTenKhoi);
            //Đỗ dữ liệu vào 2 cột vừa tạo
            for (int i = 0; i < dgvMonHoc.RowCount; i++)
            {
                string tenHK = LayTenHK(Int16.Parse(dgvMonHoc.Rows[i].Cells["MaHK"].Value.ToString()));
                string tenKhoi = LayTenKhoi(Int16.Parse(dgvMonHoc.Rows[i].Cells["MaKhoi"].Value.ToString()));
                dgvMonHoc.Rows[i].Cells["TenHK"].Value = tenHK;
                dgvMonHoc.Rows[i].Cells["TenKhoi"].Value = tenKhoi;


            }
          
            
         
        }
        //Hàm lấy tên khối
        private string LayTenKhoi(int Khoi)
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sTenKhoi = @"Select TenKhoi From Khoi Where MaKhoi=" + Khoi;
            SqlDataAdapter daTenKhoi = new SqlDataAdapter(sTenKhoi, sChuoiKetNoi);
            DataTable dt = new DataTable();
            daTenKhoi.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dr = dgvMonHoc.SelectedRows[0];
                dgvMonHoc.Rows.Remove(dr);
                MessageBox.Show("Xóa thành công", "Thông báo");
                try
                {
                    daMonHoc.Update(ds, "tblLopHoc");
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

        private void btThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
            return;
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            ds.Tables["tblMonHoc"].RejectChanges();
        }

        private void dgvMonHoc_Click(object sender, EventArgs e)
        {

            try
            {
                DataGridViewRow dr = dgvMonHoc.SelectedRows[0];
                txtMaMonHoc.Text = dr.Cells["MaMH"].Value.ToString();
                txtTenMonHoc.Text = dr.Cells["TenMH"].Value.ToString();
                cbHocKy.SelectedValue = dr.Cells["MaHK"].Value.ToString();
                cbKhoi.SelectedValue = dr.Cells["MaKhoi"].Value.ToString();
            }
            catch (Exception ex)
            {

                return;
            }
        }
        public void MaMonHocCuoiCungTruocKhiThem()
        {

            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sMaMonHocCuoiCung = @"Select MaMH From MonHoc";
            SqlDataAdapter daMonHoc = new SqlDataAdapter(sMaMonHocCuoiCung, sChuoiKetNoi);
            DataTable dt = new DataTable();
            daMonHoc.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                // Lấy chỉ số phần tử dòng cuối cùng 
                int iDongCuoi = dt.Rows.Count - 1;
                maMonHocCuoiCung = int.Parse(dt.Rows[iDongCuoi][0].ToString());
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiểm tra tính hợp lệ đầu vào dữ liệu
                if (txtTenMonHoc.Text == "")
                {
                    MessageBox.Show("Bạn cần phải nhập đầy đủ thông tin!",
                        "Thông báo");
                    return;
                }
                //Thêm 1 dòng vào tblNienKhoa
                DataRow r = ds.Tables["tblMonHoc"].NewRow();
                //Nhập giá trị vào các trường tương ứng trên Datarow
                r["TenMH"] = txtTenMonHoc.Text;
                r["MaKhoi"] = Int16.Parse(cbKhoi.SelectedValue.ToString());
                r["MaHK"] = Int16.Parse(cbHocKy.SelectedValue.ToString());

                if (maMonHocCuoiCung == 0)
                {
                    MaMonHocCuoiCungTruocKhiThem();
                }
                maMonHocCuoiCung++;
                r["MaMH"] = maMonHocCuoiCung;
                ds.Tables["tblMonHoc"].Rows.Add(r);
                dgvMonHoc.Rows[dgvMonHoc.RowCount - 2].Cells["TenHK"].Value = LayTenHK(Int16.Parse(cbHocKy.SelectedValue.ToString()));
                dgvMonHoc.Rows[dgvMonHoc.RowCount - 2].Cells["TenKhoi"].Value = LayTenKhoi(Int16.Parse(cbKhoi.SelectedValue.ToString()));
          

            }
            catch (Exception ex)
            {
                return;
            }
            try
            {
                daMonHoc.Update(ds, "tblMonHoc");
                MessageBox.Show("Thêm thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm thất bại!", "Thông báo");
                return;
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daMonHoc.Update(ds, "tblMonHoc");
                MessageBox.Show("Lưu thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {

            DataGridViewRow dr = dgvMonHoc.SelectedRows[0];
            dgvMonHoc.BeginEdit(true);
            dr.Cells["MaMH"].Value = Int16.Parse(txtMaMonHoc.Text.ToString());
            dr.Cells["TenMH"].Value = txtTenMonHoc.Text;
            dr.Cells["MaKhoi"].Value = Int16.Parse(cbKhoi.SelectedValue.ToString());
            dr.Cells["MaHK"].Value = Int16.Parse(cbHocKy.SelectedValue.ToString());
            dr.Cells["TenHK"].Value = LayTenHK(Int16.Parse(cbHocKy.SelectedValue.ToString()));
            dr.Cells["TenKhoi"].Value = LayTenKhoi(Int16.Parse(cbKhoi.SelectedValue.ToString()));
            dgvMonHoc.EndEdit();
            MessageBox.Show("Cập nhật dữ liệu thành công !", "Thông báo");
        }

       
    }
}
