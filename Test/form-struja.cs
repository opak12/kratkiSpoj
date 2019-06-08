using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test;

namespace projekat_forme_izgled
{
    public partial class form_struja : Form
    {
        Grana g;
        int rezimRada; //1 struja, 0 napon
        public form_struja(int rezim,Grana prenetaGrana)
        {
            g = prenetaGrana;
            InitializeComponent();
            rezimRada = rezim;
            postaviSliku();
        }
        private void postaviSliku()
        {
            if (rezimRada==0)
            {
                pictureBox1.Image = Test.Properties.Resources.voltmeter;
            }
            else
            {
                pictureBox1.Image = Test.Properties.Resources.kisspng_current_source_alternating_current_electric_curren_voltage_source_5b3c06a78cb4b9_1733158615306605195763;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int f;
            if (Int32.TryParse(textBox2.Text, out f))
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Nije unet Naziv!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);                 
                    return;
                }
                if (rezimRada == 0)
                {
                    Komponenta k = new NaponskiGenerator(Convert.ToInt32(textBox2.Text), textBox1.Text,true);
                    if (radioButton1.Checked == true)
                    {
                        k.polaritet = g.izvor;
                        k.frontPolaritet = k.polaritet;
                    }
                    else
                    {
                        k.polaritet = g.odrediste;
                        k.frontPolaritet = k.polaritet;
                    }
                    k.namestiSliku(g.izvor, g.odrediste);
                    g.komponente.Add(k);
                    g.brojkom++;
                }
                if (rezimRada == 1)
                {
                    Komponenta k = new StrujniGenerator(Convert.ToInt32(textBox2.Text), textBox1.Text, true);
                    if (radioButton1.Checked == true)
                    {
                        k.polaritet = g.izvor;
                        k.frontPolaritet = g.izvor;
                    }
                    else
                    {
                        k.polaritet = g.odrediste;
                        k.frontPolaritet = g.odrediste;
                    }
                    k.namestiSliku(g.izvor, g.odrediste);
                    g.komponente.Add(k);
                    g.brojkom++;
                }
            }
            else
            {
                textBox1.Text = "";
                MessageBox.Show("Uneta je losa vrednost!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (rezimRada == 1)
            {
                descriptions i = new descriptions(2);
                i.Show();
            }
            else
            {
                descriptions i = new descriptions(3);
                i.Show();
            }
        }
    }
}
