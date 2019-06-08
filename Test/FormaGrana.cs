using projekat_forme_izgled;
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
using projekat_forme_izgled;

namespace Test
{
    public partial class FormaGrana : Form
    {
        Grana grana;
        Cvor izvor;
        Form1 formica;
        Cvor odrediste;
        int idgrana;
        public FormaGrana(Form1 f,int id,Cvor izvor,Cvor odrediste,Grana preneta_grana)
        {
            formica = f;
            this.izvor = izvor;
            this.odrediste = odrediste;
            this.grana = preneta_grana;
            idgrana = id;
            if (preneta_grana == null)
            {
                grana = new Grana(id,izvor, odrediste, 0);
                izvor.grane.Add(grana);
            }
            InitializeComponent();
            osveziListbox();
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.DimGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                grana.komponente.RemoveAt(listBox1.SelectedIndex);
                grana.brojkom--;
                osveziListbox();
            }
        }

        private void osveziListbox()
        {
            listBox1.Items.Clear();
            foreach (Komponenta k in grana.komponente)
            {
                listBox1.Items.Add(k.uString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(formica,grana);
            f.ShowDialog();
            osveziListbox();
        }  

        private void button5_Click(object sender, EventArgs e)
        {
            form_struja fs = new form_struja(1, grana);
            fs.ShowDialog();
            osveziListbox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            form_struja fs = new form_struja(0, grana);
            fs.ShowDialog();
            osveziListbox();
        }
    }
}
