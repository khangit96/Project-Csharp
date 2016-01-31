namespace QuanLyHocSinh
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblTitle = new System.Windows.Forms.Label();
            this.tmNhapDiem = new System.Windows.Forms.Timer(this.components);
            this.btnQuiDinh = new System.Windows.Forms.Button();
            this.btnPhanLop = new System.Windows.Forms.Button();
            this.btnLopHoc = new System.Windows.Forms.Button();
            this.btnGiaoVien = new System.Windows.Forms.Button();
            this.btnNamHoc = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnNhapDiem = new System.Windows.Forms.Button();
            this.btnMonHoc = new System.Windows.Forms.Button();
            this.btnHocSinh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(-3, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(323, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "PHẦN MỀM QUẢN LÝ HỌC SINH";
            // 
            // tmNhapDiem
            // 
            this.tmNhapDiem.Interval = 1;
            this.tmNhapDiem.Tick += new System.EventHandler(this.tmNhapDiem_Tick);
            // 
            // btnQuiDinh
            // 
            this.btnQuiDinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(0)))), ((int)(((byte)(176)))));
            this.btnQuiDinh.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.quidinh;
            this.btnQuiDinh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnQuiDinh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQuiDinh.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuiDinh.ForeColor = System.Drawing.Color.White;
            this.btnQuiDinh.Location = new System.Drawing.Point(771, 234);
            this.btnQuiDinh.Name = "btnQuiDinh";
            this.btnQuiDinh.Size = new System.Drawing.Size(236, 113);
            this.btnQuiDinh.TabIndex = 10;
            this.btnQuiDinh.Text = "Qui định";
            this.btnQuiDinh.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnQuiDinh.UseVisualStyleBackColor = false;
            // 
            // btnPhanLop
            // 
            this.btnPhanLop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(90)))), ((int)(((byte)(188)))));
            this.btnPhanLop.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.PhanLop;
            this.btnPhanLop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPhanLop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPhanLop.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhanLop.ForeColor = System.Drawing.Color.White;
            this.btnPhanLop.Location = new System.Drawing.Point(596, 353);
            this.btnPhanLop.Name = "btnPhanLop";
            this.btnPhanLop.Size = new System.Drawing.Size(411, 113);
            this.btnPhanLop.TabIndex = 9;
            this.btnPhanLop.Text = "Phân lớp";
            this.btnPhanLop.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnPhanLop.UseVisualStyleBackColor = false;
            // 
            // btnLopHoc
            // 
            this.btnLopHoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.btnLopHoc.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.lophoc;
            this.btnLopHoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLopHoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLopHoc.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLopHoc.ForeColor = System.Drawing.Color.White;
            this.btnLopHoc.Location = new System.Drawing.Point(771, 95);
            this.btnLopHoc.Name = "btnLopHoc";
            this.btnLopHoc.Size = new System.Drawing.Size(236, 133);
            this.btnLopHoc.TabIndex = 8;
            this.btnLopHoc.Text = "Lớp học";
            this.btnLopHoc.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnLopHoc.UseVisualStyleBackColor = false;
            this.btnLopHoc.Click += new System.EventHandler(this.btnLopHoc_Click);
            // 
            // btnGiaoVien
            // 
            this.btnGiaoVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(26)))), ((int)(((byte)(63)))));
            this.btnGiaoVien.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.GiaoVien;
            this.btnGiaoVien.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGiaoVien.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGiaoVien.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiaoVien.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGiaoVien.Location = new System.Drawing.Point(596, 95);
            this.btnGiaoVien.Name = "btnGiaoVien";
            this.btnGiaoVien.Size = new System.Drawing.Size(169, 252);
            this.btnGiaoVien.TabIndex = 7;
            this.btnGiaoVien.Text = "Giáo viên";
            this.btnGiaoVien.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnGiaoVien.UseVisualStyleBackColor = false;
            // 
            // btnNamHoc
            // 
            this.btnNamHoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(0)))));
            this.btnNamHoc.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.NienKhoa;
            this.btnNamHoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNamHoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNamHoc.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNamHoc.ForeColor = System.Drawing.Color.White;
            this.btnNamHoc.Location = new System.Drawing.Point(345, 353);
            this.btnNamHoc.Name = "btnNamHoc";
            this.btnNamHoc.Size = new System.Drawing.Size(169, 110);
            this.btnNamHoc.TabIndex = 6;
            this.btnNamHoc.Text = "Niên khóa";
            this.btnNamHoc.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnNamHoc.UseVisualStyleBackColor = false;
            this.btnNamHoc.Click += new System.EventHandler(this.btnNamHoc_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(68)))), ((int)(((byte)(30)))));
            this.btnThongKe.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.ThongKe;
            this.btnThongKe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.Location = new System.Drawing.Point(162, 353);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(177, 110);
            this.btnThongKe.TabIndex = 5;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnNhapDiem
            // 
            this.btnNhapDiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(1)))), ((int)(((byte)(168)))));
            this.btnNhapDiem.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.NhapDiem1;
            this.btnNhapDiem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNhapDiem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNhapDiem.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhapDiem.ForeColor = System.Drawing.Color.White;
            this.btnNhapDiem.Location = new System.Drawing.Point(345, 234);
            this.btnNhapDiem.Name = "btnNhapDiem";
            this.btnNhapDiem.Size = new System.Drawing.Size(169, 113);
            this.btnNhapDiem.TabIndex = 4;
            this.btnNhapDiem.Text = "Nhập điểm";
            this.btnNhapDiem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnNhapDiem.UseVisualStyleBackColor = false;
            this.btnNhapDiem.Click += new System.EventHandler(this.btnNhapDiem_Click);
            // 
            // btnMonHoc
            // 
            this.btnMonHoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(117)))), ((int)(((byte)(230)))));
            this.btnMonHoc.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.MonHoc1;
            this.btnMonHoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMonHoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMonHoc.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonHoc.ForeColor = System.Drawing.Color.White;
            this.btnMonHoc.Location = new System.Drawing.Point(162, 234);
            this.btnMonHoc.Name = "btnMonHoc";
            this.btnMonHoc.Size = new System.Drawing.Size(177, 113);
            this.btnMonHoc.TabIndex = 3;
            this.btnMonHoc.Text = "Môn học";
            this.btnMonHoc.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnMonHoc.UseVisualStyleBackColor = false;
            // 
            // btnHocSinh
            // 
            this.btnHocSinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(54)))), ((int)(((byte)(177)))));
            this.btnHocSinh.BackgroundImage = global::QuanLyHocSinh.Properties.Resources.HocSinh;
            this.btnHocSinh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnHocSinh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHocSinh.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHocSinh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHocSinh.Location = new System.Drawing.Point(162, 94);
            this.btnHocSinh.Name = "btnHocSinh";
            this.btnHocSinh.Size = new System.Drawing.Size(352, 134);
            this.btnHocSinh.TabIndex = 0;
            this.btnHocSinh.Text = "Học sinh";
            this.btnHocSinh.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnHocSinh.UseVisualStyleBackColor = false;
            this.btnHocSinh.Click += new System.EventHandler(this.btnHocSinh_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(0)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(1109, 612);
            this.Controls.Add(this.btnQuiDinh);
            this.Controls.Add(this.btnPhanLop);
            this.Controls.Add(this.btnLopHoc);
            this.Controls.Add(this.btnGiaoVien);
            this.Controls.Add(this.btnNamHoc);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnNhapDiem);
            this.Controls.Add(this.btnMonHoc);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnHocSinh);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHocSinh;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnMonHoc;
        private System.Windows.Forms.Button btnNhapDiem;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnNamHoc;
        private System.Windows.Forms.Button btnGiaoVien;
        private System.Windows.Forms.Button btnLopHoc;
        private System.Windows.Forms.Button btnPhanLop;
        private System.Windows.Forms.Button btnQuiDinh;
        private System.Windows.Forms.Timer tmNhapDiem;

    }
}