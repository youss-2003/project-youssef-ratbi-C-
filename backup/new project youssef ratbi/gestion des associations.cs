using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace new_project_youssef_ratbi
{
    public partial class gestion_des_associations : Form
    {
        public static SqlConnection cnx = new SqlConnection("data source =DESKTOP-TF9AOA1\\SQLEXPRESS; initial catalog = gde; integrated security = true ");
        public static SqlCommand cmd = new SqlCommand("", cnx);
        public SqlDataReader dr;
        public void cnnx()
        {
            cnx.Open();
            dr = cmd.ExecuteReader();
            textBox1.Clear();
            textBox2.Clear();
            cnx.Close();
        }
        public gestion_des_associations()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard dash = new dashboard();
            dash.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gestion_des_associations_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Bouton d'ajout
            cmd.CommandText = "INSERT INTO Ville (ID_ville, Nom_ville) VALUES (" + int.Parse(textBox1.Text) + ", '" + textBox2.Text + "')";
            cnnx();
            MessageBox.Show("Enregistrement ajouté avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
         }

        private void button2_Click(object sender, EventArgs e)
        {
            
                // Vérifiez si textBox1 est vide
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Veuillez entrer un ID pour rechercher.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Bouton de recherche
                cmd.CommandText = "SELECT * FROM Ville WHERE ID_ville = " + int.Parse(textBox1.Text);
                cnx.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox2.Text = dr["Nom_ville"].ToString();
                }
                else
                {
                    MessageBox.Show("Aucun enregistrement trouvé avec l'ID donné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                }
                cnx.Close();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Bouton de mise à jour
            cmd.CommandText = "UPDATE Ville SET Nom_ville = '" + textBox2.Text + "' WHERE ID_ville = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Enregistrement mis à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Veuillez entrer un ID pour supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Bouton de suppression
            cmd.CommandText = "DELETE FROM Ville WHERE ID_ville = " + int.Parse(textBox1.Text);
            cnnx();
            MessageBox.Show("Enregistrement supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
