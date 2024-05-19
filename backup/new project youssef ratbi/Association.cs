using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Association : Form
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

        public Association()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void Association_Load(object sender, EventArgs e)
        {
            // Load IDs into ComboBox when the form loads
            cnx.Open();
            cmd.CommandText = "SELECT ID_quartier FROM Quartier";
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
            // Add
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Association (ID_Association, Nom_association, Description_association, Date_association, ID_quartier) VALUES (" +
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

            cmd.CommandText = "SELECT * FROM Association WHERE ID_Association = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Nom_association"].ToString();
                textBox3.Text = dr["Description_association"].ToString();
                dateTimePicker1.Value = DateTime.Parse(dr["Date_association"].ToString());
                comboBox1.SelectedItem = dr["ID_quartier"].ToString();
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

            cmd.CommandText = "UPDATE Association SET Nom_association = '" + textBox2.Text + "', Description_association = '" + textBox3.Text +
                              "', Date_association = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', ID_quartier = " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + " WHERE ID_Association = " + int.Parse(textBox1.Text);
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

            cmd.CommandText = "DELETE FROM Association WHERE ID_Association = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Enregistrement supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
