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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace new_project_youssef_ratbi
{
    public partial class Gridville : Form
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
        public Gridville()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void Gridville_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cmd.CommandText = "select * from Ville";
            cnx.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            cnx.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
