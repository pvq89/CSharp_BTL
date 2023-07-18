
namespace BTL_QuanLyLaptop_HK3
{
    partial class frmChiTietHoaDonBan
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
            this.lblHD = new System.Windows.Forms.Label();
            this.tbBaoHanh = new System.Windows.Forms.MaskedTextBox();
            this.tbTonKho = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSoLuong = new System.Windows.Forms.TextBox();
            this.lbsoluong = new System.Windows.Forms.Label();
            this.comboBoxMatHang = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxLoaiHang = new System.Windows.Forms.ComboBox();
            this.lbloaihang = new System.Windows.Forms.Label();
            this.dataGridViewCTHoaDonBan = new System.Windows.Forms.DataGridView();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnBoQua = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCTHoaDonBan)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHD
            // 
            this.lblHD.AutoSize = true;
            this.lblHD.Location = new System.Drawing.Point(441, 19);
            this.lblHD.Name = "lblHD";
            this.lblHD.Size = new System.Drawing.Size(190, 17);
            this.lblHD.TabIndex = 2;
            this.lblHD.Text = "Thêm mặt hàng cho hóa dơn";
            // 
            // tbBaoHanh
            // 
            this.tbBaoHanh.Location = new System.Drawing.Point(244, 99);
            this.tbBaoHanh.Mask = "00/tháng";
            this.tbBaoHanh.Name = "tbBaoHanh";
            this.tbBaoHanh.Size = new System.Drawing.Size(124, 22);
            this.tbBaoHanh.TabIndex = 43;
            this.tbBaoHanh.ValidatingType = typeof(int);
            // 
            // tbTonKho
            // 
            this.tbTonKho.Location = new System.Drawing.Point(834, 99);
            this.tbTonKho.Name = "tbTonKho";
            this.tbTonKho.ReadOnly = true;
            this.tbTonKho.Size = new System.Drawing.Size(105, 22);
            this.tbTonKho.TabIndex = 42;
            this.tbTonKho.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(768, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 41;
            this.label5.Text = "Tồn kho";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 40;
            this.label4.Text = "Bảo hành";
            // 
            // tbSoLuong
            // 
            this.tbSoLuong.Location = new System.Drawing.Point(648, 99);
            this.tbSoLuong.Name = "tbSoLuong";
            this.tbSoLuong.Size = new System.Drawing.Size(106, 22);
            this.tbSoLuong.TabIndex = 37;
            // 
            // lbsoluong
            // 
            this.lbsoluong.AutoSize = true;
            this.lbsoluong.Location = new System.Drawing.Point(545, 104);
            this.lbsoluong.Name = "lbsoluong";
            this.lbsoluong.Size = new System.Drawing.Size(64, 17);
            this.lbsoluong.TabIndex = 36;
            this.lbsoluong.Text = "Số lượng";
            // 
            // comboBoxMatHang
            // 
            this.comboBoxMatHang.FormattingEnabled = true;
            this.comboBoxMatHang.Location = new System.Drawing.Point(648, 60);
            this.comboBoxMatHang.Name = "comboBoxMatHang";
            this.comboBoxMatHang.Size = new System.Drawing.Size(180, 24);
            this.comboBoxMatHang.TabIndex = 35;
            this.comboBoxMatHang.SelectedIndexChanged += new System.EventHandler(this.comboBoxMatHang_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(545, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 38;
            this.label3.Text = "Mặt hàng";
            // 
            // comboBoxLoaiHang
            // 
            this.comboBoxLoaiHang.FormattingEnabled = true;
            this.comboBoxLoaiHang.Location = new System.Drawing.Point(244, 60);
            this.comboBoxLoaiHang.Name = "comboBoxLoaiHang";
            this.comboBoxLoaiHang.Size = new System.Drawing.Size(171, 24);
            this.comboBoxLoaiHang.TabIndex = 39;
            this.comboBoxLoaiHang.SelectedIndexChanged += new System.EventHandler(this.comboBoxLoaiHang_SelectedIndexChanged);
            // 
            // lbloaihang
            // 
            this.lbloaihang.AutoSize = true;
            this.lbloaihang.Location = new System.Drawing.Point(148, 67);
            this.lbloaihang.Name = "lbloaihang";
            this.lbloaihang.Size = new System.Drawing.Size(71, 17);
            this.lbloaihang.TabIndex = 34;
            this.lbloaihang.Text = "Loại hàng";
            // 
            // dataGridViewCTHoaDonBan
            // 
            this.dataGridViewCTHoaDonBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCTHoaDonBan.Location = new System.Drawing.Point(13, 195);
            this.dataGridViewCTHoaDonBan.Name = "dataGridViewCTHoaDonBan";
            this.dataGridViewCTHoaDonBan.RowHeadersWidth = 51;
            this.dataGridViewCTHoaDonBan.RowTemplate.Height = 24;
            this.dataGridViewCTHoaDonBan.Size = new System.Drawing.Size(1061, 243);
            this.dataGridViewCTHoaDonBan.TabIndex = 44;
            this.dataGridViewCTHoaDonBan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCTHoaDonBan_CellContentClick);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(554, 151);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(86, 23);
            this.btnXoa.TabIndex = 47;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(432, 151);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(86, 23);
            this.btnSua.TabIndex = 46;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(318, 151);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(86, 23);
            this.btnThem.TabIndex = 45;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnBoQua
            // 
            this.btnBoQua.Location = new System.Drawing.Point(669, 151);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(86, 23);
            this.btnBoQua.TabIndex = 48;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // frmChiTietHoaDonBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 450);
            this.Controls.Add(this.btnBoQua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dataGridViewCTHoaDonBan);
            this.Controls.Add(this.tbBaoHanh);
            this.Controls.Add(this.tbTonKho);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbSoLuong);
            this.Controls.Add(this.lbsoluong);
            this.Controls.Add(this.comboBoxMatHang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxLoaiHang);
            this.Controls.Add(this.lbloaihang);
            this.Controls.Add(this.lblHD);
            this.Name = "frmChiTietHoaDonBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết hoá đơn bán";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChiTietHoaDonBan_FormClosing);
            this.Load += new System.EventHandler(this.frmChiTietHoaDonBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCTHoaDonBan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHD;
        private System.Windows.Forms.MaskedTextBox tbBaoHanh;
        private System.Windows.Forms.TextBox tbTonKho;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSoLuong;
        private System.Windows.Forms.Label lbsoluong;
        private System.Windows.Forms.ComboBox comboBoxMatHang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxLoaiHang;
        private System.Windows.Forms.Label lbloaihang;
        private System.Windows.Forms.DataGridView dataGridViewCTHoaDonBan;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnBoQua;
    }
}