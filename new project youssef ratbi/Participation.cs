using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Participation : Form
    {
        public static SqlConnection cnx = new SqlConnection("data source=DESKTOP-TF9AOA1\\SQLEXPRESS;initial catalog=gde;integrated security=true");
        public static SqlCommand cmd = new SqlCommand("", cnx);
        public SqlDataReader dr;

        public void cnnx()
        {
            cnx.Open();
            cmd.ExecuteNonQuery();
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            cnx.Close();
        }

        public Participation()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add participation
            if (string.IsNullOrEmpty(textBox1.Text) || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Participation (ID_Participation, Date_participation, ID_Membre, ID_Événement) VALUES (" +
                              int.Parse(textBox1.Text) + ", '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + ", " + int.Parse(comboBox2.SelectedItem.ToString()) + ")";
            cnnx();
            MessageBox.Show("Participation ajoutée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Search participation
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour rechercher.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "SELECT * FROM Participation WHERE ID_Participation = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dateTimePicker1.Value = DateTime.Parse(dr["Date_participation"].ToString());
                comboBox1.SelectedItem = dr["ID_Membre"].ToString();
                comboBox2.SelectedItem = dr["ID_Événement"].ToString();
            }
            else
            {
                MessageBox.Show("Aucun enregistrement trouvé avec l'ID donné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
            dr.Close();
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Update participation
            if (string.IsNullOrEmpty(textBox1.Text) || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "UPDATE Participation SET Date_participation = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") +
                              "', ID_Membre = " + int.Parse(comboBox1.SelectedItem.ToString()) +
                              ", ID_Événement = " + int.Parse(comboBox2.SelectedItem.ToString()) +
                              " WHERE ID_Participation = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Participation mise à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete participation
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Participation WHERE ID_Participation = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Participation supprimée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Participation_Load(object sender, EventArgs e)
        {
            // Load Membre IDs into ComboBox1
            cnx.Open();
            cmd.CommandText = "SELECT ID_Membre FROM Membre";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            dr.Close();

            // Load Événement IDs into ComboBox2
            cmd.CommandText = "SELECT ID_Événement FROM Événement";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cnx.Close();
        }
    }
}
