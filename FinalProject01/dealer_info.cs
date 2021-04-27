using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalProject01
{
    public partial class dealer_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fives\source\repos\FinalProject01\FinalProject01\login.mdf;Integrated Security=True");
        public dealer_info()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into dealer_info values('"+ textBox1.Text +"','"+ textBox2.Text +"','"+ textBox3.Text +"','"+ textBox4.Text +"','"+ textBox5.Text +"')";
            cmd.ExecuteNonQuery();

            //empty text boxs
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            dg();
            MessageBox.Show("record inserted successfully!");
        }

        private void Dealer_info_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dg();
        }

        public void dg()
        {
            //to show the table in the DataGridView1
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from dealer_info ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //Delete Selcted
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from dealer_info where id="+id+"";
            cmd.ExecuteNonQuery();

            dg();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //update Selected
            panel2.Visible = true;

            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from dealer_info where id="+id+"";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)//so we can take one record
            {
                textBox6.Text = dr["dealer_name"].ToString();
                textBox7.Text = dr["dealer_company_name"].ToString();
                textBox8.Text = dr["contact"].ToString();
                textBox9.Text = dr["address"].ToString();
                textBox10.Text = dr["city"].ToString();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //update dealer

            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update dealer_info set dealer_name='"+ textBox6.Text +"', dealer_company_name='"+ textBox7.Text +"', contact='"+ textBox8.Text +"', address='"+ textBox9.Text +"', city='"+ textBox10.Text +"' where id="+ id +"";
            cmd.ExecuteNonQuery();

            panel2.Visible = false;
            dg();
            MessageBox.Show("record updated successfully!");
        }
    }
}
