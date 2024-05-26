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

namespace new_project_youssef_ratbi
{
    public partial class Gridmembre : Form
    {
        public static SqlConnection cnx = new SqlConnection("data source =DESKTOP-TF9AOA1\\SQLEXPRESS; initial catalog = gde; integrated security = true ");
        public static SqlCommand cmd = new SqlCommand("", cnx);
        public SqlDataReader dr;
        public void cnnx()
        {
            cnx.Open();
            dr = cmd.ExecuteReader();

            cnx.Close();
        }
        public Gridmembre()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void Gridmembre_Load(object sender, EventArgs e)
        {
            cnx.Open();
            cmd.CommandText = "SELECT ID_Association FROM Association";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cnx.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cnx.Open(); // Open the connection once at the beginning

            try
            {
                // First query: Select from Membre
                cmd.CommandText = "select * from Membre";
                dr = cmd.ExecuteReader();
                bool quartierFound = false;

                while (dr.Read())
                {
                    if (dr[5].ToString().Equals(comboBox1.Text))
                    {
                        quartierFound = true;
                        dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                    }
                }

                dr.Close();

                if (!quartierFound)
                {
                    MessageBox.Show("Membre n'existe pas!");
                    return;
                }

                // Second query: Select from Association
                cmd.CommandText = "select * from Association";
                dr = cmd.ExecuteReader();
                bool villeFound = false;

                while (dr.Read())
                {
                    if (dr[0].ToString().Equals(comboBox1.Text))
                    {
                        villeFound = true;
                        textBox3.Text = dr[1].ToString();
                        break;
                    }
                }

                dr.Close();

                if (!villeFound)
                {
                    MessageBox.Show("Association n'existe pas!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed in the finally block
                if (cnx.State == System.Data.ConnectionState.Open)
                {
                    cnx.Close();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
