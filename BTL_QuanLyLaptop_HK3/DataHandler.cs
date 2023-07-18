using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyLaptop_HK3
{
    class DataHandler
    {
        private String cnnStr;

        //Hàm tạo
        public DataHandler()
        {
            cnnStr = @"Data Source=PVQ1989\SQLEXPRESS;Initial Catalog=BTL_QuanLyBanLaptop;Integrated Security=True";
        }

        //
        public SqlConnection getConnection()
        {
            return new SqlConnection(cnnStr);
        }
        //get data bang query
        public DataTable getData(String query)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, cnn))
                {
                    adapter.Fill(tbl);
                }
            }
            return tbl;
        }

        //thêm sửa xoá bằng proc
        public bool insertUpdateDelete(string nameProc, SqlParameter[] listParameters)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))
                {
                    using (SqlCommand cmd = new SqlCommand(nameProc, cnn))
                    {
                        cnn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        //addRange() : thêm nhiều tham số 
                        cmd.Parameters.AddRange(listParameters);
                        // trả về số dòng bị ảnh hưởng 
                        return cmd.ExecuteNonQuery() > 0;

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi : " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        //xoá
        public bool delete(string nameProc,string nameParameter,string valueParameter)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))
                {
                    using (SqlCommand cmd = new SqlCommand(nameProc, cnn))
                    {
                        cnn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(nameParameter, valueParameter);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }catch(Exception e)
            {
                MessageBox.Show("Lỗi : " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //get data bằng proc với 1 tham số 
        public DataTable getDataProc(string nameProc, string namePrm, string valuePrm)
        {
            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                using (SqlCommand cmd = new SqlCommand(nameProc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(namePrm, valuePrm);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adapter.Fill(tbl);
                        return tbl;
                    }
                }
            }
        }




    }
}
