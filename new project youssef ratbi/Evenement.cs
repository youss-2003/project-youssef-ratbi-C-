using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Evenement : Form
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
        public Evenement()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Ajouter un événement
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Événement (ID_Événement, Nom_événement, Date_événement, Lieu_événement, ID_Association) VALUES (" +
                              int.Parse(textBox1.Text) + ", '" + textBox2.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + textBox3.Text + "', " + int.Parse(comboBox1.SelectedItem.ToString()) + ")";
            cnnx();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Rechercher un événement
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour rechercher.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "SELECT * FROM Événement WHERE ID_Événement = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Nom_événement"].ToString();
                dateTimePicker1.Value = DateTime.Parse(dr["Date_événement"].ToString());
                textBox3.Text = dr["Lieu_événement"].ToString();
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
            dr.Close();
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Mettre à jour un événement
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "UPDATE Événement SET Nom_événement = '" + textBox2.Text + "', Date_événement = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', Lieu_événement = '" + textBox3.Text + "', ID_Association = " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + " WHERE ID_Événement = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Événement mis à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Supprimer un événement
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Événement WHERE ID_Événement = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Événement supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Evenement_Load(object sender, EventArgs e)
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
