using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyLaptop_HK3
{
    public partial class frmChiTietHoaDonNhap : Form
    {
        DataHandler dataHandler = new DataHandler();
        private int maHD = 0;
        public frmChiTietHoaDonNhap(int maHD)
        {
            this.maHD = maHD;
            InitializeComponent();
        }

        private void frmChiTietHoaDonNhap_Load(object sender, EventArgs e)
        {
            checkMaHD();

            hienCTHoaDonNhap(this.maHD);
            hienLoaiHang();
        }
        private void checkMaHD()
        {
            if(maHD == 0)
            {
                lblHoaDon.Text = "Danh sách hoá đơn nhập";
                btnThem.Enabled = btnSua.Enabled = btnDelete.Enabled = false;
            }
            else
                lblHoaDon.Text += maHD;

        }

        //Hiện hoá đơn theo mã đã chọn bên fromHoaDonNhap
        private void hienCTHoaDonNhap(int maHD)
        {
            //Vì tham số getDataProc là string
            string ma = maHD.ToString();
            dataGridViewCTHoaDonNhap.DataSource = dataHandler.getDataProc("procCTHoaDonNhap","@maHD",ma);
            dataGridViewCTHoaDonNhap.AutoResizeColumns();
            dataGridViewCTHoaDonNhap.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        //Hiện loại hàng
        private void hienLoaiHang()
        {
            comboBoxLoaihang.DisplayMember = "sTenHang";
            comboBoxLoaihang.ValueMember = "iMaLH";
            comboBoxLoaihang.DataSource = dataHandler.getData("Select *from tblLoaiHang");
        }

        //Hiện mặt hàng tương ứng với loại hàng đã chọn
        private void hienMatHang(string maLH)
        {
            comboBoxMatHang.DisplayMember = "sTenHH";
            comboBoxMatHang.ValueMember = "iMaMH";
            comboBoxMatHang.DataSource = dataHandler.getDataProc("procHienMatHangCuaLoaiHang","@maLH",maLH);
        }

        private void comboBoxLoaihang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maLH = comboBoxLoaihang.SelectedValue.ToString();
            hienMatHang(maLH);
        }

        private void comboBoxMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string maMH = comboBoxMatHang.SelectedValue.ToString();
                using (SqlConnection cnn = dataHandler.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("procSoLuongMatHang", cnn))
                    {
                        cnn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@maMH", maMH);
                        //c1
                        //SqlDataReader rd = cmd.ExecuteReader();
                        //while (rd.Read())
                        //{
                        //    tbTonKho.Text = rd["iSoLuong"].ToString();
                        //}
                        //rd.Close();
                        //c2
                        int soLuong = (int)cmd.ExecuteScalar();
                        tbTonKho.Text = soLuong.ToString();
                        cnn.Close();
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show("Lỗi. có thể số lượng trong database null, "+err.Message);
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@maHD",maHD),
                    new SqlParameter("@soLuong",tbSoLuong.Text),
                    new SqlParameter("@donGia",tbDonGia.Text),
                    new SqlParameter("@maMH",comboBoxMatHang.SelectedValue.ToString()),
                };
                if (dataHandler.insertUpdateDelete("procThemCTHoaDonNhap", prm))
                {
                    MessageBox.Show("Thêm thành công !");
                    hienCTHoaDonNhap(maHD);
                }
                else
                    MessageBox.Show("Ko thể thêm !");
            }
            catch(Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chỉ sửa được số lượng và đơn giá !", "Lưu ý");
            try
            {
                int maMH = int.Parse(dataGridViewCTHoaDonNhap.CurrentRow.Cells["iMaMH"].Value.ToString());
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@maHD",maHD),
                    new SqlParameter("@soLuong",tbSoLuong.Text),
                    new SqlParameter("@donGia",tbDonGia.Text),
                    new SqlParameter("@maMH",maMH)
                };
                if (dataHandler.insertUpdateDelete("procSuaCTHoaDonNhap", prm))
                {
                    MessageBox.Show("Sửa thành công !");
                    hienCTHoaDonNhap(maHD);
                }
                else
                    MessageBox.Show("Ko thể sửa !");
                tbSoLuong.Text = tbDonGia.Text = string.Empty;
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message);
            }
        }

        private void dataGridViewCTHoaDonNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBoxMatHang.Text= dataGridViewCTHoaDonNhap.CurrentRow.Cells["sTenHH"].Value.ToString();
            comboBoxLoaihang.Text= dataGridViewCTHoaDonNhap.CurrentRow.Cells["sTenHang"].Value.ToString();
            tbSoLuong.Text = dataGridViewCTHoaDonNhap.CurrentRow.Cells["iSoLuong"].Value.ToString();
            tbDonGia.Text= dataGridViewCTHoaDonNhap.CurrentRow.Cells["fDonGia"].Value.ToString();
            comboBoxLoaihang.Enabled = comboBoxMatHang.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            tbDonGia.Text = tbSoLuong.Text = string.Empty;
            comboBoxMatHang.Enabled = comboBoxLoaihang.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhân xoá ?", "Thông báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (DialogResult.Cancel == result)
                return;
            try
            {
                int maMH = int.Parse(dataGridViewCTHoaDonNhap.CurrentRow.Cells["iMaMH"].Value.ToString());
                int maHD = int.Parse(dataGridViewCTHoaDonNhap.CurrentRow.Cells["iMaHD"].Value.ToString());

                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@maMH",maMH),
                    new SqlParameter("@maHD",maHD)
                };
                if (dataHandler.insertUpdateDelete("procXoaCTHoaDonNhap", prm))
                {
                    MessageBox.Show("Xoá thành công ! ");
                    hienCTHoaDonNhap(maHD);
                }
                else
                    MessageBox.Show("Ko thể xoá !  ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Loi : " + error.Message);
            }
        }

        private void frmChiTietHoaDonNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmHoaDonNhap frm = new frmHoaDonNhap();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}
