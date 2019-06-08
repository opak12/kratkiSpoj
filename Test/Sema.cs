using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows;


namespace Test
{
    public class Sema
    {      
        private int idseme;
        public int Idseme { get { return idseme; } set { idseme = value; } }
        public List<Cvor> cvorovi;
        public List<Grana> grane;
        public List<Poteg> pot;
        public Sema()
        {
            cvorovi = new List<Cvor>();
            grane = new List<Grana>();
        }
        public void obrisiGranu(Grana grana)
        {
            foreach (Cvor c in cvorovi)
            {
                for (int i = 0; i < c.grane.Count; i++)
                {
                    if (c.grane[i].izvor == grana.izvor && c.grane[i].odrediste == grana.odrediste)
                    {
                        c.grane.RemoveAt(i);
                        i--;
                        grane.Remove(grana);
                        return;
                    }
                }
            }
        }
        public void obrisiGraneCvora(Cvor cvor)
        {
            foreach (Cvor c in cvorovi)
            {
                if (c != cvor)
                {
                    for (int i = 0; i < c.grane.Count; i++)
                    {
                        if (c.grane[i].odrediste == cvor)//brise grane cije je odrediste obrisan cvor;
                        {
                            grane.Remove(c.grane[i]);
                            c.grane.Remove(c.grane[i]);
                            i--;
                        }
                    }
                }
            }
            foreach (Grana g in cvor.grane)//brise grane tog cvora;
            {
                grane.Remove(g);
            }
            cvorovi.Remove(cvor);
        }
        public void reset()
        {
            cvorovi.Clear();
            grane.Clear();
        }
        public void izbaciPipkeIOstrva(ListBox lb)
        {
            pot = new List<Poteg>();
            List<Cvor> backCvorovi = new List<Cvor>();
            List<Grana> backGrane = new List<Grana>();
            List<Cvor> protoCvorovi = new List<Cvor>();
            List<Grana> protoGrane = new List<Grana>();
            foreach (Cvor c in cvorovi)
            {
                c.bot = false;
                protoCvorovi.Add(c);
            }
            bool rekurzija = true;
            while (rekurzija)
            {
                rekurzija = false;
                for (int i = 0; i < protoCvorovi.Count; i++)
                {
                    if (odrediBrojGrana(protoCvorovi[i], protoCvorovi) < 2)
                    {
                        rekurzija = true;
                        protoCvorovi[i].bot = true;
                        lb.Items.Add("Warning : Cvor "+protoCvorovi[i].zaCrtanje+" visi, te se on nece uzeti u obzir pri izracunavanju!");
                        protoCvorovi.RemoveAt(i);
                        i--;
                    }
                }
            }
            foreach (Cvor c in protoCvorovi)
            {
                int brojGrana = odrediBrojGrana(c, protoCvorovi);
                if (brojGrana <= 2)
                {
                    c.bot = true;
                    backCvorovi.Add(c);
                }
                else if (brojGrana > 2)
                {
                    c.bot = false;
                    backCvorovi.Add(c);
                }

            }
            foreach (Grana g in grane)
            {
                if (odrediBrojGrana(g.izvor, protoCvorovi) >= 2 && odrediBrojGrana(g.odrediste, protoCvorovi) >= 2)
                {
                    backGrane.Add(g);
                }
            }
            int brojBackCvorova = 0;
            int brojCvorova = 0;
            foreach (Cvor c in backCvorovi)
            {
                brojCvorova++;
                if (c.bot == false)
                    brojBackCvorova++;
            }
            if (brojCvorova == 0)
            {
                lb.Items.Add("Error : Greska! Nema Struje, proverite da li je kolo zatvoreno!!!");
                return;
            }
            else if (brojBackCvorova == 0)
            {
                resiKonturu(backGrane, backCvorovi);
                return;
            }
            foreach (Cvor c in backCvorovi)
            {
                if (c.bot == true)
                {
                    foreach (Grana g in backGrane)
                    {
                        if (g.izvor == c || g.odrediste == c)
                        {
                            foreach (Komponenta k in g.komponente)
                            {
                                if ((k.vrsta == Tip.strujniGenerator || k.vrsta == Tip.naponskiGenerator) && k.polaritet == c)
                                {
                                    Grana pomGrana = g;
                                    Cvor pomCvor = c;
                                    while (k.polaritet.bot == true)
                                    {
                                        foreach (Grana g1 in backGrane)
                                        {

                                            if ((g1.izvor == pomCvor || g1.odrediste == pomCvor) && g1 != pomGrana)
                                            {
                                                if (g1.izvor != pomCvor)
                                                    pomCvor = g1.izvor;
                                                else
                                                    pomCvor = g1.odrediste;
                                                k.polaritet = pomCvor;
                                                pomGrana = g1;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            kondenzuj(backCvorovi, backGrane);
        }

        private void kondenzuj(List<Cvor> cvori, List<Grana> granee)
        {
            List<Poteg> potezi = new List<Poteg>();
            for (int i = 0; i < granee.Count; i++)
            {
                if (granee[i].izvor.bot == false && granee[i].odrediste.bot == false)
                {
                    Poteg noviPoteg = new Poteg();
                    noviPoteg.superGrana.Add(granee[i]);
                    potezi.Add(noviPoteg);
                    granee.RemoveAt(i);
                }
            }
            bool flag = true;
            foreach (Grana g in granee)
            {
                flag = true;
                Poteg noviPoteg = null;
                if (g.obradjena == false)
                {
                    noviPoteg = new Poteg();
                    noviPoteg.superGrana.Add(g);
                    g.obradjena = true;
                }
                while (flag && noviPoteg != null)
                {
                gore:
                    flag = false;
                    for (int i = 0; i < noviPoteg.superGrana.Count; i++)
                    {
                        foreach (Grana g1 in granee)
                        {
                            if (noviPoteg.superGrana[i] != g1 && imajuZajednickoTeme(noviPoteg.superGrana[i], g1))
                            {
                                noviPoteg.superGrana.Add(g1);
                                g1.obradjena = true;
                                flag = true;
                                goto gore;
                            }
                        }
                    }
                }
                if (noviPoteg != null)
                    potezi.Add(noviPoteg);
            }
            foreach (Poteg p in potezi)
            {
                p.izracunajAdmitansu();
                p.namestiIzvorIOdrediste();
            }
            List<Cvor> finalniCvorovi = new List<Cvor>();
            int brojac = -1;
            foreach (Cvor c in cvori)
            {
                if (c.bot == false)
                {
                    c.indexer = brojac++;
                    finalniCvorovi.Add(c);
                }
            }
            napraviMatricuAdmitansi(finalniCvorovi, potezi);
        }

        private void napraviMatricuAdmitansi(List<Cvor> finalniCvorovi, List<Poteg> potezi)
        {
            foreach (Cvor c in finalniCvorovi)
            {
                bool da_li_je_nelegalan = true;
                foreach (Poteg p in potezi)
                {
                    if (p.izvor == c || p.odrediste == c)
                    {
                        if (!p.imaStrujni())
                        {
                            da_li_je_nelegalan = false;
                        }
                    }
                }
                if (da_li_je_nelegalan == true)
                {
                    MessageBox.Show("Uneli ste cvor koji za svaku od svojih grana ima prikacen Strujni Generator.","Greska",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            int dimenzija = finalniCvorovi.Count - 1;
            decimal[,] matricaAdmitansi = new decimal[dimenzija, dimenzija];
            for (int i = 0; i < dimenzija; i++)
                for (int j = 0; j < dimenzija; j++)
                {
                    matricaAdmitansi[i, j] = 0;
                }
            for (int i = 1; i <= dimenzija; i++)
            {
                foreach (Poteg p in potezi)
                {
                    if (p.izvor == finalniCvorovi[i] || p.odrediste == finalniCvorovi[i])
                    {
                        matricaAdmitansi[finalniCvorovi[i].indexer, finalniCvorovi[i].indexer] += p.izracunajAdmitansu();
                        if (p.izvor == finalniCvorovi[i] && p.odrediste.indexer != -1)
                        {
                            matricaAdmitansi[finalniCvorovi[i].indexer, p.odrediste.indexer] += (-1 * p.izracunajAdmitansu());
                        }
                        if (p.odrediste == finalniCvorovi[i] && p.izvor.indexer != -1)
                        {
                            matricaAdmitansi[finalniCvorovi[i].indexer, p.izvor.indexer] += (-1 * p.izracunajAdmitansu());
                        }
                    }
                }
            }
            decimal[] injektiraneStruje = new decimal[dimenzija];
            for (int i = 0; i < dimenzija; i++)
                injektiraneStruje[i] = 0;
            for (int i = 1; i <= dimenzija; i++)
            {
                foreach (Poteg p in potezi)
                {
                    decimal jEkv = 0;
                    decimal eEkv = 0;
                    if (p.izvor == finalniCvorovi[i] || p.odrediste == finalniCvorovi[i])
                    {
                        foreach (Grana g in p.superGrana)
                        {

                            foreach (Komponenta k in g.komponente)
                            {
                                if (k.vrsta == Tip.strujniGenerator)
                                {
                                    if (k.polaritet == finalniCvorovi[i])
                                    {
                                        jEkv += k.velicina;
                                    }
                                    else
                                    {
                                        jEkv -= k.velicina;
                                    }
                                }
                                else if (k.vrsta == Tip.naponskiGenerator)
                                {
                                    if (k.polaritet == finalniCvorovi[i])
                                    {
                                        eEkv += k.velicina;
                                    }
                                    else
                                    {
                                        eEkv -= k.velicina;
                                    }
                                }
                            }

                        }
                        injektiraneStruje[i - 1] += jEkv + (eEkv * p.izracunajAdmitansu());
                    }
                }
            }
            stvoriInverznuMatricu(matricaAdmitansi, injektiraneStruje, dimenzija);
            foreach (Poteg p in potezi)
            {
                p.struja = 0;
                p.odradjena = false;
            }
            foreach (Poteg p in potezi)//??????STRUJE
            {
                foreach (Grana g in p.superGrana)
                {
                    foreach (Komponenta k in g.komponente)
                    {
                        if (k.vrsta == Tip.strujniGenerator)
                        {
                            if (p.izvor == k.polaritet)
                            {
                                p.struja = -1 * k.velicina;
                                p.odradjena = true;
                            }
                            else if (p.odrediste == k.polaritet)
                            {
                                p.struja = k.velicina;
                                p.odradjena = true;
                            }
                        }
                    }
                }
            }
            for (int i = 1; i < finalniCvorovi.Count; i++)
            {
                foreach (Poteg p in potezi)
                {

                    if ((p.izvor == finalniCvorovi[i] || p.odrediste == finalniCvorovi[i]) && p.odradjena == false)
                    {
                        decimal eEkv1 = 0;
                        foreach (Grana g in p.superGrana)
                        {
                            foreach (Komponenta k in g.komponente)
                            {
                                if (k.vrsta == Tip.naponskiGenerator)
                                {
                                    if (k.polaritet == p.izvor)
                                    {
                                        eEkv1 += -1 * k.velicina;
                                    }
                                    else
                                    {
                                        eEkv1 += k.velicina;
                                    }
                                }
                            }
                        }
                        p.struja = (p.izvor.napon - p.odrediste.napon + eEkv1) * p.izracunajAdmitansu();
                        p.odradjena = true;
                    }
                }
            }
            foreach (Poteg p in potezi)
            {
                foreach (Grana g in p.superGrana)
                {
                    foreach (Komponenta k in g.komponente)
                    {
                        if (k.vrsta == Tip.Otpornik)
                        {
                            k.snaga = p.struja * p.struja * k.velicina;
                        }
                    }
                }
            }
            pot = potezi;
        }

        private void stvoriInverznuMatricu(decimal[,] matricaAdmitansi, decimal[] injektiraneStruje, int dimenzija)
        {
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
            decimal[] potencijalCvorova = new decimal[dimenzija];
            for (int i = 0; i < dimenzija; i++)
            {
                potencijalCvorova[i] = 0;
            }
            for (int i = 1; i <= dimenzija; i++)
            {
                for (int j = 1; j <= dimenzija; j++)
                {
                    potencijalCvorova[i - 1] += pocetnoJedinicna[i, j] * injektiraneStruje[j - 1];
                }
            }
            for (int i = 0; i < dimenzija; i++)
            {
                foreach (Cvor cv in cvorovi)
                {
                    if (cv.indexer == i)
                    {
                        cv.napon = potencijalCvorova[i];
                        break;
                    }
                }
            }
        }

        private bool imajuZajednickoTeme(Grana g, Grana g1)
        {
            if (g1.obradjena == false && ((g.izvor == g1.odrediste && g.izvor.bot == true) || (g.odrediste == g1.izvor && g.odrediste.bot == true) || (g.odrediste == g1.odrediste && g.odrediste.bot == true) || (g.izvor == g1.izvor && g.izvor.bot == true)))
                return true;
            else
                return false;
        }

        private void resiKonturu(List<Grana> backGrane, List<Cvor> backCvorovi)
        {
            List<Poteg> lista = new List<Poteg>();
            Poteg noviPoteg = new Poteg();
            foreach (Grana g in backGrane)
            {
                noviPoteg.superGrana.Add(g);
            }
            noviPoteg.izvor = backCvorovi[0];
            backCvorovi[0].bot = false;
            noviPoteg.odrediste = backCvorovi[0];
            foreach (Grana g in noviPoteg.superGrana)
            {
                foreach (Komponenta k in g.komponente)
                {
                    if (k.vrsta == Tip.strujniGenerator)
                    {

                        noviPoteg.struja = k.velicina;
                        goto dole;
                    }
                }
            }
            decimal eEkv = 0;
            Cvor pocCvor = backCvorovi[0];
            Cvor tmpCvor = pocCvor;
            Grana pocGrana = null;
            foreach (Grana g in backGrane)
            {
                if (g.izvor == pocCvor || g.odrediste == pocCvor)
                {
                    pocGrana = g;
                    break;
                }
            }
            bool f = false;
            Grana tmpGrana = pocGrana;
            while (f == false)
            {
                if (tmpGrana.izvor == tmpCvor)
                {
                    foreach (Komponenta k in tmpGrana.komponente)
                    {
                        if (k.vrsta == Tip.naponskiGenerator)
                        {
                            if (k.polaritet == tmpCvor)
                            {
                                eEkv += k.velicina;
                            }
                            else
                            {
                                eEkv -= k.velicina;
                            }
                        }
                    }
                    tmpCvor = tmpGrana.odrediste;
                    if (tmpCvor == pocCvor)
                        f = true;
                    foreach (Grana g in backGrane)
                    {
                        if ((g.izvor == tmpCvor || g.odrediste == tmpCvor) && g != tmpGrana)
                        {
                            tmpGrana = g;
                            break;
                        }
                    }
                }
                if (tmpGrana.odrediste == tmpCvor)
                {
                    foreach (Komponenta k in tmpGrana.komponente)
                    {
                        if (k.vrsta == Tip.naponskiGenerator)
                        {
                            if (k.polaritet == tmpCvor)
                            {
                                eEkv += k.velicina;
                            }
                            else
                            {
                                eEkv -= k.velicina;
                            }
                        }
                    }
                    tmpCvor = tmpGrana.izvor;
                    if (tmpCvor == pocCvor)
                        f = true;
                    foreach (Grana g in backGrane)
                    {
                        if ((g.izvor == tmpCvor || g.odrediste == tmpCvor) && g != tmpGrana)
                        {
                            tmpGrana = g;
                            break;
                        }
                    }
                }
            }
            noviPoteg.struja = eEkv * noviPoteg.izracunajAdmitansu();
        dole:
            foreach (Grana g in noviPoteg.superGrana)
            {
                foreach (Komponenta k in g.komponente)
                {
                    if (k.vrsta == Tip.Otpornik)
                        k.snaga = noviPoteg.struja * noviPoteg.struja * k.velicina;
                }
            }
            lista.Add(noviPoteg);
            pot = lista;
        }
        public int odrediBrojGrana(Cvor cvor, List<Cvor> cvori)
        {
            int brojGrana = 0;
            foreach (Cvor c in cvori)
            {
                if (c == cvor)
                {
                    foreach (Grana g in c.grane)
                    {
                        if (da_li_pripada(g.odrediste, cvori))
                            brojGrana++;
                    }
                }
                else
                {
                    foreach (Grana g in c.grane)
                    {
                        if (g.odrediste == cvor)
                        {
                            brojGrana++;
                        }
                    }
                }
            }
            return brojGrana;
        }

        private bool da_li_pripada(Cvor cvor, List<Cvor> cvori)
        {
            foreach (Cvor c in cvori)
            {
                if (cvor == c)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
