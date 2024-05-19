using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Finance : Form
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
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            cnx.Close();
        }

        public Finance()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ajouter une transaction financière
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Finance (ID_Finances, Date_transaction, Montant_transaction, ID_Association) VALUES (" +
                              int.Parse(textBox1.Text) + ", '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " + decimal.Parse(textBox2.Text) + ", " + int.Parse(comboBox1.SelectedItem.ToString()) + ")";
            cnnx();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Rechercher une transaction financière
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour rechercher.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "SELECT * FROM Finance WHERE ID_Finances = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dateTimePicker1.Value = DateTime.Parse(dr["Date_transaction"].ToString());
                textBox2.Text = dr["Montant_transaction"].ToString();
                comboBox1.SelectedItem = dr["ID_Association"].ToString();
            }
            else
            {
                MessageBox.Show("Aucun enregistrement trouvé avec l'ID donné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.SelectedIndex = -1;
            }
            dr.Close();
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Mettre à jour une transaction financière
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "UPDATE Finance SET Date_transaction = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', Montant_transaction = " + decimal.Parse(textBox2.Text) + ", ID_Association = " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + " WHERE ID_Finances = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Transaction financière mise à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Supprimer une transaction financière
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Finance WHERE ID_Finances = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Transaction financière supprimée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void Finance_Load(object sender, EventArgs e)
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
    }
}
