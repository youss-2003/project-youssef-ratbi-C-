using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Comite : Form
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
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            cnx.Close();
        }

        public Comite()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add new comité
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Comité (ID_Comité, Nom_comité, Description_comité, ID_Association) VALUES (" +
                              int.Parse(textBox1.Text) + ", '" + textBox2.Text + "', '" + textBox3.Text + "', " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + ")";
            cnnx();
            MessageBox.Show("Comité ajouté avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Search comité by ID
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour rechercher.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "SELECT * FROM Comité WHERE ID_Comité = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Nom_comité"].ToString();
                textBox3.Text = dr["Description_comité"].ToString();
                comboBox1.SelectedItem = dr["ID_Association"].ToString();
            }
            else
            {
                MessageBox.Show("Aucun enregistrement trouvé avec l'ID donné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.SelectedIndex = -1;
            }
            dr.Close();
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Update comité
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "UPDATE Comité SET Nom_comité = '" + textBox2.Text +
                              "', Description_comité = '" + textBox3.Text +
                              "', ID_Association = " + int.Parse(comboBox1.SelectedItem.ToString()) +
                              " WHERE ID_Comité = " + int.Parse(textBox1.Text);
            cnnx();
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete comité
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Comité WHERE ID_Comité = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Comité supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Comite_Load(object sender, EventArgs e)
        {
            // Load Association IDs into ComboBox when the form loads
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

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }
    }
}
