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
    public partial class frmMatHang : Form
    {
        DataHandler dataHandler = new DataHandler();

        public frmMatHang()
        {
            InitializeComponent();
        }
        private bool comboBoxLoaiHangValueChange = false;
        private void frmMatHang_Load(object sender, EventArgs e)
        {
            hienMatHang();
            hienLoaiHang();
            comboBoxLoaiHangValueChange = true;
            comboBoxLoaiHang.Text = String.Empty;
            btnSua.Enabled = btnXoa.Enabled = false;
        }

        //clean control
        private void xoaDuLieuTrenControl()
        {
            tbGia.Text = tbSoLuong.Text = comboBoxLoaiHang.Text = tbTenMH.Text = string.Empty;
        }
        private void hienMatHang(string filter = "")
        {
            DataTable tbl = dataHandler.getData("Select*from v_MatHang_LoaiHang");
            DataView view = new DataView(tbl);
            if (!string.IsNullOrEmpty(filter))
            {
                view.RowFilter = filter;
            }
            dataGridViewMatHang.DataSource = view;
            //Tự căn chỉnh trên datagridview
            dataGridViewMatHang.AutoResizeColumns();
            dataGridViewMatHang.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        //hien loai hang len combobox
        private void hienLoaiHang()
        {
            DataTable tbl = dataHandler.getData("Select * from tblLoaiHang");
            comboBoxLoaiHang.DisplayMember = "sTenHang";
            //Dùng để xác định SelectedValue
            comboBoxLoaiHang.ValueMember = "sTenHang";
            comboBoxLoaiHang.DataSource = tbl;
        }

        private void dataGridViewMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbTenMH.Text = dataGridViewMatHang.CurrentRow.Cells["Mặt hàng"].Value.ToString();
            tbGia.Text = dataGridViewMatHang.CurrentRow.Cells["Giá bán"].Value.ToString();
            tbSoLuong.Text = dataGridViewMatHang.CurrentRow.Cells["Số lượng"].Value.ToString();
            comboBoxLoaiHang.Text = dataGridViewMatHang.CurrentRow.Cells["Loại hàng"].Value.ToString();
            btnSua.Enabled = btnXoa.Enabled = true;

        }

        //Thay đổi item trên combobox tự động thay đổi data hiện thị
        private void comboBoxLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLoaiHangValueChange)
            {
                //Ko dùng được selecteditem ????
                if (comboBoxLoaiHang.SelectedValue != null)
                {
                    string loaiHang = comboBoxLoaiHang.SelectedValue.ToString();
                    DataTable tbl = dataHandler.getData($"Select *from v_MatHang_LoaiHang where [Loại hàng] like N'%{loaiHang}%'");
                    dataGridViewMatHang.DataSource = tbl;
                }
            }
        }

        //Khi không focus combobox nữa thì hiện all data
        //private void comboBoxLoaiHang_Leave(object sender, EventArgs e)
        //{
        //    // Kiểm tra nếu ComboBox không được focus
        //    if (!comboBoxLoaiHang.Focused)
        //    {
        //        DataTable tbl = dataHandler.getData("Select * from v_MatHang_LoaiHang");
        //        dataGridViewMatHang.DataSource = tbl;
        //        comboBoxLoaiHang.Text = "";
        //    }
        //}

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@tenLoaiHang",comboBoxLoaiHang.Text),
                    new SqlParameter("@tenHH",tbTenMH.Text),
                    new SqlParameter("@soLuong",tbSoLuong.Text),
                    new SqlParameter("@giaBan",tbGia.Text)
                };
                if (dataHandler.insertUpdateDelete("procThemMatHang", prm))
                {
                    MessageBox.Show("Thêm mặt hàng thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hienMatHang();
                    xoaDuLieuTrenControl();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message, "Thông báo");
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            xoaDuLieuTrenControl();
            hienMatHang();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string maMatHang = dataGridViewMatHang.CurrentRow.Cells["Mã MH"].Value.ToString();
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@tenloaihang",comboBoxLoaiHang.Text),
                    new SqlParameter("@tenHH",tbTenMH.Text),
                    new SqlParameter("@giaban",tbGia.Text),
                    new SqlParameter("@soLuong",tbSoLuong.Text),
                    new SqlParameter("@mahang",maMatHang)
                };
                if (dataHandler.insertUpdateDelete("procSuaMatHang", prm))
                {
                    MessageBox.Show("Sửa thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hienMatHang();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message, "Thông báo");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận xoá ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
                return;
            try
            {
                //Xoá theo mã
                string maMH = dataGridViewMatHang.CurrentRow.Cells["Mã MH"].Value.ToString();
                if (dataHandler.delete("procXoaMatHang", "@maMH", maMH))
                {
                    MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    xoaDuLieuTrenControl();
                    hienMatHang();
                }
            }
            catch (Exception error)
            {
                if (error.Message.Contains("REFERENCE"))
                    MessageBox.Show("Mặt hàng này có dữ liệu liên quan, không xóa được", "Thông báo");
                else
                    MessageBox.Show("Đã có lỗi xảy ra" + error.Message, "Thông báo");
            }

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string filter = "[Mã MH]>0";
            if (!string.IsNullOrEmpty(tbTenMH.Text))
            {
                filter += string.Format(" and [Mặt hàng] like '%{0}%'", tbTenMH.Text);
            }
            if (!string.IsNullOrEmpty(comboBoxLoaiHang.Text))
            {
                filter += string.Format(" and [Loại hàng] like '%{0}%'", comboBoxLoaiHang.Text);
            }
            hienMatHang(filter);
        }
    }

}
