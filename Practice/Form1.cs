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

namespace Practice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCbbLoaiHang();
            LoadData();


        }
        
        private void InsertHang()
        {
            string connnect = "Data Source=DESKTOP-IKP0EDP;Initial Catalog=practice;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connnect);
            connection.Open();
            string sql = "Insert into VatTu values(@TenHang, @DonViTinh, @MaLoai)";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("TenHang", txtTenHang.Text);
            cmd.Parameters.AddWithValue("DonViTinh", cbbDonViTinh.Text);
            cmd.Parameters.AddWithValue("MaLoai", int.Parse( cbbLoaiHang.SelectedValue.ToString()));
            cmd.ExecuteNonQuery();
            connection.Close();
            LoadData();
        }
        private void LoadCbbLoaiHang()
        {
            string connnect = "Data Source=DESKTOP-IKP0EDP;Initial Catalog=practice;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connnect);
            connection.Open();
            string sql = "Select * from LoaiVT";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader dt = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(dt);
            connection.Close();
            
            cbbLoaiHang.DisplayMember = "TenLoaiVT";
            cbbLoaiHang.ValueMember = "MaLoai";
            cbbLoaiHang.DataSource = data;
        }
        private void LoadData()
        {
            string connnect = "Data Source=DESKTOP-IKP0EDP;Initial Catalog=practice;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connnect);
            connection.Open();
            string sql = "Select * from VatTu";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader dt = cmd.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(dt);
            dataGridView1.DataSource = data;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertHang();
        }

     

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtTenHang.Text = row.Cells[1].Value.ToString();
                cbbLoaiHang.SelectedValue = int.Parse(row.Cells[3].Value.ToString());
                cbbDonViTinh.Text = row.Cells[2].Value.ToString();
                lblMaVT.Text = row.Cells[0].Value.ToString();
            }
                
        }

        private void UpdateVT()
        {
            string connnect = "Data Source=DESKTOP-IKP0EDP;Initial Catalog=practice;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connnect);
            connection.Open();
            string sql = "update VatTu set TenVT = @TenHang, DvTinh =  @DonViTinh, Maloai = @MaLoai where MaVT = @MaVT";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("TenHang", txtTenHang.Text);
            cmd.Parameters.AddWithValue("DonViTinh", cbbDonViTinh.Text);
            cmd.Parameters.AddWithValue("MaLoai", int.Parse(cbbLoaiHang.SelectedValue.ToString()));
            cmd.Parameters.AddWithValue("MaVT", int.Parse(lblMaVT.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateVT();
        }

        private void DeleteVT()
        {
            string connnect = "Data Source=DESKTOP-IKP0EDP;Initial Catalog=practice;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connnect);
            connection.Open();
            string sql = "delete from VatTu where MaVT = @MaVT";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("MaVT", int.Parse(lblMaVT.Text));
            cmd.ExecuteNonQuery();
            connection.Close();
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteVT();
        }
    }
}
