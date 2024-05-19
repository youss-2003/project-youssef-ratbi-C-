using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Documents : Form
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

        public Documents()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void Documents_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Add new document
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Documents (ID_Document, Nom_document, Type_document, ID_Association) VALUES (" +
                              int.Parse(textBox1.Text) + ", '" + textBox2.Text + "', '" + textBox3.Text + "', " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + ")";
            cnnx();
            MessageBox.Show("Document ajouté avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Search document by ID
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour rechercher.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "SELECT * FROM Documents WHERE ID_Document = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Nom_document"].ToString();
                textBox3.Text = dr["Type_document"].ToString();
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
            // Update document
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "UPDATE Documents SET Nom_document = '" + textBox2.Text +
                              "', Type_document = '" + textBox3.Text +
                              "', ID_Association = " + int.Parse(comboBox1.SelectedItem.ToString()) +
                              " WHERE ID_Document = " + int.Parse(textBox1.Text);
            cnnx();
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete document
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Documents WHERE ID_Document = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Document supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
