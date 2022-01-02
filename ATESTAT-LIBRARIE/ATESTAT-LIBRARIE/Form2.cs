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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            librarieDataSet1.EnforceConstraints = false;
            this.cartiTableAdapter1.Fill(this.librarieDataSet1.Carti);
            this.detaliiFacturiTableAdapter1.Fill(this.librarieDataSet1.DetaliiFacturi);
            this.facturiTableAdapter1.Fill(this.librarieDataSet1.Facturi);
            textBox2.Text = "0";
           
        }
        Carte[] v = new Carte[36];
        int cate = 0;
        int ales = -1;
        Carte[] cos = new Carte[10];
        int cateCos = 0;
        
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void Form2_FormClosed_1(object sender, FormClosedEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            cate = 0;
            if (radioButton1.Checked==true)
            {
                //dupa genre
                this.cartiTableAdapter1.AfisareGenre(librarieDataSet1.Carti, comboBox1.Text);
                DataTable dt = this.librarieDataSet1.Carti;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Carte c = new Carte(dt.Rows[i]["titlu"].ToString(), dt.Rows[i]["autor"].ToString(), Convert.ToDouble(dt.Rows[i]["pret"]));
                    v[cate] = c;
                    listBox1.Items.Add(v[cate].InformatiiCarte());
                    cate++;
                }

            }
            else
            {
                //dupa pret
                this.cartiTableAdapter1.AfisarePretMaxim(librarieDataSet1.Carti, Convert.ToDecimal(numericUpDown1.Value));
                DataTable dt = this.librarieDataSet1.Carti;
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    Carte c = new Carte(dt.Rows[i]["titlu"].ToString(), dt.Rows[i]["autor"].ToString(), Convert.ToDouble(dt.Rows[i]["pret"]));
                    v[cate] = c;
                    listBox1.Items.Add(v[cate].InformatiiCarte());
                    cate++;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cate; i++)
                if (v[i].titlu.ToString() == textBox1.Text) ales = i;
            tabControl1.SelectTab(2);
            if (ales == -1)
                MessageBox.Show("scrieti un titlu corect");
            else
            {
                this.cartiTableAdapter1.CarteaAleasa(this.librarieDataSet1.Carti, v[ales].titlu.ToString());
                DataTable dt = this.librarieDataSet1.Carti;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double pret = Convert.ToDouble(dt.Rows[i]["pret"]);
                    richTextBox1.Text = dt.Rows[i]["descriere"].ToString();
                    richTextBox2.Text = dt.Rows[i]["titlu"].ToString() + " de " + dt.Rows[i]["autor"] + "\n";
                    richTextBox2.Text += pret.ToString() + " lei" + "\n";
                    richTextBox2.Text += dt.Rows[i]["genre"].ToString() + " de la editura: " + dt.Rows[i]["editura"].ToString() + "\n";
                    richTextBox2.Text += dt.Rows[i]["nr_pagini"].ToString() + " de pagini" + "\n";
                }
                ales = -1;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //comanda
            DateTime acum = DateTime.Now;
            this.facturiTableAdapter1.FacturaNoua(acum.ToString(),Program.id_client);
            this.facturiTableAdapter1.Update(librarieDataSet1);
            this.facturiTableAdapter1.ReturnIdFactura(librarieDataSet1.Facturi, acum.ToString(), Program.id_client);
            DataTable dtf = this.librarieDataSet1.Facturi;
            int id_factura = Convert.ToInt32(dtf.Rows[0]["id_factura"]);
            for (int i=0;i<cateCos;i++)
            {
                this.cartiTableAdapter1.IdCarte(librarieDataSet1.Carti,cos[i].titlu);
                DataTable dt = librarieDataSet1.Carti;
                int id_carte = Convert.ToInt32(dt.Rows[0]["id_carte"]);
                this.detaliiFacturiTableAdapter1.DetaliiNoi(id_carte,cos[i].cantitate,id_factura);
                this.detaliiFacturiTableAdapter1.Update(librarieDataSet1);
                this.cartiTableAdapter1.UpdateStoc(cos[i].cantitate, id_carte, id_carte);
                this.cartiTableAdapter1.Update(librarieDataSet1);

            }
            MessageBox.Show("Comanda a fost plasata!");
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //adaugare in cos
            string titlu = textBox1.Text;
            int ok = 0;
            int loc=-1;
            for (int i=0;i<cateCos;i++)
            {
                if (cos[i].titlu == titlu)
                {
                    this.cartiTableAdapter1.StocTitlu(librarieDataSet1.Carti, titlu);
                    DataTable dt = this.librarieDataSet1.Carti;
                    int stoc = Convert.ToInt32(dt.Rows[0]["stoc"]);
                    if (stoc - (cos[i].cantitate + 1) >= 0)
                    {
                        cos[i].cantitate++;
                        ok = 1;
                        loc = i;
                    }
                    else MessageBox.Show("Nu sunt suficiente exemplare de aceasta carte! Ne pare rau!");
                }
            }
            if (ok==1)
            {
                listBox2.Items.Clear();
                for (int i = 0; i < cateCos; i++)
                    listBox2.Items.Add(cos[i].InformatiiCarteCantitate());
                double pret_total = Convert.ToDouble(textBox2.Text) +cos[loc].pret;
                textBox2.Text = pret_total.ToString();
            }
            if (ok == 0)
            {
                Carte aux = new Carte();
                aux.titlu = titlu;
                for (int i = 0; i < cate; i++)
                    if (titlu == v[i].titlu)
                    {
                        aux.autor = v[i].autor;
                        aux.pret = v[i].pret;
                    }
                cos[cateCos] = aux;
                cos[cateCos].cantitate = 1;
                listBox2.Items.Add(cos[cateCos].InformatiiCarteCantitate());
                loc = cateCos;
                cateCos++;
                textBox1.Clear();
                double pret_momentan = Convert.ToDouble(textBox2.Text);
                double pret_actual = pret_momentan + cos[loc].cantitate * cos[loc].pret;
                textBox2.Text = pret_actual.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //stergerea unei carti din cos
            int x = listBox2.SelectedIndex;
            double pret = Convert.ToInt32(textBox2.Text)-cos[x].cantitate*cos[x].pret;

            for (int i=x;i<cateCos-1;i++)
            {
                cos[i] = cos[i + 1];
            }
            cateCos--;
            listBox2.Items.Clear();
            textBox2.Text = Convert.ToString(pret);
            for (int i = 0; i < cateCos; i++)
                listBox2.Items.Add(cos[i].InformatiiCarteCantitate());

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }
    }
}
