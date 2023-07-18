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
    public partial class frmLoaiHang : Form
    {
        DataHandler dataHandler = new DataHandler();
        public frmLoaiHang()
        {
            InitializeComponent();
        }


        private void hienLoaiHang()
        {
            dataGridViewLoaiHang.DataSource = dataHandler.getData("Select *from tblLoaiHang");
            //dataGridViewLoaiHang.AutoResizeColumns();
            dataGridViewLoaiHang.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }
        private void frmLoaiHang_Load(object sender, EventArgs e)
        {
            hienLoaiHang();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Xác nhận xoá ? ","Thông báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                using(SqlConnection cnn = dataHandler.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("Delete tblLoaiHang where iMaLH=@maLH",cnn))
                    {
                        cnn.Open();
                        string maLH = dataGridViewLoaiHang.CurrentRow.Cells["iMaLH"].Value.ToString();
                        cmd.Parameters.AddWithValue("@maLH", maLH);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Thành công !", "Thông báo");
                            hienLoaiHang();
                        }
                        else
                            MessageBox.Show("Không thành công !", "Thông báo");
                        cnn.Close();
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Text = "Cập nhật";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string query = string.Empty;
                if (btnThem.Text.Equals("Thêm"))
                    query = "Insert into tblLoaiHang (sTenHang) values (@tenHang)";
                else
                    //Nếu =="Cập nhật"
                    query = "Update tblLoaiHang set sTenHang = @tenHang where iMaLH=@maLH";

                using (SqlConnection cnn = dataHandler.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, cnn))
                    {
                        cnn.Open();
                        cmd.Parameters.AddWithValue("@tenHang", tbLoaiHang.Text);
                        if (btnThem.Text.Equals("Cập nhật"))
                        {
                            string maLH = dataGridViewLoaiHang.CurrentRow.Cells["iMaLH"].Value.ToString();
                            cmd.Parameters.AddWithValue("@maLH", maLH);
                        }
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Thành công !", "Thông báo");
                            hienLoaiHang();
                        }
                        else
                            MessageBox.Show("Không thành công !", "Thông báo");
                        tbLoaiHang.Text = string.Empty;
                        btnThem.Text = "Thêm";
                        cnn.Close();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Lỗi : " + error.Message, "Thông báo");
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            tbLoaiHang.Text = "";
        }

        private void dataGridViewLoaiHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbLoaiHang.Text = dataGridViewLoaiHang.CurrentRow.Cells["iMaLH"].Value.ToString();
            tbLoaiHang.Text = dataGridViewLoaiHang.CurrentRow.Cells["sTenHang"].Value.ToString();
            btnThem.Text = "Cập nhật";
        }
    }
}
