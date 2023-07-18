using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyLaptop_HK3
{
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhanVien frmNhanVien = new frmNhanVien();
            frmNhanVien.MdiParent = this;
            frmNhanVien.Show();
        }

        private void mặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMatHang frmMatHang = new frmMatHang();
            frmMatHang.MdiParent = this;
            frmMatHang.Show();
        }

        private void loạiHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoaiHang frmLoaiHang = new frmLoaiHang();
            frmLoaiHang.MdiParent = this;
            frmLoaiHang.Show();
        }

        private void hoáĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonNhap frmHoaDonNhap = new frmHoaDonNhap();
            frmHoaDonNhap.MdiParent = this;
            frmHoaDonNhap.Show();
        }

        private void chiTiếtHoáĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChiTietHoaDonNhap frm = new frmChiTietHoaDonNhap(0);
            frm.MdiParent = this;
            frm.Show();
        }

        private void hoáĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonBan frm = new frmHoaDonBan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void chiTiếtHoáĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChiTietHoaDonBan frm = new frmChiTietHoaDonBan(0);
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
