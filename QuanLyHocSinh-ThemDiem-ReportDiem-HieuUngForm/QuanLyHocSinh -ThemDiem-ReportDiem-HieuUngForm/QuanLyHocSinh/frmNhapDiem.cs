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
        DataView dv;
        SqlDataAdapter daDiem;
        SqlDataAdapter daNienKhoa;
        SqlDataAdapter daKhoi;
        private bool NutNhan = false;//dùng để nhận biết xem người dùng đã nhấn nút nhập điểm hay chưa.Nếu nhấn nút nhập điểm thì insert điểm còn ko thì update điểm
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
            //load điểm
            removeCot();
            loadDiem();
            
            
            
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
            NutNhan = false;
            removeCot();
            //nếu chọn niên khóa mà khác null
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
                //kiểm tra nếu không tìm đk lớp theo điều kiện
                if (dt.Rows.Count!= 0)
                {
                    cmbLop.DataSource = dt;
                    cmbLop.ValueMember = "MaLop";
                    cmbLop.DisplayMember = "TenLop";
                    lblNienKhoa.Text = cmbNienKhoa.Text;
                    //Load dgv học sinh khi cmb thay đổi
                   // LoadDSDiemHS();
                    removeCot();
                    for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                    {
                        dgvBangDiem.Rows.RemoveAt(i);
                    }
                    dgvBangDiem.DataSource = null;
                    removeCot();
                    loadDiem();
                }
                else
                {
                    cmbLop.DataSource = null;//gán combobo==null
                    cmbLop.ValueMember = "MaLop";
                    cmbLop.DisplayMember = "TenLop";
                    lblNienKhoa.Text = cmbNienKhoa.Text;
                    lblLop.Text = "";
                    dgvBangDiem.DataSource = null;
                }
              
            }
          
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
            NutNhan = false;
            removeCot();
            if (cmbKhoi.SelectedValue != null && !(cmbKhoi.SelectedValue is DataRowView) 
                && cmbHocKy.SelectedValue!=null&&!(cmbHocKy.SelectedValue is DataRowView)
                && cmbNienKhoa.SelectedValue!=null&&!(cmbNienKhoa.SelectedValue is DataRowView) //Bổ sung điều kiện để load cmblop
                )
            {
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectMonHoc = @"Select * From MonHoc where MaKhoi=" + cmbKhoi.SelectedValue+" and MaHK ="+cmbHocKy.SelectedValue;
                string sSlectLop = @"Select * From Lop where MaNamHoc=" + cmbNienKhoa.SelectedValue + "and MaKhoi=" + cmbKhoi.SelectedValue;//(Bổ sung)
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daMonHoc = new SqlDataAdapter(sSelectMonHoc, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong dataset
                daMonHoc.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    cmbMonHoc.DataSource = dt;
                    cmbMonHoc.ValueMember = "MaMH";
                    cmbMonHoc.DisplayMember = "TenMH";
                    lblKhoi.Text = cmbKhoi.Text;
                    //Load lại dgv khi cmb thay đổi
                    //LoadDSDiemHS();
                    removeCot();
                    for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                    {
                        dgvBangDiem.Rows.RemoveAt(i);
                    }
                    dgvBangDiem.DataSource = null;
                    removeCot();
                    loadDiem();
                }
                else
                {
                    cmbMonHoc.DataSource =null;
                    cmbMonHoc.ValueMember = "MaMH";
                    cmbMonHoc.DisplayMember = "TenMH";
                    lblKhoi.Text = cmbKhoi.Text;
                    lblMonHoc.Text = "";
                    dgvBangDiem.DataSource = null;
                }

                //Bổ sung cmbLop
                DataTable dtLop = new DataTable();
                daLop = new SqlDataAdapter(sSlectLop, sChuoiKetNoi);
                daLop.Fill(dtLop);
                if (dtLop.Rows.Count != 0)
                {
                    cmbLop.DataSource = dtLop;
                    cmbLop.DisplayMember = "TenLop";
                    cmbLop.ValueMember = "MaLop";
                }
                else
                {
                    cmbLop.DataSource = null;//gán combobo==null
                    cmbLop.ValueMember = "MaLop";
                    cmbLop.DisplayMember = "TenLop";
                    lblNienKhoa.Text = cmbNienKhoa.Text;
                    lblLop.Text = "";
                    dgvBangDiem.DataSource = null;
                }
            }
          
        }
        
        private void cmbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeCot();
            NutNhan = false;
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
                if (dt.Rows.Count!=0)
                {
                    cmbMonHoc.DataSource = dt;
                    cmbMonHoc.ValueMember = "MaMH";
                    cmbMonHoc.DisplayMember = "TenMH";
                    lblHocKy.Text = cmbHocKy.Text;
                    removeCot();
                    for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                    {
                        dgvBangDiem.Rows.RemoveAt(i);
                    }
                    dgvBangDiem.DataSource = null;
                    removeCot();
                    loadDiem();
                }
                else
                {
                    cmbMonHoc.DataSource = null;
                    cmbMonHoc.ValueMember = "MaMH";
                    cmbMonHoc.DisplayMember = "TenMH";
                    lblMonHoc.Text = "";
                    lblHocKy.Text = cmbHocKy.Text;
                }
            }
            //Load dgv học sinh khi cmb thay đổi
           // LoadDSDiemHS();
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
            NutNhan = false;
            removeCot();
            lblLop.Text = cmbLop.Text;
            //Load dgv học sinh khi cmb thay đổi
            //LoadDSDiemHS();
            removeCot();
            for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
            {
                dgvBangDiem.Rows.RemoveAt(i);
            }
            dgvBangDiem.DataSource = null;
            removeCot();
            loadDiem();
        }

        private void cmbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            NutNhan = false;
            removeCot();
           for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
           {
               dgvBangDiem.Rows.RemoveAt(i);
           }
           dgvBangDiem.DataSource = null;
           removeCot();
            loadDiem();

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
            else
            {
                
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
                    dgvBangDiem.Columns["MaHS"].HeaderText = "Mã Hs";
                    dgvBangDiem.Columns["MaHS1"].Visible = false;
                    dgvBangDiem.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvBangDiem.Columns["MaMH"].HeaderText = "Mã môn học";
                    dgvBangDiem.Columns["DiemMieng"].HeaderText = "Điểm miệng";
                    dgvBangDiem.Columns["Diem15Phut"].HeaderText = "Điểm 15 phút";
                    dgvBangDiem.Columns["Diem1Tiet"].HeaderText = "Điểm 1 tiết";
                    dgvBangDiem.Columns["DiemThi"].HeaderText = "Điểm thi";
                    dgvBangDiem.Columns["DiemTb"].HeaderText = "Điểm trung bình";
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
        int TaoCot=0;
        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            //kiểm tra xem có môn học nào đk chọn để nhập điểm hay không
            if (cmbMonHoc.Items.Count == 0||cmbNienKhoa.Items.Count==0||cmbLop.Items.Count==0)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!");
                NutNhan = false;
                return;
            }
            else
            {   
                
                int countHocSinhLop = countHocSinh_Lop();//biến đếm số học sinh của lớp học
                if (countHocSinhLop == 0)//lớp học chưa đk nhập học sinh
                {
                    MessageBox.Show("Vui lòng nhập học sinh cho lớp học!");
                    NutNhan = false;
                    return;
                }
                int countHocSinhNhapDiemMonHoc = CountNhapDiem_MonHoc();//biến đếm số học sinh đk nhập điểm theo môn học đã đk chọn
                if (countHocSinhNhapDiemMonHoc == countHocSinhLop)//nếu học sinh đã đk nhập hết điểm
                {
                    MessageBox.Show("Học sinh đã đk nhập điểm hết cho môn "+cmbMonHoc.Text+"!");
                    NutNhan = false;
                    removeCot();
                    dgvBangDiem.DataSource = null;
                    loadDiem();
                }
                else
                {
                    NutNhan = true;
                    removeCot();
                    dgvBangDiem.DataSource = null;
                    hocSinhChuaNhapDiem();
                }
            }
             

        }
         
        //hàm lưu điểm và database
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (NutNhan == true)//người dùng đã chọn nút nhập điểm và insert dữ liệu vào
                {
                    try
                    {
                        //Chuỗi kết nối 
                        string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                        //Khởi tạo đối tượng connection
                        SqlConnection con = new SqlConnection(sChuoiKetNoi);
                        con.Open();
                        int sMaMH= layMaMH();
                        for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                        {
                            DataGridViewRow row = dgvBangDiem.Rows[i];
                            //Kiểm tra xem người dùng đã chọn tính điểm trung bình ch 
                          //  int sMaMH = int.Parse(row.Cells[2].Value.ToString());
                            int sMaHS = int.Parse(row.Cells[0].Value.ToString());
                            int sDiemMieng = int.Parse(row.Cells[4].Value.ToString());
                            int sDiem15Phut = int.Parse(row.Cells[5].Value.ToString());
                            int sDiem1Tiet = int.Parse(row.Cells[6].Value.ToString());
                            int sDiemThi = int.Parse(row.Cells[7].Value.ToString());
                            int sDiemTB = int.Parse(row.Cells[8].Value.ToString());
                            string sInsert = string.Format("INSERT INTO Diem values ({0},{1},{2},{3},{4},{5},{6})", sMaMH, sMaHS, sDiemMieng, sDiem15Phut, sDiem1Tiet, sDiemThi, sDiemTB);
                            SqlCommand cm = new SqlCommand(sInsert, con);
                            cm.ExecuteNonQuery();

                        }
                        con.Close();
                        MessageBox.Show("Lưu điểm thành công !", "Thông báo");
                        removeCot();
                        for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                        {
                            dgvBangDiem.Rows.RemoveAt(i);
                        }
                        dgvBangDiem.DataSource = null;
                        removeCot();
                        loadDiem();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lưu điểm thất bại!", "Thông báo");
                    }
            }
            else
            {
                //Tiến hành thục hiện cập nhật điểm
                try
                {
                    //Chuỗi kết nối 
                    string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                    //Khởi tạo đối tượng connection
                    SqlConnection con = new SqlConnection(sChuoiKetNoi);
                    con.Open();
                   // int sMaMH = layMaMH();
                    for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                    {
                        DataGridViewRow row = dgvBangDiem.Rows[i];
                        //Kiểm tra xem người dùng đã chọn tính điểm trung bình ch 
                       /*
                        * int sMaMH = int.Parse(row.Cells[2].Value.ToString());
                        int sMaHS = int.Parse(row.Cells[0].Value.ToString());
                        int sDiemMieng = int.Parse(row.Cells[4].Value.ToString());
                        int sDiem15Phut = int.Parse(row.Cells[5].Value.ToString());
                        int sDiem1Tiet = int.Parse(row.Cells[6].Value.ToString());
                        int sDiemThi = int.Parse(row.Cells[7].Value.ToString());
                        int sDiemTB = int.Parse(row.Cells[8].Value.ToString());
                        */
                        int sMaMH = int.Parse(row.Cells[2].Value.ToString());
                        int sMaHS = int.Parse(row.Cells[0].Value.ToString());
                        int sDiemMieng = int.Parse(row.Cells[4].Value.ToString());
                        int sDiem15Phut = int.Parse(row.Cells[5].Value.ToString());
                        int sDiem1Tiet = int.Parse(row.Cells[6].Value.ToString());
                        int sDiemThi = int.Parse(row.Cells[7].Value.ToString());
                        int sDiemTB = int.Parse(row.Cells[8].Value.ToString());
                        string sUpdate = string.Format("Update Diem set DiemMieng={0},Diem15Phut={1},Diem1Tiet={2},DiemThi={3},DiemTB={4} where MaHS={5} and MaMH={6}", sDiemMieng, sDiem15Phut, sDiem1Tiet, sDiemThi, sDiemTB, sMaHS, sMaMH);
                        SqlCommand cm = new SqlCommand(sUpdate, con);
                        cm.ExecuteNonQuery();

                    }
                    con.Close();
                    MessageBox.Show("Cập nhật điểm thành công !", "Thông báo");
                    removeCot();
                    for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                    {
                        dgvBangDiem.Rows.RemoveAt(i);
                    }
                    dgvBangDiem.DataSource = null;
                    removeCot();
                    loadDiem();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cập nhật điểm thất bại!", "Thông báo");
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
            
        //Hàm tính điểm trung bình
            private void btnTinhDiem_Click(object sender, EventArgs e)
            {
                //Tính điểm trung bình 
                try
                {
                    if (dgvBangDiem.Rows.Count != 0 || dgvBangDiem.DataSource != null)
                    {
                        for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
                        {   
                            DataGridViewRow row = dgvBangDiem.Rows[i];
                            int sDiemMieng = int.Parse(row.Cells[4].Value.ToString());
                            int sDiem15Phut = int.Parse(row.Cells[5].Value.ToString());
                            int sDiem1Tiet = int.Parse(row.Cells[6].Value.ToString());
                            int sDiemThi = int.Parse(row.Cells[7].Value.ToString());
                            int DTB = (sDiemMieng + sDiem15Phut + sDiem1Tiet * 2 + sDiemThi * 3) / 7;
                            dgvBangDiem.Rows[i].Cells[8].Value = DTB;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ điểm!");
                }
            }

          //hàm load Điểm
        private void loadDiem(){
            try
            {
                    if (cmbKhoi.SelectedValue != null && !(cmbKhoi.SelectedValue is DataRowView)
                      && cmbHocKy.SelectedValue != null && !(cmbHocKy.SelectedValue is DataRowView)
                      && cmbLop.SelectedValue != null && !(cmbLop.SelectedValue is DataRowView)
                      && cmbNienKhoa.SelectedValue != null && !(cmbNienKhoa.SelectedValue is DataRowView)
                      && cmbMonHoc.SelectedValue != null && !(cmbMonHoc.SelectedValue is DataRowView)
                      )
                    {

                        lblMonHoc.Text = cmbMonHoc.Text;
                        //Chuỗi kết nối 
                        string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                        //Chuỗi truy vấn
                        string sSelectDiem = string.Format(@"Select HocSinh.MaHS,HocSinh.HoTen,Diem.* From Diem,HocSinh,Lop,NienKhoa,Khoi,HocKy,MonHoc where HocSinh.MaLop=Lop.MaLop and Lop.MaKhoi=Khoi.MaKhoi and Lop.MaNamHoc=NienKhoa.MaNamHoc and MonHoc.MaHK=HocKy.MaHK and Diem.MaHS=HocSinh.MaHS and Diem.MaMH=MonHoc.MaMH and NienKhoa.MaNamHoc={0} and Lop.MaLop={1} and Khoi.MaKhoi={2} and MonHoc.MaMH={3} and HocKy.MaHK ={4} and MonHoc.MaMH={5}", cmbNienKhoa.SelectedValue, cmbLop.SelectedValue, cmbKhoi.SelectedValue, cmbMonHoc.SelectedValue, cmbHocKy.SelectedValue, cmbMonHoc.SelectedValue);
                        //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                        daDiem = new SqlDataAdapter(sSelectDiem, sChuoiKetNoi);
                        daDiem.Fill(ds, "tblDiem");
                        dv = new DataView(ds.Tables["tblDiem"]);
                        //dgvDanhSachHocSinh.DataSource = ds.Tables["tblHocSinh"];
                        DataTable dt = new DataTable();
                        //Đổ dữ liệu vào 1 bảng trong datatable
                        daDiem.Fill(dt);
                        if (dt.Rows.Count != 0)
                        {
                            //Nếu có điểm thì gán datasource
                            //  dgvBangDiem.Columns
                            dgvBangDiem.DataSource = null;
                            dgvBangDiem.DataSource = dt;
                            // dgvBangDiem.DataSource = dv;
                            dgvBangDiem.Columns[2].HeaderText = "Mã môn học";                   
                            dgvBangDiem.Columns[0].HeaderText = "Mã học sinh";               
                             dgvBangDiem.Columns[1].HeaderText = "Họ tên";    
                            dgvBangDiem.Columns[3].Visible = false;
                            dgvBangDiem.Columns[4].HeaderText = "Điểm miệng";
                            dgvBangDiem.Columns[5].HeaderText = "Điểm 15 phút";
                           dgvBangDiem.Columns[6].HeaderText = "Điểm 1 tiết";
                           dgvBangDiem.Columns[7].HeaderText = "Điểm thi";
                          dgvBangDiem.Columns[8].HeaderText = "Điểm TB";

                        }
                        else
                        {
                            MessageBox.Show("Chưa có học sinh nào đk nhập điểm !Vui lòng nhập điểm cho học sinh");
                            dgvBangDiem.DataSource = null;
                        }

                    }
                    else
                    {
                        dgvBangDiem.DataSource = null;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không load đk điểm!");
            }

        }
        
        //hàm dùng để đếm số học sinh trong 1 lớp
        private int countHocSinh_Lop()
        {
            SqlDataAdapter daCountHocSinh;
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelect_Count = @"Select * From HocSinh Where MaLop=" + cmbLop.SelectedValue;
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daCountHocSinh = new SqlDataAdapter(sSelect_Count, sChuoiKetNoi);
            DataTable dt = new DataTable();
            //Đổ dữ liệu vào 1 bảng trong datatable
            daCountHocSinh.Fill(dt);
            return dt.Rows.Count;
            
        }
        

        //Hàm đếm xem bao nhiêu học sinh đk nhập điểm theo từng môn học
        private int CountNhapDiem_MonHoc()
        {
                SqlDataAdapter daCountNhapDiem;
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectNhapDiem = string.Format(@"Select HocSinh.MaHS,HocSinh.HoTen,Diem.* From Diem,HocSinh,Lop,NienKhoa,Khoi,HocKy,MonHoc where HocSinh.MaLop=Lop.MaLop and Lop.MaKhoi=Khoi.MaKhoi and Lop.MaNamHoc=NienKhoa.MaNamHoc and MonHoc.MaHK=HocKy.MaHK and Diem.MaHS=HocSinh.MaHS and Diem.MaMH=MonHoc.MaMH and NienKhoa.MaNamHoc={0} and Lop.MaLop={1} and Khoi.MaKhoi={2} and MonHoc.MaMH={3} and HocKy.MaHK ={4} and MonHoc.MaMH={5}", cmbNienKhoa.SelectedValue, cmbLop.SelectedValue, cmbKhoi.SelectedValue, cmbMonHoc.SelectedValue, cmbHocKy.SelectedValue, cmbMonHoc.SelectedValue);
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daCountNhapDiem = new SqlDataAdapter(sSelectNhapDiem, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong datatable
                daCountNhapDiem.Fill(dt);
                return dt.Rows.Count;   
            
        }


        //hàm lấy danh sách học sinh chưa nhập điểm theo môn
        private void hocSinhChuaNhapDiem()
        {
            removeCot();
            dgvBangDiem.DataSource = null;
            SqlDataAdapter daHocSinhNhapDiem;
            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn học sinh đã nhập điểm
            string sSelectNhapDiem = string.Format(@"Select HocSinh.MaHS,HocSinh.HoTen,Diem.* From Diem,HocSinh,Lop,NienKhoa,Khoi,HocKy,MonHoc where HocSinh.MaLop=Lop.MaLop and Lop.MaKhoi=Khoi.MaKhoi and Lop.MaNamHoc=NienKhoa.MaNamHoc and MonHoc.MaHK=HocKy.MaHK and Diem.MaHS=HocSinh.MaHS and Diem.MaMH=MonHoc.MaMH and NienKhoa.MaNamHoc={0} and Lop.MaLop={1} and Khoi.MaKhoi={2} and MonHoc.MaMH={3} and HocKy.MaHK ={4} and MonHoc.MaMH={5}", cmbNienKhoa.SelectedValue, cmbLop.SelectedValue, cmbKhoi.SelectedValue, cmbMonHoc.SelectedValue, cmbHocKy.SelectedValue, cmbMonHoc.SelectedValue);
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daHocSinhNhapDiem = new SqlDataAdapter(sSelectNhapDiem, sChuoiKetNoi);
            DataTable dtNhapDiem = new DataTable();
            //Đổ dữ liệu vào 1 bảng trong datatable
            daHocSinhNhapDiem.Fill(dtNhapDiem);
            int count = 0;//biến để đếm số học sinh chưa nhập điểm
            int countAddCell = 0;//biến để đếm số dòng để tạo
            //Chuỗi truy vấn học sinh chưa nhập điểm
            string sSelectChuaNhapDiem = @"Select * From HocSinh where MaLop="+cmbLop.SelectedValue;
            SqlDataAdapter daHocSinhChuaNhapDiem;
            daHocSinhChuaNhapDiem = new SqlDataAdapter(sSelectChuaNhapDiem, sChuoiKetNoi);
            DataTable dtChuaNhapDiem = new DataTable();
            daHocSinhChuaNhapDiem.Fill(dtChuaNhapDiem);
           //Remove các cột trong dgvBangDiem vì muốn tạo 1 dgvBangDiem khác
            removeCot();
           dgvBangDiem.DataSource = null;

            //Hàm tạo cột
           createColumns();
            //
            for (int i = 0; i < dgvBangDiem.Rows.Count; i++)
            {
                dgvBangDiem.Rows.RemoveAt(i);
             
            }
             //xử lí 
            for (int i = 0; i < dtChuaNhapDiem.Rows.Count; i++)
            {
                for(int j=0;j<dtNhapDiem.Rows.Count;j++){
                     if (dtChuaNhapDiem.Rows[i][0].ToString()!=dtNhapDiem.Rows[j][0].ToString()){
                         count++;
                     }           
                }
                if (count == dtNhapDiem.Rows.Count)
                {
                    string maHs = dtChuaNhapDiem.Rows[i][0].ToString();
                    string hoTen = dtChuaNhapDiem.Rows[i][1].ToString();
                    string[] row = new string[] { maHs,hoTen};
                    dgvBangDiem.Rows.Add(row);
                    countAddCell++;
                }
                count = 0;
               
            }
             

        }

        //Hàm remove cột dgvBangDiem
        private void removeCot()
        {        
          
            if (dgvBangDiem.Columns.Contains("MaHS"))
            {
                dgvBangDiem.Columns.Remove("MaHS");
            }
            if (dgvBangDiem.Columns.Contains("MaHS1"))
            {
                dgvBangDiem.Columns.Remove("MaHS1");
            }
            if (dgvBangDiem.Columns.Contains("Diem15Phut"))
            {
                dgvBangDiem.Columns.Remove("Diem15Phut");
            }
            if (dgvBangDiem.Columns.Contains("Diem1Tiet"))
            {
                dgvBangDiem.Columns.Remove("Diem1Tiet");
            }
            if (dgvBangDiem.Columns.Contains("DiemThi"))
            {
                dgvBangDiem.Columns.Remove("DiemThi");
            }
            if (dgvBangDiem.Columns.Contains("DiemTB"))
            {
                dgvBangDiem.Columns.Remove("DiemTB");
            }
            if (dgvBangDiem.Columns.Contains("HoTen"))
            {
                dgvBangDiem.Columns.Remove("HoTen");
            }
            if (dgvBangDiem.Columns.Contains("MaMH"))
            {
                dgvBangDiem.Columns.Remove("MaMH");
            }
            if (dgvBangDiem.Columns.Contains("DiemMieng"))
            {
                dgvBangDiem.Columns.Remove("DiemMieng");
            }

        }
        //Hàm tạo cột

        private void createColumns()
        {
            dgvBangDiem.ColumnCount =9;
            dgvBangDiem.Columns[0].Name = "Mã học sinh";
            dgvBangDiem.Columns[0].DataPropertyName = "MaHS";
            dgvBangDiem.Columns[1].Name = "Họ tên";
            dgvBangDiem.Columns[1].DataPropertyName = "HoTen";
            dgvBangDiem.Columns[2].Name = "Mã môn học";
            dgvBangDiem.Columns[2].DataPropertyName = "MaMH";
            dgvBangDiem.Columns[3].Name = "Mã HS1";
            dgvBangDiem.Columns[3].DataPropertyName = "MaHS1";
            dgvBangDiem.Columns[4].Name = "Điểm miệng";
            dgvBangDiem.Columns[4].DataPropertyName = "DiemMieng";
            dgvBangDiem.Columns[5].Name = "Điểm 15 phút";
            dgvBangDiem.Columns[5].DataPropertyName = "Diem15Phut";
            dgvBangDiem.Columns[6].Name = "Điểm 1 tiết";
            dgvBangDiem.Columns[6].DataPropertyName = "Diem1Tiet";
            dgvBangDiem.Columns[7].Name = "Điểm thi";
            dgvBangDiem.Columns[7].DataPropertyName = "DiemThi";
            dgvBangDiem.Columns[8].Name = "Điểm trung bình";
            dgvBangDiem.Columns[8].DataPropertyName = "DiemTB";
            dgvBangDiem.Columns[3].Visible = false;
            dgvBangDiem.Columns[2].Visible = false;
            
          /*  dgvBangDiem.ColumnCount = 8;
            dgvBangDiem.Columns[0].Name = "Mã học sinh";
            dgvBangDiem.Columns[0].DataPropertyName = "MaHS";
            dgvBangDiem.Columns[1].Name = "Họ tên";
            dgvBangDiem.Columns[1].DataPropertyName = "HoTen";
            dgvBangDiem.Columns[2].Name = "Mã môn học";
            dgvBangDiem.Columns[2].DataPropertyName = "MaMH";
            dgvBangDiem.Columns[3].Name = "Điểm miệng";
            dgvBangDiem.Columns[3].DataPropertyName = "DiemMieng";
            dgvBangDiem.Columns[4].Name = "Điểm 15 phút";
            dgvBangDiem.Columns[4].DataPropertyName = "Diem15Phut";
            dgvBangDiem.Columns[5].Name = "Điểm 1 tiết";
            dgvBangDiem.Columns[5].DataPropertyName = "Diem1Tiet";
            dgvBangDiem.Columns[6].Name = "Điểm thi";
            dgvBangDiem.Columns[6].DataPropertyName = "DiemThi";
            dgvBangDiem.Columns[7].Name = "Điểm trung bình";
            dgvBangDiem.Columns[7].DataPropertyName = "DiemTB";
           */
        }

        //Hàm lấy thông tin MaMH
        private int layMaMH()
        {
                SqlDataAdapter daMH;
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn học sinh đã nhập điểm
                string sSelectMaMH = @"Select MaMH From MonHoc Where MaHK=" + cmbHocKy.SelectedValue + "and MaKhoi=" + cmbKhoi.SelectedValue;
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daMH = new SqlDataAdapter(sSelectMaMH, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong datatable
                daMH.Fill(dt);
                return int.Parse(dt.Rows[0][0].ToString());
        }
        //Hàm nhấn nút thoát
            private void btnThoat_Click(object sender, EventArgs e)
            {
                frmMain.pnl.Location = new Point(frmMain.iDoRongFormMainBanDau, 50);
                frmMain.iDoRongFormMain = frmMain.iDoRongFormMainBanDau;
                frmMain.pnl.Size = new Size(0, 0);
            }

            private void dgvBangDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }
            
           //Hàm xóa 1 dòng dữ liệu
            private void btXoa_Click(object sender, EventArgs e)
            {
                if (NutNhan == false)//xóa khi đã nhập điểm đầy đủ
                {
                        try
                        {
                            DataGridViewRow rowSelected = dgvBangDiem.SelectedRows[0];
                            //Chuỗi kết nối 
                            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                            //Khởi tạo đối tượng connection
                            SqlConnection con = new SqlConnection(sChuoiKetNoi);
                            con.Open();
                            string sMaMH =rowSelected.Cells[2].Value.ToString();
                            string sMaHS = rowSelected.Cells[0].Value.ToString();
                            string sDelete = @"Delete From Diem Where MaMH="+sMaMH+"and MaHS="+sMaHS;
                            SqlCommand cm = new SqlCommand(sDelete, con);
                            cm.ExecuteNonQuery();
                           con.Close();
                            dgvBangDiem.Rows.Remove(rowSelected);
                            MessageBox.Show("Xóa điểm thành công!", "Thông báo");


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Xóa điểm thất bại!", "Thông báo");
                        }
                    }
                else
                {
                    MessageBox.Show("Xóa điểm thất bại!", "Thông báo");
                }
            }

            private void grbNhapDiem_Enter(object sender, EventArgs e)
            {

            }
        }
       


}
