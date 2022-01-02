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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            librarieDataSet1.EnforceConstraints = false;
            this.autentificareTableAdapter.Fill(this.librarieDataSet1.Autentificare);
            this.clientiTableAdapter1.Fill(this.librarieDataSet1.Clienti);
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedTab = tabControl1.TabPages["CLIENT NOU"];
            tabControl1.SelectTab(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedTab = tabControl1.TabPages["ANGAJAT"];
            tabControl1.SelectTab(3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedTab = tabControl1.TabPages["CLIENT INREGISTRAT"];
            tabControl1.SelectTab(2);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //deschiderea form angajat
            this.autentificareTableAdapter.AutentificareAngajat(librarieDataSet1.Autentificare, textBox8.Text, textBox9.Text);
            DataTable dt = this.librarieDataSet1.Autentificare;
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                Form3 f3 = new Form3();
                f3.ShowDialog();
                this.Close();
            }
            else MessageBox.Show("Nu s-a introdus corect");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //deschidere form client
            this.autentificareTableAdapter.AutentificareClient(librarieDataSet1.Autentificare, textBox6.Text, textBox7.Text);
            DataTable dt = this.librarieDataSet1.Autentificare;
            if (dt.Rows.Count == 1)
            {
                this.autentificareTableAdapter.ReturnIdClient(librarieDataSet1.Autentificare, textBox6.Text, textBox7.Text);
                DataTable dt1 = this.librarieDataSet1.Autentificare;
                Program.id_client = Convert.ToInt32(dt1.Rows[0]["id_clienti"]);
                this.Hide();
                Form2 f2 = new Form2();
                f2.ShowDialog();
                this.Close();
            }
            else MessageBox.Show("Nu s-a introdus corect");


            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'librarieDataSet1.Autentificare' table. You can move, or remove it, as needed.
            this.autentificareTableAdapter.Fill(this.librarieDataSet1.Autentificare);

        }

        private void autentificarebindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||
                textBox5.Text == "")
                MessageBox.Show("toate campurile trebuie completate");
            else
            {
                this.clientiTableAdapter1.EmailLaFel(this.librarieDataSet1.Clienti, textBox3.Text);
                DataTable dt = this.librarieDataSet1.Clienti;
                if (dt.Rows.Count == 0)
                {
                    //trebuie facut
                    this.clientiTableAdapter1.InsertClient(textBox1.Text, textBox2.Text, Convert.ToInt32(textBox5.Text), textBox3.Text, textBox4.Text);
                    clientiTableAdapter1.Update(librarieDataSet1);
                    this.clientiTableAdapter1.IdClient(librarieDataSet1.Clienti, textBox1.Text, textBox2.Text);
                    DataTable dt1 = this.librarieDataSet1.Clienti;
                    int id = Convert.ToInt32(dt1.Rows[0]["id_clienti"]);
                    this.autentificareTableAdapter.InsertAutentificareClient(textBox10.Text, textBox11.Text, id);
                    autentificareTableAdapter.Update(librarieDataSet1);
                    MessageBox.Show("Va puteti loga in aplicatie!");
                    tabControl1.SelectTab(2);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();

                }
                else MessageBox.Show("Acest email a fost utilizat deja");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //this.clientiTableAdapter1.Fill(librarieDataSet1.Clienti);
            //DataTable dt = librarieDataSet1.Clienti;
            //de terminat 

            //for (int i = 0; i < dt.Rows.Count; i++)
                //richTextBox1.Text += dt.Rows[i]["id_clienti"].ToString() + " " + dt.Rows[i]["nume"].ToString()+"\n";
        }
    }
}
