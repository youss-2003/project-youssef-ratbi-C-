﻿using System;
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
    public partial class Gridquartier : Form
    {
        public static SqlConnection cnx = new SqlConnection("data source =DESKTOP-TF9AOA1\\SQLEXPRESS; initial catalog = gde; integrated security = true ");
        public static SqlCommand cmd = new SqlCommand("", cnx);
        public SqlDataReader dr;
        public void cnnx()
        {
            cnx.Open();
            dr = cmd.ExecuteReader();
            
            cnx.Close();
        }
        public Gridquartier()
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


            dataGridView1.Rows.Clear();
            cnx.Open(); // Open the connection once at the beginning

            try
            {
                // First query: Select from Quartier
                cmd.CommandText = "select * from Quartier";
                dr = cmd.ExecuteReader();
                bool quartierFound = false;

                while (dr.Read())
                {
                    if (dr[2].ToString().Equals(comboBox1.Text))
                    {
                        quartierFound = true;
                        dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                    }
                }

                dr.Close();

                if (!quartierFound)
                {
                    MessageBox.Show("Ville n'existe pas! Veuillez remplir tous les champs.");
                    return;
                }

                // Second query: Select from Ville
                cmd.CommandText = "select * from Ville";
                dr = cmd.ExecuteReader();
                bool villeFound = false;

                while (dr.Read())
                {
                    if (dr[0].ToString().Equals(comboBox1.Text))
                    {
                        villeFound = true;
                        textBox3.Text = dr[1].ToString();
                        break;
                    }
                }

                dr.Close();

                if (!villeFound)
                {
                    MessageBox.Show("Ville n'existe pas! Veuillez remplir tous les champs.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed in the finally block
                if (cnx.State == System.Data.ConnectionState.Open)
                {
                    cnx.Close();
                }
            }

        }

        private void Gridquartier_Load(object sender, EventArgs e)
        {
            cnx.Open();
            cmd.CommandText = "SELECT ID_ville FROM Ville";
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

