using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Quartier : Form
    {
        public static SqlConnection cnx = new SqlConnection("data source=DESKTOP-TF9AOA1\\SQLEXPRESS;initial catalog=gde;integrated security=true");
        public static SqlCommand cmd = new SqlCommand("", cnx);
        public SqlDataReader dr;

        public void cnnx()
        {
            cnx.Open();
            cmd.ExecuteNonQuery();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            cnx.Close();
        }

        public Quartier()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add to database
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Quartier (ID_quartier, Nom_quartier, ID_ville) VALUES (" + int.Parse(textBox1.Text) + ", '" + textBox2.Text + "', " + int.Parse(comboBox1.SelectedItem.ToString()) + ")";
            cnnx();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Search
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour rechercher.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "SELECT * FROM Quartier WHERE ID_quartier = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Nom_quartier"].ToString();
                comboBox1.SelectedItem = dr["ID_ville"].ToString();
            }
            else
            {
                MessageBox.Show("Aucun enregistrement trouvé avec l'ID donné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                comboBox1.SelectedIndex = -1;
            }
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Update
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "UPDATE Quartier SET Nom_quartier = '" + textBox2.Text + "', ID_ville = " + int.Parse(comboBox1.SelectedItem.ToString()) + " WHERE ID_quartier = " + int.Parse(textBox1.Text);
            cnnx();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Quartier WHERE ID_quartier = " + int.Parse(textBox1.Text);
            cnnx();
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void Quartier_Load(object sender, EventArgs e)
        {
            // Load IDs into ComboBox when the form loads
            cnx.Open();
            cmd.CommandText = "SELECT ID_ville FROM Ville";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cnx.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }
    }
}
