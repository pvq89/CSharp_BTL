using CrystalDecisions.Windows.Forms;
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
    public partial class frmRptMatHangLoaiHang : Form
    {
        DataHandler dataHandler = new DataHandler();
        public frmRptMatHangLoaiHang()
        {
            InitializeComponent();
        }

        private void frmRptMatHangLoaiHang_Load(object sender, EventArgs e)
        {
            hienLenComboBox();
            string query = "Select *from tblMatHang";
            loadCrytal(query);
        }


        private void hienLenComboBox()
        {
            using (SqlConnection cnn = dataHandler.getConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("Select *from tblLoaiHang", cnn))
                {
                    DataTable tbl = new DataTable();
                    adapter.Fill(tbl);
                    comboBox1.ValueMember = "iMaLH";
                    comboBox1.DisplayMember = "iMaLH";
                    comboBox1.DataSource = tbl;
                }
            }
        }
        private void loadCrytal(string query)
        {
            using (SqlConnection cnn = dataHandler.getConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, cnn))
                {
                    //dataset đã tạo
                    DataSetMatHangLoaiHang ds = new DataSetMatHangLoaiHang();
                    DataTable tbl = new DataTable();
                    adapter.Fill(ds, "dataSetMatHangLoaiHang");

                    rptDataSetMatHangLoaiHang rpt = new rptDataSetMatHangLoaiHang();
                    rpt.SetDataSource(ds.Tables[0]);
                    crystalReportViewer1.ReportSource = rpt;
                    crystalReportViewer1.Refresh();
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maLoaiHang = comboBox1.Text;
            //Phải là bảng có các cột giống dataset
            string query = "Select *from tblMatHang where iMaLH = " + maLoaiHang;
            loadCrytal(query);
        }
    }
}
