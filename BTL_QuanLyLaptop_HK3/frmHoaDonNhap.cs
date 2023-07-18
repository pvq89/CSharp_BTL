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
    public partial class frmHoaDonNhap : Form
    {
        DataHandler dataHandler = new DataHandler();
        public frmHoaDonNhap()
        {
            InitializeComponent();
        }

        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            hienHoaDonNhap();
            hienNhanVienCombobox();
            comboBoxNhanVien.Text = string.Empty;
        }

        private void hienHoaDonNhap()
        {
            dataGridViewHoaDonNhap.DataSource = dataHandler.getData("Select *from viewHoaDonNhap");
            dataGridViewHoaDonNhap.AutoResizeColumns();
            dataGridViewHoaDonNhap.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        private void hienNhanVienCombobox()
        {
            DataTable tbl = dataHandler.getData("Select * from tblNhanVien");
            comboBoxNhanVien.DataSource = tbl;
            comboBoxNhanVien.DisplayMember = "sTen";
            comboBoxNhanVien.ValueMember = "iMaNV";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int maNV = int.Parse(comboBoxNhanVien.SelectedValue.ToString());

            MessageBox.Show("Bạn đã chọn nv : "+comboBoxNhanVien.DisplayMember +","+ maNV);
            try
            {
                //int maNV = int.Parse(comboBoxNhanVien.SelectedValue.ToString());
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@maNV",maNV),
                    new SqlParameter("@nhaCungCap",tbNhaCungCap.Text)
                };
                if (dataHandler.insertUpdateDelete("procThemHoaDonNhap", prm))
                {
                    MessageBox.Show("Thêm thành công ! ", "Thông báo");
                    hienHoaDonNhap();
                }
                else
                    MessageBox.Show("Không thể thêm ! ", "Thông báo");
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message, "Thông báo");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int maHD = int.Parse(dataGridViewHoaDonNhap.CurrentRow.Cells["iMaHD"].Value.ToString());
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@nhaCungCap",tbNhaCungCap.Text),
                    new SqlParameter("@maHD",maHD)
                };
                if (dataHandler.insertUpdateDelete("procSuaHoaDonNhap", prm))
                {
                    MessageBox.Show("Sửa thành công ", "Thông báo");
                    hienHoaDonNhap();
                }
                else
                    MessageBox.Show("Không thể sửa  ", "Thông báo");
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message, "Thông báo");
            }
        }

        private void dataGridViewHoaDonNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBoxNhanVien.Text = dataGridViewHoaDonNhap.CurrentRow.Cells["sTen"].Value.ToString();
            tbNhaCungCap.Text = dataGridViewHoaDonNhap.CurrentRow.Cells["sNhaCungCap"].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Xác nhận xoá ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == result)
                {
                    using (SqlConnection cnn = dataHandler.getConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand("Delete tblHoaDonNhap where iMaHD = @maHD", cnn))
                        {
                            cnn.Open();
                            int maHD = int.Parse(dataGridViewHoaDonNhap.CurrentRow.Cells["iMaHD"].Value.ToString());
                            cmd.Parameters.AddWithValue("@maHD", maHD);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Xoá thành công ", "Thông báo");
                                hienHoaDonNhap();
                            }
                            else
                                MessageBox.Show("Ko thể xoá ", "Thông báo");

                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message, "Thông báo");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            comboBoxNhanVien.Text = tbNhaCungCap.Text = string.Empty;
            hienHoaDonNhap();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tìm kiếm theo nhà cung cấp !", "Chú ý");
            string filter = "iMaHD>0";
            if (!string.IsNullOrEmpty(tbNhaCungCap.Text))
                filter += string.Format(" and sNhaCungCap like '%{0}%'", tbNhaCungCap.Text.Trim());
            DataTable tbl = dataHandler.getData("Select *from tblHoaDonNhap");
            DataView view = new DataView(tbl);
            view.RowFilter = filter;
            dataGridViewHoaDonNhap.DataSource = view;
            if (view.Count == 0)
                MessageBox.Show("Ko tìm thấy !", "Thông báo");

        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            int maHD = int.Parse(dataGridViewHoaDonNhap.CurrentRow.Cells["iMaHD"].Value.ToString());
            frmChiTietHoaDonNhap frm = new frmChiTietHoaDonNhap(maHD);
            frm.MdiParent = this.MdiParent;
            frm.Show();
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            int maHD = int.Parse(dataGridViewHoaDonNhap.CurrentRow.Cells["iMaHD"].Value.ToString());
            frmRptHoaDonBan frm = new frmRptHoaDonBan(0,maHD);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }





        //private void btnThem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string query = "INSERT INTO tblHoaDonNhap (iMaNV) VALUES (@maNV)";
        //        int selectedIndex = comboBoxNhanVien.SelectedIndex;

        //        if (selectedIndex != -1)
        //        {
        //            DataRowView selectedRow = (DataRowView)comboBoxNhanVien.Items[selectedIndex];
        //            string maNV = selectedRow["iMaNV"].ToString();

        //            using (SqlConnection cnn = dataHandler.getConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand(query, cnn))
        //                {
        //                    cnn.Open();
        //                    cmd.Parameters.AddWithValue("@maNV", maNV);

        //                    if (cmd.ExecuteNonQuery() > 0)
        //                    {
        //                        MessageBox.Show("Thêm thành công!", "Thông báo");
        //                        hienHoaDonNhap();
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Không thể thêm!", "Thông báo");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show("Lỗi: " + error.Message, "Thông báo");
        //    }
        //}
    }
}
