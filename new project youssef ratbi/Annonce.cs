using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Annonce : Form
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
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            cnx.Close();
        }

        public Annonce()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Annonce (ID_Annonce, Titre_annonce, Contenu_annonce, Date_publication, ID_Association) VALUES (" +
                              int.Parse(textBox1.Text) + ", '" + textBox2.Text + "', '" + textBox3.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + ")";
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

            cmd.CommandText = "SELECT * FROM Annonce WHERE ID_Annonce = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Titre_annonce"].ToString();
                textBox3.Text = dr["Contenu_annonce"].ToString();
                dateTimePicker1.Value = DateTime.Parse(dr["Date_publication"].ToString());
                comboBox1.SelectedItem = dr["ID_Association"].ToString();
            }
            else
            {
                MessageBox.Show("Aucun enregistrement trouvé avec l'ID donné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox3.Clear();
                dateTimePicker1.Value = DateTime.Now;
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

            cmd.CommandText = "UPDATE Annonce SET Titre_annonce = '" + textBox2.Text + "', Contenu_annonce = '" + textBox3.Text +
                              "', Date_publication = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', ID_Association = " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + " WHERE ID_Annonce = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Enregistrement mis à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Annonce WHERE ID_Annonce = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Enregistrement supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Annonce_Load(object sender, EventArgs e)
        {
            // Load IDs into ComboBox when the form loads
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
