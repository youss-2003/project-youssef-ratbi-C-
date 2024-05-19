using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Evaluations : Form
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

        public Evaluations()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void Evaluations_Load(object sender, EventArgs e)
        {
            // Load IDs into ComboBox when the form loads
            cnx.Open();
            cmd.CommandText = "SELECT ID_Membre FROM Membre";
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
            // Add new evaluation
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Évaluations (ID_Évaluation, Note, Commentaire, ID_Membre) VALUES (" +
                              int.Parse(textBox1.Text) + ", " + int.Parse(textBox2.Text) + ", '" + textBox3.Text + "', " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + ")";
            cnnx();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Search evaluation by ID
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour rechercher.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "SELECT * FROM Évaluations WHERE ID_Évaluation = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Note"].ToString();
                textBox3.Text = dr["Commentaire"].ToString();
                comboBox1.SelectedItem = dr["ID_Membre"].ToString();
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
            // Update evaluation
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "UPDATE Évaluations SET Note = " + int.Parse(textBox2.Text) +
                              ", Commentaire = '" + textBox3.Text +
                              "', ID_Membre = " + int.Parse(comboBox1.SelectedItem.ToString()) +
                              " WHERE ID_Évaluation = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Évaluation mise à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete evaluation
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Évaluations WHERE ID_Évaluation = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Évaluation supprimée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
