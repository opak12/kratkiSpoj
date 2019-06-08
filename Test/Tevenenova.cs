using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Tevenenova : Form
    {
        Sema sema;
        int izvorIndex = -1;
        int odredisteIndex = -1;
        Cvor Izvor = null;
        Cvor Odrediste = null;
        List<Cvor> praviCvorovi = new List<Cvor>();
        Form1 f;
        Sema mali = new Sema();
        decimal Rt = 0;
        decimal Et = 0;
        public Tevenenova
            ()
        {
            InitializeComponent();
            this.ActiveControl = label2;
        }
        public Tevenenova(Sema sema, Form1 forma)
        {
            f = forma;
            this.sema = sema;
            InitializeComponent();
            this.ActiveControl = label2;
            foreach (Cvor c in sema.cvorovi)
            {
                c.TevenenIndexer = -2;
                if (sema.odrediBrojGrana(c, sema.cvorovi) >= 3)
                {
                    listBox1.Items.Add(c.uString());
                    listBox2.Items.Add(c.uString());
                    praviCvorovi.Add(c);
                }
            }

        }

        private void pokreni()
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (praviCvorovi.Count < 2)
            {
                MessageBox.Show("Nacrtano strujno kolo nije pravilno!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
            if (listBox1.SelectedIndex != -1 && listBox2.SelectedIndex != -1 && listBox1.SelectedIndex != listBox2.SelectedIndex)
            {
                mali.grane.Clear();
                mali.cvorovi.Clear();
                foreach (Grana g in sema.grane)
                {
                    g.obradjena = false;
                    g.struja = 0;
                }
                foreach (Cvor c in sema.cvorovi)
                {
                    c.bot = true;
                    c.indexer = -2;
                    c.napon = 0;
                }
                izvorIndex = listBox1.SelectedIndex;
                odredisteIndex = listBox2.SelectedIndex;
                //MessageBox.Show("Moze racuna izmenju " + praviCvorovi[izvorIndex].uString() + praviCvorovi[odredisteIndex].uString());
                Izvor = praviCvorovi[izvorIndex];
                Odrediste = praviCvorovi[odredisteIndex];
                odrediZBusMatricu();
            }
        }

        private void odrediZBusMatricu()
        {
            int brojac = 1;
            sema.izbaciPipkeIOstrva(new ListBox());
            foreach (Cvor c1 in praviCvorovi)
            {
                if (c1 == Izvor)
                {
                    c1.TevenenIndexer = -1;
                }
                else if (c1 == Odrediste)
                {
                    c1.TevenenIndexer = 0;
                }
                else
                {
                    c1.TevenenIndexer = brojac++;
                }
            }
            List<Cvor> tCvorovi = new List<Cvor>();
            foreach (Cvor c1 in praviCvorovi)
            {
                if (c1 == Izvor)
                    tCvorovi.Add(c1);
            }
            foreach (Cvor c1 in praviCvorovi)
            {
                if (c1 == Odrediste)
                    tCvorovi.Add(c1);
            }
            foreach (Cvor c1 in praviCvorovi)
            {
                if (c1 != Izvor && c1 != Odrediste)
                    tCvorovi.Add(c1);
            }
            //IZMENA
            int dimenzija = praviCvorovi.Count - 1;
            decimal[,] matricaAdmitansi = new decimal[dimenzija, dimenzija];
            for (int i = 0; i < dimenzija; i++)
                for (int j = 0; j < dimenzija; j++)
                {
                    matricaAdmitansi[i, j] = 0;
                }
            for (int i = 1; i <= dimenzija; i++)
            {
                foreach (Poteg p in sema.pot)
                {
                    if (p.izvor == tCvorovi[i] || p.odrediste == tCvorovi[i])
                    {
                        matricaAdmitansi[tCvorovi[i].TevenenIndexer, tCvorovi[i].TevenenIndexer] += p.izracunajAdmitansu();
                        if (p.izvor == tCvorovi[i] && p.odrediste.TevenenIndexer != -1)
                        {
                            matricaAdmitansi[tCvorovi[i].TevenenIndexer, p.odrediste.TevenenIndexer] += (-1 * p.izracunajAdmitansu());
                        }
                        if (p.odrediste == tCvorovi[i] && p.izvor.TevenenIndexer != -1)
                        {
                            matricaAdmitansi[tCvorovi[i].TevenenIndexer, p.izvor.TevenenIndexer] += (-1 * p.izracunajAdmitansu());
                        }
                    }
                }
            }
            //IZMENA
            int n = dimenzija;
            decimal c, h;
            decimal[,] prosirenaMatrica = new decimal[dimenzija + 1, dimenzija + 1];
            decimal[,] pocetnoJedinicna = new decimal[dimenzija + 1, dimenzija + 1];
            for (int i = 0; i <= dimenzija; i++)
            {
                for (int j = 0; j <= dimenzija; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        prosirenaMatrica[i, j] = 0;
                        pocetnoJedinicna[i, j] = 0;
                    }
                    else
                    {
                        prosirenaMatrica[i, j] = matricaAdmitansi[i - 1, j - 1];
                        if (i == j)
                            pocetnoJedinicna[i, j] = 1;
                        else
                            pocetnoJedinicna[i, j] = 0;
                    }
                }
            }
            for (int k = 1; k <= n; k++)//POCETAK ALGORITMA.
            {
                h = prosirenaMatrica[k, k];
                for (int i = 1; i <= n; i++)
                {
                    pocetnoJedinicna[k, i] = pocetnoJedinicna[k, i] / h;
                    prosirenaMatrica[k, i] = prosirenaMatrica[k, i] / h;
                }

                for (int p = k + 1; p <= n; p++)
                {
                    c = prosirenaMatrica[p, k];
                    for (int j = 1; j <= n; j++)
                    {

                        pocetnoJedinicna[p, j] = pocetnoJedinicna[p, j] - c * pocetnoJedinicna[k, j];
                        prosirenaMatrica[p, j] = prosirenaMatrica[p, j] - c * prosirenaMatrica[k, j];
                    }
                }
            }
            for (int z = 1; z <= n; z++)
                pocetnoJedinicna[n, z] = pocetnoJedinicna[n, z] / prosirenaMatrica[n, n];
            prosirenaMatrica[n, n] = 1;
            for (int k = 0; k <= n - 2; k++)
                for (int s = k + 1; s <= n - 1; s++)
                {
                    c = prosirenaMatrica[n - s, n - k];
                    for (int t = 1; t <= n; t++)
                    {
                        pocetnoJedinicna[n - s, t] = pocetnoJedinicna[n - s, t] - pocetnoJedinicna[n - k, t] * c;
                        prosirenaMatrica[n - s, t] = prosirenaMatrica[n - s, t] - prosirenaMatrica[n - k, t] * c;
                    }
                }//KRAJ ALGORITMA.
            decimal napon = Odrediste.napon - Izvor.napon;
            f.osveziListBoxove();
            Cvor prvi = new Cvor(Odrediste.id, 8, 1);
            prvi.zaCrtanje = Odrediste.zaCrtanje;
            Cvor drugi = new Cvor(Izvor.id, 8, 11);
            drugi.zaCrtanje = Izvor.zaCrtanje;
            mali.cvorovi.Add(prvi);
            mali.cvorovi.Add(drugi);
            Grana g = new Grana(66,drugi, prvi, 100);
            Komponenta k2 = new Otpornik(100, "Rt");
            Komponenta k1 = new NaponskiGenerator(100, "Et", true);
            k1.polaritet = prvi;
            k1.frontPolaritet = prvi;
            k1.namestiSliku(drugi, prvi);
            k2.namestiSliku(drugi, prvi);
            g.komponente.Add(k2);
            g.komponente.Add(k1);
            g.brojkom = 2;
            mali.grane.Add(g);
            Rt = pocetnoJedinicna[1, 1];
            Et = napon;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", 11);
            foreach (Cvor c in mali.cvorovi)
            {
                e.Graphics.FillEllipse(c.boja, c.x * 20 + 7, c.y * 20 + 7, 5, 5);
                e.Graphics.DrawString(c.zaCrtanje.ToString(), font, c.boja, new Point(c.x * 20 - 4, c.y * 20 - 4));
            }
            foreach (Grana g in mali.grane)
            {
                e.Graphics.DrawLine(g.boja, g.izvor.x * 20 + 9, g.izvor.y * 20 + 9, g.odrediste.x * 20 + 9, g.odrediste.y * 20 + 9);
                for (int i = 0; i < g.komponente.Count; i++)
                {
                    int x = (g.brojkom * (g.izvor.x * 20) + g.odrediste.x * 20) / (g.brojkom + 1) + (((g.odrediste.x - g.izvor.x) * 20) / (g.brojkom + 1)) * i + 7;
                    int y = (g.brojkom * (g.izvor.y * 20) + g.odrediste.y * 20) / (g.brojkom + 1) + (((g.odrediste.y - g.izvor.y) * 20) / (g.brojkom + 1)) * i + 7;
                    e.Graphics.DrawImage(g.komponente[i].slika, x - 15, y - 15, 35, 35);
                    if (i == 0)
                        e.Graphics.DrawString("Rt = " + Rt.ToString("0.000") + " Om", font, Brushes.Black, new Point(x + 30, y - 4));
                    else
                    {
                        e.Graphics.DrawString("Et = " + Et.ToString("0.000") + " V", font, Brushes.Black, new Point(x + 30, y - 4));
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
