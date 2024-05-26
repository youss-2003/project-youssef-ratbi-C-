using System;
using System.Windows.Forms;

namespace new_project_youssef_ratbi
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            // Populate ComboBox1 with form names
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
            // Populate ComboBox2 with form names
            comboBox2.Items.Add("Ville");
            comboBox2.Items.Add("Quartier");
            comboBox2.Items.Add("Association");
            comboBox2.Items.Add("Membre");
            comboBox2.Items.Add("Rôles");
            comboBox2.Items.Add("Événement");
            comboBox2.Items.Add("Finance");
            comboBox2.Items.Add("Participation");
            comboBox2.Items.Add("Comité");
            comboBox2.Items.Add("Évaluations");
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedForm = comboBox2.SelectedItem.ToString();
            switch (selectedForm)
            {
                case "Ville":
                    Gridville gdv = new Gridville();
                    gdv.Show();
                    this.Hide();
                    break;
                case "Quartier":
                    Gridquartier qua = new Gridquartier();
                    qua.Show();
                    this.Hide();
                    break;
                case "Association":
                    Gridassociation gass = new Gridassociation();
                    gass.Show();
                    this.Hide();
                    break;
                case "Membre":
                    Gridmembre gmem = new Gridmembre();
                    gmem.Show();
                    this.Hide();
                    break;
                case "Rôles":
                    Gridroles grol = new Gridroles();
                    grol.Show();
                    this.Hide();
                    break;
                case "Événement":
                    Gridevenement geve = new Gridevenement();
                    geve.Show();
                    this.Hide();
                    break;
                case "Finance":
                    Gridfinance gfin = new Gridfinance();
                    gfin.Show();
                    this.Hide();
                    break;
                case "Participation":
                    Gridparticipation gpar = new Gridparticipation();
                    gpar.Show();
                    this.Hide();
                    break;
                case "Comité":
                    Gridcomite gcom = new Gridcomite();
                    gcom.Show();
                    this.Hide();
                    break;
                case "Évaluations":
                    Gridevaluation geva = new Gridevaluation();
                    geva.Show();
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("Fonction n'existe pas !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
