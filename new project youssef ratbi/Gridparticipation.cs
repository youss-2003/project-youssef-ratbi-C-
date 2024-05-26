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
    public partial class Gridparticipation : Form
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
        public Gridparticipation()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void Gridparticipation_Load(object sender, EventArgs e)
        {
            cnx.Open();
            cmd.CommandText = "SELECT ID_Membre FROM Membre";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cnx.Close();

            cnx.Open();
            cmd.CommandText = "SELECT ID_Événement FROM Événement";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
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
                // First query: Select from Participation
                cmd.CommandText = "select * from Participation";
                dr = cmd.ExecuteReader();
                bool participationFound = false;

                while (dr.Read())
                {
                    if (dr[2].ToString().Equals(comboBox1.Text) && dr[3].ToString().Equals(comboBox2.Text))
                    {
                        participationFound = true;
                        dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString());
                    }
                }

                dr.Close();

                if (!participationFound)
                {
                    MessageBox.Show("Participation n'existe pas! Veuillez remplir tous les champs.");
                    return;
                }

                // Second query: Select from Membre
                cmd.CommandText = "select * from Membre";
                dr = cmd.ExecuteReader();
                bool membreFound = false;

                while (dr.Read())
                {
                    if (dr[0].ToString().Equals(comboBox1.Text))
                    {
                        membreFound = true;
                        textBox3.Text = dr[1].ToString();
                        break;
                    }
                }

                dr.Close();

                if (!membreFound)
                {
                    MessageBox.Show("Membre n'existe pas!");
                    return;
                }

                // Third query: Select from Événement
                cmd.CommandText = "select * from Événement";
                dr = cmd.ExecuteReader();
                bool evenementFound = false;

                while (dr.Read())
                {
                    if (dr[0].ToString().Equals(comboBox2.Text))
                    {
                        evenementFound = true;
                        textBox1.Text = dr[1].ToString();
                        break;
                    }
                }

                dr.Close();

                if (!evenementFound)
                {
                    MessageBox.Show("Événement n'existe pas!");
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
    }
}
