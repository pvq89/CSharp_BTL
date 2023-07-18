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
    public partial class frmNhanVien : Form
    {
        DataHandler dataHandler = new DataHandler();
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = false;
            hienNhanVien();
        }

        //Hien du lieu theo filter
        private void hienNhanVien(string filter = "")
        {
            DataTable tbl = dataHandler.getData("Select *from viewNhanVien");
            DataView view = new DataView(tbl);
            //neu co dieu kien loc
            if (!string.IsNullOrEmpty(filter))
            {
                view.RowFilter = filter;
            }
            dataGridViewNhanVien.DataSource = view;
            dataGridViewNhanVien.AutoResizeColumns();
            dataGridViewNhanVien.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        public bool isDataEntered()
        {
            //TextBox[] listTextBox = new TextBox[] { tbTen, tbDiaChi, tbLuong, tbSDT };
            TextBox[] listTextBox = { tbTen, tbDiaChi, tbLuong, tbSDT };
            foreach (TextBox tb in listTextBox)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    //substring(n) : cắt chuỗi từ ký tự thứ n 
                    string tbName = tb.Name.Substring(2);
                    MessageBox.Show($"Bạn chưa nhập {tbName} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tb.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(tbNgaySinh.Text))
            {
                MessageBox.Show($"Bạn chưa nhập ngày sinh !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbNgaySinh.Focus();
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (isDataEntered())
            {
                try
                {
                    //tạo 1 arr chưa tham số
                    SqlParameter[] prm = new SqlParameter[]
                    {
                        new SqlParameter("@tenNV",tbTen.Text),
                        new SqlParameter("@sdt",tbSDT.Text),
                        new SqlParameter("@diaChi",tbDiaChi.Text),
                        new SqlParameter("@luong",tbLuong.Text),
                        new SqlParameter("@ngaySinh",DateTime.Parse(tbNgaySinh.Text))
                    };

                    if (dataHandler.insertUpdateDelete("procThemNV", prm))
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        hienNhanVien();
                        tbLuong.Text = tbDiaChi.Text = tbSDT.Text = tbTen.Text = string.Empty;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Lỗi : " + error.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] listPrm = new SqlParameter[]
                {
                    new SqlParameter("@maNV",tbMaNV.Text),
                    new SqlParameter("@tenNV",tbTen.Text),
                    new SqlParameter("@sdt",tbSDT.Text),
                    new SqlParameter("@diaChi",tbDiaChi.Text),
                    new SqlParameter("@luong",tbLuong.Text),
                    new SqlParameter("@ngaySinh",tbNgaySinh.Text)
                };
                if (dataHandler.insertUpdateDelete("procCapNhatNV", listPrm))
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hienNhanVien();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = btnBoqua.Enabled = true;
            tbMaNV.Text = dataGridViewNhanVien.CurrentRow.Cells[0].Value.ToString();
            tbTen.Text = dataGridViewNhanVien.CurrentRow.Cells[1].Value.ToString();
            tbSDT.Text = dataGridViewNhanVien.CurrentRow.Cells[2].Value.ToString();
            tbDiaChi.Text = dataGridViewNhanVien.CurrentRow.Cells[3].Value.ToString();
            tbLuong.Text = dataGridViewNhanVien.CurrentRow.Cells[4].Value.ToString();
            tbNgaySinh.Text = dataGridViewNhanVien.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            tbMaNV.Text = tbTen.Text = tbSDT.Text = tbLuong.Text = tbDiaChi.Text = tbNgaySinh.Text = string.Empty;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xoá ?", "Thông báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // SqlParameter[] prm = new SqlParameter[]
                //{
                //     new SqlParameter("@maNV", tbMaNV.Text)
                //};
                try
                {
                    if (dataHandler.delete("procXoaNV", "@maNV", tbMaNV.Text))
                    {
                        MessageBox.Show(" Xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        hienNhanVien();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Lỗi : " + error.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbDiaChi.Text) && string.IsNullOrEmpty(tbTen.Text))
                MessageBox.Show("Tìm kiếm theo tên, địa chỉ nhé !", "Lưu ý");


            //vì dùng view đổ lên datagridview nên phải để tên cột đúng với view
            string filter = "[Mã NV]>0";
            if (!string.IsNullOrEmpty(tbTen.Text))
                filter += string.Format("and [Họ tên] like '%{0}%' ", tbTen.Text);
            if (!string.IsNullOrEmpty(tbDiaChi.Text))
                filter += string.Format("and [Địa chỉ] like '%{0}%' ", tbDiaChi.Text);
            hienNhanVien(filter);
        }

        private void tbLuong_Validating(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(tbLuong.Text, out float luong))
            {
                errorProvider.SetError(tbLuong, "Lương phải là số");
            }
            else
            {
                errorProvider.SetError(tbLuong, "");
            }

        }

        private void tbSDT_Validating(object sender, CancelEventArgs e)
        {
            string query = "Select count(*) from tblNhanVien where sSDT=@SDT";
            using (SqlConnection cnn = dataHandler.getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cnn.Open();
                    cmd.Parameters.AddWithValue("@SDT", tbSDT.Text);
                    //ExecuteScalar() : 
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                        errorProvider.SetError(tbSDT, "So dien thoai da co");
                    else
                        errorProvider.SetError(tbSDT, "");
                }

            }
        }

    }
}


