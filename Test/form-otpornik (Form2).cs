using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test;

namespace projekat_forme_izgled
{
    public partial class Form2 : Form
    {
        Grana g;
        Form1 formica;
        public Form2(Form1 f,Grana prosledjenaGrana)
        {
            formica = f;
            g = prosledjenaGrana;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            descriptions i = new descriptions(1);
            i.Show();          
        }
        private void button2_Click(object sender, EventArgs e)
        {
           int f;
            if (Int32.TryParse(textBox1.Text, out f))
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Nije unet naziv!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Komponenta k = new Otpornik(Convert.ToInt32(textBox1.Text),textBox2.Text);
                k.namestiSliku(g.izvor,g.odrediste);
                g.komponente.Add(k);
                g.brojkom++;   
            }
            else
            {
                textBox1.Text = "";
                MessageBox.Show("Uneta je losa vrednost!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }
    }
}
