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

namespace Test
{
    public partial class FormaGrana : Form
    {
        Grana grana;
        Cvor izvor;
        Cvor odrediste;
        public FormaGrana(Cvor izvor,Cvor odrediste,Grana preneta_grana)
        {
            this.izvor = izvor;
            this.odrediste = odrediste;
            this.grana = preneta_grana;
            if (preneta_grana == null)
            {
                grana = new Grana(izvor, odrediste, 0);
                izvor.grane.Add(grana);
            }
            InitializeComponent();
            osveziListbox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
               // Debugger.Break();
                Komponenta k = new Otpornik(10, "R1");
                k.slika = Image.FromFile("opt.png");
                grana.komponente.Add(k);
                grana.brojkom++;
                osveziListbox();
            }
            if (radioButton2.Checked == true)
            {
                Komponenta k = new NaponskiGenerator(10, "E1", true);
                k.slika = Image.FromFile("napon.png");
                grana.komponente.Add(k);
                grana.brojkom++;
                osveziListbox();
            }
            if (radioButton3.Checked == true)
            {
                Komponenta k = new StrujniGenerator(10, "J1",true);
                k.slika = Image.FromFile("struja.png");
                grana.komponente.Add(k);
                grana.brojkom++;
                osveziListbox();
            }
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
    }
}
