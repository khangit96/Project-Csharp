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
using Microsoft.Reporting.WinForms;

namespace QuanLyHocSinh
{
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
        }
        DataSet ds;
        SqlDataAdapter daNienKhoa;
        SqlDataAdapter daKhoi;

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLyHocSinhDataSet.DataTableTongHop' table. You can move, or remove it, as needed.
            this.DataTableTongHopTableAdapter.Fill(this.QuanLyHocSinhDataSet.DataTableTongHop);
    
           
            

            ds = new DataSet("dsThongKe");

            //Load cmb niên khóa 
            LoadcmbNienKhoa();
            //Load cmb Khối
            LoadcmbKhoi();
            //Load cmbHocKy
            LoadcmbHocKy();
            //Load cmbLop
            LoadcmbLop();

            LoadDSDiemReport();
            //Refresh Report
            this.rpBangDiem.RefreshReport();
        }
        //Load cmb niên khóa
        private void LoadcmbNienKhoa()
        {

            //Chuỗi kết nối 
            string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
            //Chuỗi truy vấn
            string sSelectNienKhoa = @"Select * From NienKhoa";
            //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
            daNienKhoa = new SqlDataAdapter(sSelectNienKhoa, sChuoiKetNoi);
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
        }//Tạo đối tượng DataAdapter load cmbLop dựa vào cmbNienKhoa
        SqlDataAdapter daLop;
        private void cmbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNienKhoa.SelectedValue != null && !(cmbNienKhoa.SelectedValue is DataRowView)
                 && cmbKhoi.SelectedValue != null && !(cmbKhoi.SelectedValue is DataRowView) //Bổ sung load cmb lớp 
                 )
            {
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectLop = @"Select * From Lop where MaNamHoc=" + cmbNienKhoa.SelectedValue + "and MaKhoi=" + cmbKhoi.SelectedValue;//Bổ sung
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daLop = new SqlDataAdapter(sSelectLop, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong dataset
                daLop.Fill(dt);
                cmbLop.DataSource = dt;
                cmbLop.ValueMember = "MaLop";
                cmbLop.DisplayMember = "TenLop";

           
            }
            //Load datasource report theo gia tri cmb
            LoadDSDiemReport();
    
           
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
                //Đổ dữ liệu vào 1 bảng trong dataset
                daMonHoc.Fill(dt);
                cmbMonHoc.DataSource = dt;
                cmbMonHoc.ValueMember = "MaMH";
                cmbMonHoc.DisplayMember = "TenMH";
              
                //Bổ sung cmbLop
                DataTable dtLop = new DataTable();
                daLop = new SqlDataAdapter(sSlectLop, sChuoiKetNoi);
                daLop.Fill(dtLop);
                cmbLop.DataSource = dtLop;
                cmbLop.DisplayMember = "TenLop";
                cmbLop.ValueMember = "MaLop";
            }
            //Load datasource report theo gia tri cmb
            LoadDSDiemReport();
          
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
              
            }
            //Load datasource report theo gia tri cmb
            LoadDSDiemReport();
        }
      

        private void cmbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load datasource report theo gia tri cmb
            LoadDSDiemReport();
         
           
            
        }

        private void cmbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Tạo reportparameter
            ReportParameter rpaMonHoc = new ReportParameter("parameterMonHoc", cmbMonHoc.Text);
            //Set parameter rpaMonHoc
            rpBangDiem.LocalReport.SetParameters(rpaMonHoc);
            //Load datasource report theo gia tri cmb
            LoadDSDiemReport();
        }
        //Load cmbLop 
        private void LoadcmbLop()
        {

            if (cmbKhoi.SelectedValue != null && !(cmbKhoi.SelectedValue is DataRowView)
                && cmbMonHoc.SelectedValue != null && !(cmbMonHoc.SelectedValue is DataRowView)
                && cmbNienKhoa.SelectedValue != null && !(cmbNienKhoa.SelectedValue is DataRowView) //Bổ sung điều kiện để load cmblop
                )
            {
                //Chuỗi kết nối 
                string sChuoiKetNoi = @"Data Source=(local);Initial Catalog=QuanLyHocSinh;Integrated Security=True";
                //Chuỗi truy vấn
                string sSelectMonHoc = @"Select * From MonHoc where MaKhoi=" + cmbKhoi.SelectedValue + " and MaHK =" + cmbMonHoc.SelectedValue;
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
        private void LoadDSDiemReport()
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
                string sSelectDiem = string.Format(@"Select HocSinh.MaHS,HocSinh.HoTen,Diem.* From Diem,HocSinh,Lop,NienKhoa,Khoi,HocKy,MonHoc where HocSinh.MaLop=Lop.MaLop and Lop.MaKhoi=Khoi.MaKhoi and Lop.MaNamHoc=NienKhoa.MaNamHoc and MonHoc.MaHK=HocKy.MaHK and Diem.MaHS=HocSinh.MaHS and NienKhoa.MaNamHoc={0} and Lop.MaLop={1} and Khoi.MaKhoi={2} and MonHoc.MaMH={3} and HocKy.MaHK ={4}", cmbNienKhoa.SelectedValue, cmbLop.SelectedValue, cmbKhoi.SelectedValue, cmbMonHoc.SelectedValue, cmbHocKy.SelectedValue);
                //Khởi tạo đối tượng SQLDataAdapter thực hiện truy vấn lấy dữ liệu từ database về
                daHocSinh = new SqlDataAdapter(sSelectDiem, sChuoiKetNoi);
                DataTable dt = new DataTable();
                //Đổ dữ liệu vào 1 bảng trong datatable
                daHocSinh.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    //Nếu có điểm thì gán datasource
                    DataTableTongHopBindingSource.DataSource = dt;
                    this.rpBangDiem.RefreshReport();
                }
                else
                {
                    DataTableTongHopBindingSource.DataSource = null;
                    this.rpBangDiem.RefreshReport();
                }
            }
        }
       

      
   

        
        
    }
}
