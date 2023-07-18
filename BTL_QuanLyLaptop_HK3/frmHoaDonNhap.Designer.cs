
namespace BTL_QuanLyLaptop_HK3
{
    partial class frmHoaDonNhap
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
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.labelNvNhap = new System.Windows.Forms.Label();
            this.dataGridViewHoaDonNhap = new System.Windows.Forms.DataGridView();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.tbNhaCungCap = new System.Windows.Forms.TextBox();
            this.comboBoxNhanVien = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHoaDonNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.AutoSize = true;
            this.btnTimKiem.Location = new System.Drawing.Point(639, 120);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 30);
            this.btnTimKiem.TabIndex = 25;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(351, 120);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(168, 30);
            this.btnXem.TabIndex = 23;
            this.btnXem.Text = "Chi tiết hoá đơn nhập";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnReport
            // 
            this.btnReport.AutoSize = true;
            this.btnReport.Location = new System.Drawing.Point(783, 120);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(100, 30);
            this.btnReport.TabIndex = 22;
            this.btnReport.Text = "Báo cáo";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(783, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Location = new System.Drawing.Point(639, 66);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSua
            // 
            this.btnSua.AutoSize = true;
            this.btnSua.Location = new System.Drawing.Point(783, 15);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 30);
            this.btnSua.TabIndex = 19;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.AutoSize = true;
            this.btnThem.Location = new System.Drawing.Point(639, 11);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 30);
            this.btnThem.TabIndex = 18;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // labelNvNhap
            // 
            this.labelNvNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNvNhap.AutoSize = true;
            this.labelNvNhap.Location = new System.Drawing.Point(200, 24);
            this.labelNvNhap.Name = "labelNvNhap";
            this.labelNvNhap.Size = new System.Drawing.Size(108, 17);
            this.labelNvNhap.TabIndex = 14;
            this.labelNvNhap.Text = "Nhân viên nhập";
            // 
            // dataGridViewHoaDonNhap
            // 
            this.dataGridViewHoaDonNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHoaDonNhap.Location = new System.Drawing.Point(10, 183);
            this.dataGridViewHoaDonNhap.Name = "dataGridViewHoaDonNhap";
            this.dataGridViewHoaDonNhap.RowHeadersWidth = 51;
            this.dataGridViewHoaDonNhap.RowTemplate.Height = 24;
            this.dataGridViewHoaDonNhap.Size = new System.Drawing.Size(1061, 262);
            this.dataGridViewHoaDonNhap.TabIndex = 26;
            this.dataGridViewHoaDonNhap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHoaDonNhap_CellContentClick);
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Location = new System.Drawing.Point(200, 66);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(96, 17);
            this.lblNgayTao.TabIndex = 28;
            this.lblNgayTao.Text = "Nhà cung cấp";
            // 
            // tbNhaCungCap
            // 
            this.tbNhaCungCap.Location = new System.Drawing.Point(324, 66);
            this.tbNhaCungCap.Name = "tbNhaCungCap";
            this.tbNhaCungCap.Size = new System.Drawing.Size(226, 22);
            this.tbNhaCungCap.TabIndex = 29;
            // 
            // comboBoxNhanVien
            // 
            this.comboBoxNhanVien.FormattingEnabled = true;
            this.comboBoxNhanVien.Location = new System.Drawing.Point(324, 19);
            this.comboBoxNhanVien.Name = "comboBoxNhanVien";
            this.comboBoxNhanVien.Size = new System.Drawing.Size(226, 24);
            this.comboBoxNhanVien.TabIndex = 30;
            // 
            // frmHoaDonNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 450);
            this.Controls.Add(this.comboBoxNhanVien);
            this.Controls.Add(this.tbNhaCungCap);
            this.Controls.Add(this.lblNgayTao);
            this.Controls.Add(this.dataGridViewHoaDonNhap);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.labelNvNhap);
            this.Name = "frmHoaDonNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoá đơn nhập";
            this.Load += new System.EventHandler(this.frmHoaDonNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHoaDonNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label labelNvNhap;
        private System.Windows.Forms.DataGridView dataGridViewHoaDonNhap;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.TextBox tbNhaCungCap;
        private System.Windows.Forms.ComboBox comboBoxNhanVien;
    }
}