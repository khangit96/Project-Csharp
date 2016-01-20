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
    public partial class frmNhapDiem : Form
    {
        public frmNhapDiem()
        {
            InitializeComponent();
        }
        DataSet ds;
        SqlDataAdapter daNienKhoa;
        SqlDataAdapter daKhoi;
        private void frmNhapDiem_Load(object sender, EventArgs e)
        {

            //Khởi tạo dataset
            ds = new DataSet("DsQuanLyHocSinh");
            //Load cmb niên khóa 
            LoadcmbNienKhoa();
            //Load cmb Khối
            LoadcmbKhoi();
            //Load cmbHocKy
            LoadcmbHocKy();
            //Load cmbLop
            LoadcmbLop();
            //Load Label thông tin nhập điêm
            LoadLabel();
            
            
            
        }
       
        //Load cmb niên khóa
        private void LoadcmbNienKhoa()
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelectNienKhoa = @"Select * From NienKhoa";
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daNienKhoa = new SqlDataAdapter(sSelectNienKhoa,sChuoiKetNoi);
            //Đổ dữ liệu vào 1 bảng trong dataset
            daNienKhoa.Fill(ds, "tblNienKhoa");
            cmbNienKhoa.DataSource = ds.Tables["tblNienKhoa"];
            cmbNienKhoa.ValueMember = "MaNamHoc";
            cmbNienKhoa.DisplayMember = "TenNamHoc";
        }
        //Load cmb Khối
        private void LoadcmbKhoi()
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelectKhoi = @"Select * From Khoi";
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daKhoi = new SqlDataAdapter(sSelectKhoi, sChuoiKetNoi);
            //Đổ dữ liệu vào 1 bảng trong dataset
            daKhoi.Fill(ds, "tblKhoi");
            cmbKhoi.DataSource = ds.Tables["tblKhoi"];
            cmbKhoi.ValueMember = "MaKhoi";
            cmbKhoi.DisplayMember = "TenKhoi";
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {

        }
        //Tạo đối tượng DataAdapter load cmbLop dựa vào cmbNienKhoa
        SqlDataAdapter daLop;
        private void cmbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNienKhoa.SelectedValue != null &&!(cmbNienKhoa.SelectedValue is DataRowView)
                && cmbKhoi.SelectedValue!=null&& !(cmbKhoi.SelectedValue is DataRowView) //Bổ sung load cmb lớp 
                )
            {
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectLop = @"Select * From Lop where MaNamHoc="+cmbNienKhoa.SelectedValue+"and MaKhoi="+cmbKhoi.SelectedValue;//Bổ sung
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daLop = new SqlDataAdapter(sSelectLop, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong dataset
                daLop.Fill(dt);
                cmbLop.DataSource = dt;
                cmbLop.ValueMember = "MaLop";
                cmbLop.DisplayMember = "TenLop";
                lblNienKhoa.Text = cmbNienKhoa.Text;
            }
            //Load dgv học sinh khi cmb thay đổi
            LoadDSDiemHS();
        }

        //Tạo đối tượng DataAdapter load cmbHocKy
        SqlDataAdapter daHocKy;
        private void LoadcmbHocKy()
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelectHocKy = @"Select * From HocKy";
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daHocKy = new SqlDataAdapter(sSelectHocKy, sChuoiKetNoi);
            DataTable dt = new DataTable();
            //Đổ dữ liệu vào 1 bảng trong dataset
            daHocKy.Fill(dt);
            cmbHocKy.DataSource = dt;
            cmbHocKy.ValueMember = "MaHK";
            cmbHocKy.DisplayMember = "TenHK";

            
    
        }
        SqlDataAdapter daMonHoc;
        private void cmbKhoi_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbKhoi.SelectedValue != null && !(cmbKhoi.SelectedValue is DataRowView) 
                && cmbHocKy.SelectedValue!=null&&!(cmbHocKy.SelectedValue is DataRowView)
                && cmbNienKhoa.SelectedValue!=null&&!(cmbNienKhoa.SelectedValue is DataRowView) //Bổ sung điều kiện để load cmblop
                )
            {
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectMonHoc = @"Select * From MonHoc where MaKhoi=" + cmbKhoi.SelectedValue+" and MaHK ="+cmbHocKy.SelectedValue;
                string sSlectLop = @"Select * From Lop where MaKhoi=" + cmbKhoi.SelectedValue+"and MaNamHoc="+cmbNienKhoa.SelectedValue;//(Bổ sung)
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daMonHoc = new SqlDataAdapter(sSelectMonHoc, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong dataset
                daMonHoc.Fill(dt);
                cmbMonHoc.DataSource = dt;
                cmbMonHoc.ValueMember = "MaMH";
                cmbMonHoc.DisplayMember = "TenMH";
                lblKhoi.Text = cmbKhoi.Text;

                //Bổ sung cmbLop
                DataTable dtLop = new DataTable();
                daLop = new SqlDataAdapter(sSlectLop, sChuoiKetNoi);
                daLop.Fill(dtLop);
                cmbLop.DataSource = dtLop;
                cmbLop.DisplayMember = "TenLop";
                cmbLop.ValueMember = "MaLop";
            }
            //Load lại dgv khi cmb thay đổi
            LoadDSDiemHS();
        }
        
        private void cmbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKhoi.SelectedValue != null && !(cmbKhoi.SelectedValue is DataRowView)
               && cmbHocKy.SelectedValue != null && !(cmbHocKy.SelectedValue is DataRowView)
               )
            {
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectMonHoc = @"Select * From MonHoc where MaKhoi=" + cmbKhoi.SelectedValue + " and MaHK =" + cmbHocKy.SelectedValue;
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daMonHoc = new SqlDataAdapter(sSelectMonHoc, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong dataset
                daMonHoc.Fill(dt);
                cmbMonHoc.DataSource = dt;
                cmbMonHoc.ValueMember = "MaMH";
                cmbMonHoc.DisplayMember = "TenMH";
                lblHocKy.Text = cmbHocKy.Text;
            }
            //Load dgv học sinh khi cmb thay đổi
            LoadDSDiemHS();
        }
        //SqlDataAdapter daBangDiem;
        ////Load dgvBangDiem
        //private void LoadDataGridViewBangDiem()
        //{
        //    //Chuỗi kết nối 
        //    string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
        //    //Chuỗi truy vấn
        //    string sSelectDiem = @"Select * From Diem";
        //    //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
        //    daBangDiem = new SqlDataAdapter(sSelectDiem, sChuoiKetNoi);
        //    //Đổ dữ liệu vào 1 bảng trong dataset
        //    daBangDiem.Fill(ds, "tblBangDiem");
        //    //Thêm vào cột họ tên bên trái dgvBangDiem
        //    ThemCotHoTenDauTien();
        //    dgvBangDiem.DataSource = ds.Tables["tblBangDiem"];
        
        //}

        //Load label
        private void LoadLabel()
        {
            lblHocKy.Text = cmbHocKy.Text;
            lblLop.Text = cmbLop.Text;
            lblMonHoc.Text = cmbMonHoc.Text;
            lblNienKhoa.Text = cmbNienKhoa.Text;
            lblKhoi.Text = cmbKhoi.Text;
        }

        private void cmbLop_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblLop.Text = cmbLop.Text;
            //Load dgv học sinh khi cmb thay đổi
            LoadDSDiemHS();
        }

        private void cmbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMonHoc.Text = cmbMonHoc.Text;
            //Load dgv học sinh khi cmb thay đổi
            LoadDSDiemHS();

        }
        //Load cmbLop 
        private void LoadcmbLop()
        {

            if (cmbKhoi.SelectedValue != null && !(cmbKhoi.SelectedValue is DataRowView)
                && cmbHocKy.SelectedValue != null && !(cmbHocKy.SelectedValue is DataRowView)
                && cmbNienKhoa.SelectedValue != null && !(cmbNienKhoa.SelectedValue is DataRowView) //Bổ sung điều kiện để load cmblop
                )
            {
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectMonHoc = @"Select * From MonHoc where MaKhoi=" + cmbKhoi.SelectedValue + " and MaHK =" + cmbHocKy.SelectedValue;
                string sSlectLop = @"Select * From Lop where MaKhoi=" + cmbKhoi.SelectedValue + "and MaNamHoc=" + cmbNienKhoa.SelectedValue;//(Bổ sung)
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daMonHoc = new SqlDataAdapter(sSelectMonHoc, sChuoiKetNoi);
                DataTable dt = new DataTable();
          
                //Bổ sung cmbLop
                DataTable dtLop = new DataTable();
                daLop = new SqlDataAdapter(sSlectLop, sChuoiKetNoi);
                daLop.Fill(dtLop);
                cmbLop.DataSource = dtLop;
                cmbLop.DisplayMember = "TenLop";
                cmbLop.ValueMember = "MaLop";
            }
        
        }
        
        SqlDataAdapter daHocSinh;
        private void LoadDSDiemHS()
        {
            if (cmbKhoi.SelectedValue != null && !(cmbKhoi.SelectedValue is DataRowView)
              && cmbHocKy.SelectedValue != null && !(cmbHocKy.SelectedValue is DataRowView)
              && cmbLop.SelectedValue != null && !(cmbLop.SelectedValue is DataRowView)
              && cmbNienKhoa.SelectedValue != null && !(cmbNienKhoa.SelectedValue is DataRowView)
              && cmbMonHoc.SelectedValue != null && !(cmbMonHoc.SelectedValue is DataRowView)
              )
            {
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectDiem = string.Format(@"Select HocSinh.MaHS,HocSinh.HoTen,Diem.* From Diem,HocSinh,Lop,NienKhoa,Khoi,HocKy,MonHoc where HocSinh.MaLop=Lop.MaLop and Lop.MaKhoi=Khoi.MaKhoi and Lop.MaNamHoc=NienKhoa.MaNamHoc and MonHoc.MaHK=HocKy.MaHK and Diem.MaHS=HocSinh.MaHS and NienKhoa.MaNamHoc={0} and Lop.MaLop={1} and Khoi.MaKhoi={2} and MonHoc.MaMH={3} and HocKy.MaHK ={4}",cmbNienKhoa.SelectedValue,cmbLop.SelectedValue,cmbKhoi.SelectedValue,cmbMonHoc.SelectedValue,cmbHocKy.SelectedValue);
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daHocSinh = new SqlDataAdapter(sSelectDiem, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong datatable
                daHocSinh.Fill(dt);
                if(dt.Rows.Count!=0)
                { 
                    //Nếu có điểm thì gán datasource
                    dgvBangDiem.DataSource = dt;
                }
                else 
                { 
                  dgvBangDiem.DataSource = null;
                }
            }

        }
        //Load dgvBangDiem những học sinh chưa nhập điểm 
        public void LoaddgvHSNhapDiem()
        {
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelectDiem = string.Format(@"Select MonHoc.MaMH,HocSinh.MaHS,HocSinh.HoTen From HocSinh,Lop,NienKhoa,Khoi,HocKy,MonHoc where HocSinh.MaLop=Lop.MaLop and Lop.MaKhoi=Khoi.MaKhoi and Lop.MaNamHoc=NienKhoa.MaNamHoc and MonHoc.MaHK=HocKy.MaHK and NienKhoa.MaNamHoc={0} and Lop.MaLop={1} and Khoi.MaKhoi={2} and MonHoc.MaMH={3} and HocKy.MaHK ={4} and HocSinh.MaHS NOT IN (select MaHS From Diem)  ", cmbNienKhoa.SelectedValue, cmbLop.SelectedValue, cmbKhoi.SelectedValue, cmbMonHoc.SelectedValue, cmbHocKy.SelectedValue);
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daHocSinh = new SqlDataAdapter(sSelectDiem, sChuoiKetNoi);
            DataTable dt = new DataTable();
            //Đổ dữ liệu vào 1 bảng trong datatable
            daHocSinh.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                //Nếu có điểm thì gán datasource
                dgvBangDiem.DataSource = dt;
            }
            else
            {
                dgvBangDiem.DataSource = null;
            }
        }
        private bool NutNhan = true;
        int TaoCot=0;
        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
                //Gọi hàm để load dgv học sinh chưa nhập điểm
                LoaddgvHSNhapDiem();
                if(dgvBangDiem.Rows.Count==0||dgvBangDiem.DataSource==null)
                {
                    MessageBox.Show("Học sinh lớp"+lblLop.Text+"đã được nhập điểm rồi !","Thông Báo!");
                    NutNhan = true;
                    LoadDSDiemHS();
                    return;
                }
            if(TaoCot==0)
            { 
                //Thêm vào một cột điểm cho dgvBangDiem
                    //Cột điểm miệng
                    DataGridViewColumn clDiemMieng = new DataGridViewColumn();
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    clDiemMieng.CellTemplate = cell;
                    clDiemMieng.Name = "DiemMieng";
                    dgvBangDiem.Columns.Add(clDiemMieng);
                    //Cột điểm 15 phút
                    DataGridViewColumn clDiem15Phut = new DataGridViewColumn();
                    clDiem15Phut.CellTemplate = cell;
                    clDiem15Phut.Name = "Diem15Phut";
                    dgvBangDiem.Columns.Add(clDiem15Phut);
                    //Một tiết
                    DataGridViewColumn cl1Tiet = new DataGridViewColumn();
                    cl1Tiet.CellTemplate = cell;
                    cl1Tiet.Name = "Diem1Tiet";
                    dgvBangDiem.Columns.Add(cl1Tiet);
                    //Thi
                    DataGridViewColumn clDiemThi = new DataGridViewColumn();
                    clDiemThi.CellTemplate = cell;
                    clDiemThi.Name = "DiemThi";
                    dgvBangDiem.Columns.Add(clDiemThi);
                    TaoCot++;
                    //Nút nhấn 
                    NutNhan = false;
            }
               

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (NutNhan == false)
            {
                try
                {

                    //Chuỗi kết nối 
                    string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                    //Khởi tạo đối tượng connection
                    SqlConnection con = new SqlConnection(sChuoiKetNoi);
                    con.Open();
                    for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                    {
                        //Tạo biến trung gian 
                        int sMaMH = int.Parse(dgvBangDiem.Rows[i].Cells["MaMH"].Value.ToString());
                        int sMaHS = int.Parse(dgvBangDiem.Rows[i].Cells["MaHS"].Value.ToString());
                        int sDiemMieng = int.Parse(dgvBangDiem.Rows[i].Cells["DiemMieng"].Value.ToString());
                        int sDiem15Phut = int.Parse(dgvBangDiem.Rows[i].Cells["Diem15Phut"].Value.ToString());
                        int sDiem1Tiet = int.Parse(dgvBangDiem.Rows[i].Cells["Diem1Tiet"].Value.ToString());
                        int sDiemThi = int.Parse(dgvBangDiem.Rows[i].Cells["DiemThi"].Value.ToString());
                        int DTB = (sDiemMieng + sDiem15Phut + sDiem1Tiet * 2 + sDiemThi * 3) / 7;

                        //Chuỗi truy vấn
                        string sInsert = string.Format("INSERT INTO Diem values ({0},{1},{2},{3},{4},{5},{6})", sMaMH, sMaHS, sDiemMieng, sDiem15Phut, sDiem1Tiet, sDiemThi, DTB);
                        SqlCommand cm = new SqlCommand(sInsert, con);
                        cm.ExecuteNonQuery();

                    }
                    con.Close();
                    NutNhan = true;
                    MessageBox.Show("Nhập điểm thành công !", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nhập điểm thất bại!", "Thông báo");
                }
            }
            else
            {
                try
                {
                    //Chuỗi kết nối 
                    string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                    //Khởi tạo đối tượng connection
                    SqlConnection con = new SqlConnection(sChuoiKetNoi);
                    con.Open();
                    for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                    {
                        //Tạo biến trung gian 
                        int sMaMH = int.Parse(dgvBangDiem.Rows[i].Cells["MaMH"].Value.ToString());
                        int sMaHS = int.Parse(dgvBangDiem.Rows[i].Cells["MaHS"].Value.ToString());
                        int sDiemMieng = int.Parse(dgvBangDiem.Rows[i].Cells["DiemMieng"].Value.ToString());
                        int sDiem15Phut = int.Parse(dgvBangDiem.Rows[i].Cells["Diem15Phut"].Value.ToString());
                        int sDiem1Tiet = int.Parse(dgvBangDiem.Rows[i].Cells["Diem1Tiet"].Value.ToString());
                        int sDiemThi = int.Parse(dgvBangDiem.Rows[i].Cells["DiemThi"].Value.ToString());
                        int DTB = (sDiemMieng + sDiem15Phut + sDiem1Tiet * 2 + sDiemThi * 3) / 7;

                        //Chuỗi truy vấn
                        string sInsert = string.Format("Update Diem set DiemMieng={0},Diem15Phut={1},Diem1Tiet={2},DiemThi={3},DiemTB={4} where MaHS={5} and MaMH={6}", sDiemMieng, sDiem15Phut, sDiem1Tiet, sDiemThi, DTB,sMaHS,sMaMH);
                        SqlCommand cm = new SqlCommand(sInsert, con);
                        cm.ExecuteNonQuery();

                    }
                    con.Close();
                    NutNhan = true;
                    MessageBox.Show("Cập nhật thành công !", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cập nhật thất bại !", "Thông báo");
                }
            }
           
        }

            private void btnXemDiem_Click(object sender, EventArgs e)
            {
                //Load lại danh sách điểm
                LoadDSDiemHS();
                //Remove cột sau khi thêm điểm
                if(TaoCot!=0)
                {
                    dgvBangDiem.Columns.Remove("DiemMieng");
                    dgvBangDiem.Columns.Remove("Diem15Phut");
                    dgvBangDiem.Columns.Remove("Diem1Tiet");
                    dgvBangDiem.Columns.Remove("DiemThi");
                }

                NutNhan = true;
            }

            private void btnTinhDiem_Click(object sender, EventArgs e)
            {
                //Tính điểm trung bình 
                try
                {
                    if (dgvBangDiem.Rows.Count != 0 || dgvBangDiem.DataSource != null)
                    {
                        for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                        {
                            //Tạo biến trung gian 
                            int sMaMH = int.Parse(dgvBangDiem.Rows[i].Cells["MaMH"].Value.ToString());
                            int sMaHS = int.Parse(dgvBangDiem.Rows[i].Cells["MaHS"].Value.ToString());
                            int sDiemMieng = int.Parse(dgvBangDiem.Rows[i].Cells["DiemMieng"].Value.ToString());
                            int sDiem15Phut = int.Parse(dgvBangDiem.Rows[i].Cells["Diem15Phut"].Value.ToString());
                            int sDiem1Tiet = int.Parse(dgvBangDiem.Rows[i].Cells["Diem1Tiet"].Value.ToString());
                            int sDiemThi = int.Parse(dgvBangDiem.Rows[i].Cells["DiemThi"].Value.ToString());
                            int DTB = (sDiemMieng + sDiem15Phut + sDiem1Tiet * 2 + sDiemThi * 3) / 7;
                            dgvBangDiem.Rows[i].Cells["DiemTB"].Value = DTB;

                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            private void btnThoat_Click(object sender, EventArgs e)
            {
                frmMain.pnl.Location = new Point(frmMain.iDoRongFormMainBanDau, 50);
                frmMain.iDoRongFormMain = frmMain.iDoRongFormMainBanDau;
                frmMain.pnl.Size = new Size(0, 0);
            }
        }
       


}
