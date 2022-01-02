using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATESTAT_LIBRARIE
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            librarieDataSet1.EnforceConstraints = false;
            this.cartiTableAdapter1.Fill(this.librarieDataSet1.Carti);
            this.facturiTableAdapter1.Fill(this.librarieDataSet1.Facturi);
            this.clientiTableAdapter1.Fill(this.librarieDataSet1.Clienti);
            this.detaliiFacturiTableAdapter1.Fill(this.librarieDataSet1.DetaliiFacturi);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.cartiTableAdapter1.AfisareStoc(this.librarieDataSet1.Carti);
            DataTable dt = this.librarieDataSet1.Carti;
            richTextBox1.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
                richTextBox1.Text += dt.Rows[i]["id_carte"].ToString() + ": " + dt.Rows[i]["titlu"].ToString() +" de "+ dt.Rows[i]["autor"] + " = " + dt.Rows[i]["stoc"] + "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            if (comboBox1.SelectedIndex == 0)
            {
                this.facturiTableAdapter1.ComenziLunaActuala(this.librarieDataSet1.Facturi);
                richTextBox2.Text = "Comenzi din luna curenta: " + "\n";
                DataTable dt = this.librarieDataSet1.Facturi;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime d = Convert.ToDateTime(dt.Rows[i]["data_incheierii"]);
                    richTextBox2.Text += dt.Rows[i]["id_factura"].ToString() + ": " + d.Day.ToString() +" "+ d.Month.ToString() +" "+d.Year.ToString() + "\n";
                }
                    


            }
            else if (comboBox1.SelectedIndex == 1)
            {
                this.facturiTableAdapter1.ComandaCartiMaxime(this.librarieDataSet1.Facturi);
                DataTable dt = this.librarieDataSet1.Facturi;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime d = Convert.ToDateTime(dt.Rows[i]["data_incheierii"]);
                    richTextBox2.Text += dt.Rows[i]["id_factura"].ToString() + ": " + d.Day.ToString()+" "+d.Month.ToString()+" "+d.Year.ToString() + " cu numarul total de carti: " + dt.Rows[i]["CantitateTotala"] + "\n";
                }
                   
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                richTextBox2.Text += "Sunt inregistrati " +
                this.clientiTableAdapter1.NumarClientiInregistrati().ToString() + " clienti." + "\n";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                this.detaliiFacturiTableAdapter1.CarteaCeaMaiVanduta(librarieDataSet1.DetaliiFacturi);
                DataTable dt = this.librarieDataSet1.DetaliiFacturi;
                richTextBox2.Text = "Cartea cea mai vanduta este: " + "\n";
                for (int i = 0; i < dt.Rows.Count; i++)
                    richTextBox2.Text +=  dt.Rows[i]["titlu"].ToString() + " de " + dt.Rows[i]["autor"] + "\n";
            }
            else MessageBox.Show("Alegeti o singura varianta");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }
    }
}
