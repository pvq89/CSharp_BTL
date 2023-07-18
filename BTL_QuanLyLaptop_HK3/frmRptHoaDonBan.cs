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
    public partial class frmRptHoaDonBan : Form
    {
        int maHDBan, maHDNhap;

        public frmRptHoaDonBan(int maHDBan, int maHDNhap)
        {
            this.maHDBan = maHDBan;
            this.maHDNhap = maHDNhap;
            InitializeComponent();
        }

        private void frmRptHoaDon_Load(object sender, EventArgs e)
        {
            int maHD;
            String nameProc;
            DataHandler dataHandler = new DataHandler();

            if (maHDBan == 0)
            {
                nameProc = "rptHoaDonNhap";
                maHD = maHDNhap;
                rptHoaDonNhap rpt = new rptHoaDonNhap();

                DataTable tbl = dataHandler.getDataProc(nameProc, "@iMaHD", maHD.ToString());
                //thông báo nếu ko có hoá đơn
                //if (tbl.Rows.Count == 0)
                //{
                //    MessageBox.Show("Không có dữ liệu");
                //    return;
                //}
                rpt.SetDataSource(tbl);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            else
            {
                nameProc = "rptHoaDonBan";
                maHD = maHDBan;
                rptHoaDonBan rpt = new rptHoaDonBan();

                DataTable tbl = dataHandler.getDataProc(nameProc, "@iMaHD", maHD.ToString());
                //if (tbl.Rows.Count == 0)
                //{
                //    MessageBox.Show("Không có dữ liệu");
                //    return;
                //}
                rpt.SetDataSource(tbl);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }



            //code gốc hiện rpt bằng proc
            //using(SqlConnection cnn = dataHandler.getConnection())
            //{
            //    using(SqlCommand cmd = new SqlCommand("rptHoaDonBan", cnn))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@iMaHD", maHD);
            //        using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            //        {
            //            DataTable tbl = new DataTable();
            //            adapter.Fill(tbl);

            //            //gán dữ liệu cho rpt
            //            rptHoaDonBan rpt = new rptHoaDonBan();
            //            rpt.SetDataSource(tbl);
            //            //gán cho rptviewer
            //            crystalReportViewer1.ReportSource = rpt;
            //            crystalReportViewer1.Refresh();
            //        }
            //    }
            //}
        }
    }
}
