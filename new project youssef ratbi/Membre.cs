using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class Membre : Form
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

        public Membre()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "INSERT INTO Membre (ID_Membre, Nom, Prénom, E_mail, Date_adhésion, ID_Association) VALUES (" +
                              int.Parse(textBox1.Text) + ", '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" +
                              dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " + int.Parse(comboBox1.SelectedItem.ToString()) + ")";
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

            cmd.CommandText = "SELECT * FROM Membre WHERE ID_Membre = " + int.Parse(textBox1.Text);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Nom"].ToString();
                textBox3.Text = dr["Prénom"].ToString();
                textBox4.Text = dr["E_mail"].ToString();
                dateTimePicker1.Value = DateTime.Parse(dr["Date_adhésion"].ToString());
                comboBox1.SelectedItem = dr["ID_Association"].ToString();
            }
            else
            {
                MessageBox.Show("Aucun enregistrement trouvé avec l'ID donné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.SelectedIndex = -1;
            }
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Update
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "UPDATE Membre SET Nom = '" + textBox2.Text + "', Prénom = '" + textBox3.Text + "', E_mail = '" + textBox4.Text +
                              "', Date_adhésion = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', ID_Association = " +
                              int.Parse(comboBox1.SelectedItem.ToString()) + " WHERE ID_Membre = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Membre mis à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = "DELETE FROM Membre WHERE ID_Membre = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Membre supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }


        private void Membre_Load(object sender, EventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
