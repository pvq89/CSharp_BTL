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
    public partial class frmChiTietHoaDonBan : Form
    {
        DataHandler dataHandler = new DataHandler();
        private int maHD = 0;
        public frmChiTietHoaDonBan(int maHD)
        {
            this.maHD = maHD;
            InitializeComponent();
        }

        private void frmChiTietHoaDonBan_Load(object sender, EventArgs e)
        {
            checkMaHD();
            hienCTHoaDonBan();
            hienLoaiHang();
        }

        private void checkMaHD()
        {
            if (maHD == 0)
            {
                lblHD.Text = "Danh sách hoá đơn bán";
                btnSua.Enabled = btnThem.Enabled = btnXoa.Enabled = false;
            }
            else
                lblHD.Text = "Hoá đơn bán có mã " + maHD;
        }

        private void hienCTHoaDonBan()
        {
            DataTable tbl = dataHandler.getDataProc("procHienCTHDBan", "@iMaHD", maHD.ToString());
            dataGridViewCTHoaDonBan.DataSource = tbl;
            dataGridViewCTHoaDonBan.AutoResizeColumns();
            dataGridViewCTHoaDonBan.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        private void hienLoaiHang()
        {
            comboBoxLoaiHang.DisplayMember = "sTenHang";
            comboBoxLoaiHang.ValueMember = "iMaLH";
            comboBoxLoaiHang.DataSource = dataHandler.getData("Select *from tblLoaiHang");
        }

        //Hiện mặt hàng tương ứng với loại hàng đã chọn
        private void hienMatHang(string maLH)
        {
            comboBoxMatHang.DisplayMember = "sTenHH";
            comboBoxMatHang.ValueMember = "iMaMH";
            comboBoxMatHang.DataSource = dataHandler.getDataProc("procHienMatHangCuaLoaiHang", "@maLH", maLH);
        }

        private void frmChiTietHoaDonBan_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmHoaDonBan frm = new frmHoaDonBan();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void comboBoxLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maLH = comboBoxLoaiHang.SelectedValue.ToString();
            hienMatHang(maLH);
        }

        private void comboBoxMatHang_SelectedIndexChanged(object sender, EventArgs e)
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

        private void dataGridViewCTHoaDonBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbBaoHanh.Text = dataGridViewCTHoaDonBan.CurrentRow.Cells["iBaoHanh"].Value.ToString();
            tbSoLuong.Text = dataGridViewCTHoaDonBan.CurrentRow.Cells["iSoLuong"].Value.ToString();
            comboBoxLoaiHang.Text = dataGridViewCTHoaDonBan.CurrentRow.Cells["sTenHang"].Value.ToString();
            tbBaoHanh.Text = dataGridViewCTHoaDonBan.CurrentRow.Cells["iBaoHanh"].Value.ToString();
            //comboBoxMatHang.Enabled = comboBoxLoaiHang.Enabled = false;

        }
        private string convertBaoHanh()
        {
            string strBaoHanh = tbBaoHanh.Text;
            //Chia chuỗi thành mảng 
            string[] baoHanhs = strBaoHanh.Split('/');
            return baoHanhs[0];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                int iMaMH = int.Parse(comboBoxMatHang.SelectedValue.ToString());
                int iSoLuong = int.Parse(tbSoLuong.Text);
                int iTonKho = int.Parse(tbTonKho.Text);
                string sBaoHanh = convertBaoHanh();
                if (iSoLuong > iTonKho)
                {
                    MessageBox.Show("Sản phẩm trong kho hiện không đủ");
                    return;
                }
                SqlParameter[] prm = new SqlParameter[]
                {
                new SqlParameter("iMaHD",maHD),
                new SqlParameter("iMaMH",iMaMH),
                new SqlParameter("iSoLuong",iSoLuong),
                new SqlParameter("iBaoHanh",sBaoHanh)
                };
                if (dataHandler.insertUpdateDelete("procThemCTHDBan", prm))
                {
                    MessageBox.Show("Thành công !");
                    hienCTHoaDonBan();
                }
                else
                    MessageBox.Show("Thất bại !");
            }
            catch(Exception er)
            {
                MessageBox.Show("Lỗi : " + er.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string sTenMHCu = dataGridViewCTHoaDonBan.CurrentRow.Cells["sTenHH"].Value.ToString();
                int iMaMH = int.Parse(comboBoxMatHang.SelectedValue.ToString());
                int iSoLuong = int.Parse(tbSoLuong.Text);
                int iBaoHanh = int.Parse(convertBaoHanh());
                int iTonKho = int.Parse(tbTonKho.Text);
                int iSoLuongBanDau = int.Parse(dataGridViewCTHoaDonBan.CurrentRow.Cells["iSoLuong"].Value.ToString());
                if (iSoLuong > iTonKho + iSoLuongBanDau)
                {
                    MessageBox.Show("Số lượng trong kho không đủ");
                    return;
                }
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@iMaHD", this.maHD),
                    new SqlParameter("@iMaMH", iMaMH),
                    new SqlParameter("@iSoluong", iSoLuong),
                    new SqlParameter("@iBaohanh", iBaoHanh),
                    new SqlParameter("@sTenMHCu",sTenMHCu)
                };
                if (dataHandler.insertUpdateDelete("procSuaCTHDBan", prm))
                {
                    MessageBox.Show("Thành công");
                    hienCTHoaDonBan();
                }
                else
                    MessageBox.Show("Thất bại");
            }
            catch(Exception err)
            {
                MessageBox.Show("Lỗi : " + err.Message);
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int maMH = int.Parse(dataGridViewCTHoaDonBan.CurrentRow.Cells["iMaMH"].Value.ToString());
                int maHD = int.Parse(dataGridViewCTHoaDonBan.CurrentRow.Cells["iMaHD"].Value.ToString());
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@maMH",maMH),
                    new SqlParameter("@maHD",maHD)
                };
                if (dataHandler.insertUpdateDelete("procXoaCTHDBan", prm))
                {
                    MessageBox.Show("Thành công");
                    hienCTHoaDonBan();
                }else
                    MessageBox.Show("Thất bại");
            }
            catch (Exception er)
            {
                MessageBox.Show("Lỗi : " + er.Message);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            //comboBoxLoaiHang.Enabled = comboBoxMatHang.Enabled = true;
            tbBaoHanh.Text = tbSoLuong.Text = "";
        }
    }
}
