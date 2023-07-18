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
    public partial class frmHoaDonBan : Form
    {
        DataHandler dataHandler = new DataHandler();
        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            hienHoaDonBan();
            hienNhanVienComboBox();
        }

        private void hienHoaDonBan()
        {
            dataGridViewHoaDonBan.DataSource = dataHandler.getData("Select *from viewHDBan");
            dataGridViewHoaDonBan.AutoResizeColumns();
            dataGridViewHoaDonBan.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        private void hienNhanVienComboBox()
        {
            DataView view = new DataView(dataHandler.getData("Select *from tblNhanVien"));
            view.Sort = "sTen";
            comboBoxNhanVien.DisplayMember = "sTen";
            comboBoxNhanVien.ValueMember = "iMaNV";
            comboBoxNhanVien.DataSource = view;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@maNV",comboBoxNhanVien.SelectedValue.ToString())
                };
                if (dataHandler.insertUpdateDelete("procThemHoaDonBan", prm))
                {
                    MessageBox.Show("Thêm thành công ");
                    hienHoaDonBan();
                }else
                    MessageBox.Show("Không thể thêm ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xoá ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;
            try
            {
                int maHD = int.Parse(dataGridViewHoaDonBan.CurrentRow.Cells["iMaHD"].Value.ToString());
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@iMaHD",maHD)
                };
                if (dataHandler.insertUpdateDelete("prXoaHDban", prm))
                {
                    MessageBox.Show("Xoá thành công ");
                    hienHoaDonBan();
                }
                else
                    MessageBox.Show("Không thể xoá ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int maHD = int.Parse(dataGridViewHoaDonBan.CurrentRow.Cells["iMaHD"].Value.ToString());
                int maNV = int.Parse(comboBoxNhanVien.SelectedValue.ToString());
                SqlParameter[] prm = new SqlParameter[]
                {
                    new SqlParameter("@iMaHD",maHD),
                    new SqlParameter("@iMaNV",maNV)
                };
                if (dataHandler.insertUpdateDelete("procSuaHDban", prm))
                {
                    MessageBox.Show("Sửa thành công ");
                    hienHoaDonBan();
                }
                else
                    MessageBox.Show("Không thể sửa ");
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int max = (int)numericMax.Value;
            int min = (int)numericMin.Value;
            //nếu min > mã thì đảo lại
            if (min > max)
            {
                int tmp = min;
                min = max;
                max = tmp;
            }
            using(SqlConnection cnn = dataHandler.getConnection())
            {
                using(SqlCommand cmd = new SqlCommand("procTimKiemTheoGiaHoaDonBan", cnn))
                {
                    SqlParameter[] prm = new SqlParameter[]
                    {
                        new SqlParameter("@min",min),
                        new SqlParameter("@max",max)
                    };
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(prm);
                    using(SqlDataAdapter adapter= new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adapter.Fill(tbl);
                        dataGridViewHoaDonBan.DataSource = tbl;
                    }
                }

            }
            
            
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            numericMax.Value = numericMax.Value = 0;
            hienHoaDonBan();
        }

        private void dataGridViewHoaDonBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBoxNhanVien.Text = dataGridViewHoaDonBan.CurrentRow.Cells["sNhanVienLap"].Value.ToString();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            int maHD = int.Parse(dataGridViewHoaDonBan.CurrentRow.Cells["iMaHD"].Value.ToString());
            frmChiTietHoaDonBan frm = new frmChiTietHoaDonBan(maHD);
            frm.MdiParent = this.MdiParent;
            frm.Show();
            this.Close();
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            int maHD = int.Parse(dataGridViewHoaDonBan.CurrentRow.Cells["iMaHD"].Value.ToString());
            frmRptHoaDonBan frm = new frmRptHoaDonBan(maHD,0);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}
