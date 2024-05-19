using System;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            // Populate ComboBox with form names
            comboBox1.Items.Add("Gestion des associations");
            comboBox1.Items.Add("Quartier");
            comboBox1.Items.Add("Association");
            comboBox1.Items.Add("Annonce");
            comboBox1.Items.Add("Membre");
            comboBox1.Items.Add("Rôles");
            comboBox1.Items.Add("Événement");
            comboBox1.Items.Add("Finance");
            comboBox1.Items.Add("Participation");
            comboBox1.Items.Add("Comité");
            comboBox1.Items.Add("Évaluations");
            comboBox1.Items.Add("Form2");
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 conx = new Form1();
            conx.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form1 conx = new Form1();
            conx.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Navigate to the selected form
            string selectedForm = comboBox1.SelectedItem.ToString();
            switch (selectedForm)
            {
                case "Gestion des associations":
                    gestion_des_associations gde = new gestion_des_associations();
                    gde.Show();
                    this.Hide();
                    break;
                case "Quartier":
                    Quartier qua = new Quartier();
                    qua.Show();
                    this.Hide();
                    break;
                case "Association":
                    Association ass = new Association();
                    ass.Show();
                    this.Hide();
                    break;
                case "Annonce":
                    Annonce ann = new Annonce();
                    ann.Show();
                    this.Hide();
                    break;
                case "Membre":
                    Membre mem = new Membre();
                    mem.Show();
                    this.Hide();
                    break;
                case "Rôles":
                    Roles rol = new Roles();
                    rol.Show();
                    this.Hide();
                    break;
                case "Événement":
                    Evenement eve = new Evenement();
                    eve.Show();
                    this.Hide();
                    break;
                case "Finance":
                    Finance dash = new Finance();
                    dash.Show();
                    this.Hide();
                    break;
                case "Participation":
                    Participation par = new Participation();
                    par.Show();
                    this.Hide();
                    break;
                case "Comité":
                    Comite com = new Comite();
                    com.Show();
                    this.Hide();
                    break;
                case "Documents":
                    Documents doc = new Documents();
                    doc.Show();
                    this.Hide();
                    break;
                case "Évaluations":
                    Evaluations eva = new Evaluations();
                    eva.Show();
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("Unknown selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
