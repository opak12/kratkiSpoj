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
    public partial class RandomForma : Form
    {
        Sema sema;
        int idBrojac,idGra;
        Prenos p;
        public RandomForma(Sema sema,int idBrojac,int idGrana,Prenos p)
        {
            this.p = p;
            this.sema = sema;
            this.idBrojac = idBrojac;
            idGra = idGrana;
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int brojac_za_crtanje=1;
            int brcvorova = (int)numericUpDown1.Value;
            int crt = 0;
            if (brcvorova == 1)
                crt = 1;
            if (brcvorova == 2 || brcvorova==3)
                crt = 2;
            if (brcvorova == 4)
                crt = 3;
            int y=7;
            for(int i=0;i<2;i++)
            {
                int x=12;
                for (int j = 0; j < crt + 1; j++)
                {
                    Cvor c = new Cvor(idBrojac++, x, y);
                        sema.cvorovi.Add(c);
                        c.zaCrtanje = brojac_za_crtanje++;
                    x+=10;
                }
                y += 13;
            }
            int numNodes = sema.cvorovi.Count;
            for (int i = 0; i < numNodes / 2; i++)
            {
                Grana g=null;
                if (i != (numNodes/2 - 1))
                {
                    g = new Grana(idGra++, sema.cvorovi[i], sema.cvorovi[i + 1], 0);
                    sema.cvorovi[i].grane.Add(g);
                    sema.grane.Add(g);
                }
            }
            for (int i = numNodes / 2; i < numNodes; i++)
            {
                Grana g = null;
                if (i != (numNodes - 1))
                {
                    g = new Grana(idGra++, sema.cvorovi[i], sema.cvorovi[i + 1], 0);
                    sema.cvorovi[i].grane.Add(g);
                    sema.grane.Add(g);
                }
            }
            for (int i = 0; i < numNodes / 2; i++)
            {
                Grana g = new Grana(idGra++, sema.cvorovi[i], sema.cvorovi[i + numNodes / 2], 0);
                sema.cvorovi[i].grane.Add(g);
                sema.grane.Add(g);
            }
            int rand = r.Next(10);
            if (brcvorova == 3)
            {
                int iz=0;
                int odr=4;
                if (rand < 2)
                {
                    iz = 0;
                    odr = 4;
                }
                else if (rand < 4)
                {
                    iz = 1;
                    odr = 3;
                }
                else if (rand < 6)
                {
                    iz = 1;
                    odr = 5;
                }
                else
                {
                    iz = 2;
                    odr = 4;
                }
                Grana g = new Grana(idGra++, sema.cvorovi[iz], sema.cvorovi[odr], 0);
                sema.cvorovi[iz].grane.Add(g);
                sema.grane.Add(g);
            }
            if (brcvorova == 4)
            {
                int iz = 0;
                int odr = 4;
                if (rand < 3)
                {
                    iz = 1;
                    odr = 6;
                }
                else if (rand < 6)
                {
                    iz = 2;
                    odr = 5;
                }
                else
                {
                    iz = -1;
                    odr = -1;
                }
                if (iz != -1 && odr != -1)
                {
                    Grana g = new Grana(idGra++, sema.cvorovi[iz], sema.cvorovi[odr], 0);
                    sema.cvorovi[iz].grane.Add(g);
                    sema.grane.Add(g);
                }
            }
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
            sema.izbaciPipkeIOstrva(new ListBox());
            List<Poteg> potezi =new List<Poteg>();
            potezi = sema.pot;
            int brojacOtp = 1, brojacStrujni = 1, brojacNapon = 1;
            if (potezi.Count == 1)
            {
                if (r.Next(8) == 0)
                {
                    Komponenta k = new StrujniGenerator(r.Next(6) + 1, "J" + brojacStrujni.ToString(), true);
                    brojacStrujni++;
                    if (r.Next(2) == 0)
                    {
                        k.polaritet = potezi[0].superGrana[0].izvor;
                        k.frontPolaritet = potezi[0].superGrana[0].izvor;
                    }
                    else
                    {
                        k.polaritet = potezi[0].superGrana[0].odrediste;
                        k.frontPolaritet = potezi[0].superGrana[0].odrediste;
                    }
                    k.namestiSliku(potezi[0].superGrana[0].izvor,potezi[0].superGrana[0].odrediste);
                    potezi[0].superGrana[0].komponente.Add(k);
                    potezi[0].superGrana[0].brojkom++;
                }
                foreach (Grana g in potezi[0].superGrana)
                {
                    if (r.Next(10) != 0)
                    {
                        int koliko = r.Next(10);
                        if (koliko < 7)
                        {
                            koliko = 1;
                        }
                        else 
                        {
                            koliko = 2;
                        }
                        for (int i = 0; i < koliko; i++)
                        {
                            int ran = r.Next(100);
                            if (ran < 62)
                            {
                                Komponenta k = new Otpornik(r.Next(10) + 1, "R" + brojacOtp.ToString());
                                k.namestiSliku(g.izvor, g.odrediste);
                                brojacOtp++;
                                g.komponente.Add(k);
                                g.brojkom++;
                            }
                            else 
                            {
                                Komponenta k = new NaponskiGenerator(r.Next(10) + 1, "E" + brojacNapon.ToString(), true);
                                brojacNapon++;
                                if (r.Next(2) == 0)
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
                    }
                }
                this.p.godina = idBrojac;
                this.p.tezina = idGra;
                this.Close();
                return;
            }
            foreach (Poteg p in potezi)
            {
                if (p.superGrana.Count == 1)
                {
                    int koliko = r.Next(10);
                    if (koliko < 7)
                    {
                        koliko = 1;
                    }
                    else
                    {
                        koliko = 2;
                    }
                    for (int i = 0; i < koliko; i++)
                    {
                        int ran = r.Next(100);
                        if (ran < 50)
                        {
                            Komponenta k = new Otpornik(r.Next(10) + 1, "R" + brojacOtp.ToString());
                            k.namestiSliku(p.superGrana[0].izvor, p.superGrana[0].odrediste);
                            brojacOtp++;
                            p.superGrana[0].komponente.Add(k);
                            p.superGrana[0].brojkom++;
                        }
                        else if (ran < 84)
                        {
                            Komponenta k = new NaponskiGenerator(r.Next(10) + 1, "E" + brojacNapon.ToString(), true);
                            brojacNapon++;
                            if (r.Next(2) == 0)
                            {
                                k.polaritet = p.superGrana[0].izvor;
                                k.frontPolaritet = p.superGrana[0].izvor;
                            }
                            else
                            {
                                k.polaritet = p.superGrana[0].odrediste;
                                k.frontPolaritet = p.superGrana[0].odrediste;
                            }
                            k.namestiSliku(p.superGrana[0].izvor, p.superGrana[0].odrediste);
                            p.superGrana[0].komponente.Add(k);
                            p.superGrana[0].brojkom++;
                        }
                        else if (p.superGrana[0].isStrujni()==false)
                        {
                            Komponenta k = new StrujniGenerator(r.Next(6) + 1, "J" + brojacStrujni.ToString(), true);
                            brojacStrujni++;
                            if (r.Next(2) == 0)
                            {
                                k.polaritet = p.superGrana[0].izvor;
                                k.frontPolaritet = p.superGrana[0].izvor;
                            }
                            else
                            {
                                k.polaritet = p.superGrana[0].odrediste;
                                k.frontPolaritet = p.superGrana[0].odrediste;
                            }
                            k.namestiSliku(p.superGrana[0].izvor, p.superGrana[0].odrediste);
                            p.superGrana[0].komponente.Add(k);
                            p.superGrana[0].brojkom++;
                        }
                    }
                }
                else
                {
                    for (int ee = 0; ee < p.superGrana.Count; ee++)
                    {
                        if (ee == 1)
                        {
                            int koliko = r.Next(10);
                            if (koliko < 7)
                            {
                                koliko = 1;
                            }
                            else
                            {
                                koliko = 2;
                            }
                            for (int i = 0; i < koliko; i++)
                            {
                                int ran = r.Next(100);
                                if (ran < 50)
                                {
                                    Komponenta k = new Otpornik(r.Next(10) + 1, "R" + brojacOtp.ToString());
                                    k.namestiSliku(p.superGrana[1].izvor, p.superGrana[1].odrediste);
                                    brojacOtp++;
                                    p.superGrana[1].komponente.Add(k);
                                    p.superGrana[1].brojkom++;
                                }
                                else if (ran < 84)
                                {
                                    Komponenta k = new NaponskiGenerator(r.Next(10) + 1, "E" + brojacNapon.ToString(), true);
                                    brojacNapon++;
                                    if (r.Next(2) == 0)
                                    {
                                        k.polaritet = p.superGrana[1].izvor;
                                        k.frontPolaritet = p.superGrana[1].izvor;
                                    }
                                    else
                                    {
                                        k.polaritet = p.superGrana[1].odrediste;
                                        k.frontPolaritet = p.superGrana[1].odrediste;
                                    }
                                    k.namestiSliku(p.superGrana[1].izvor, p.superGrana[1].odrediste);
                                    p.superGrana[1].komponente.Add(k);
                                    p.superGrana[1].brojkom++;
                                }
                                else if(p.superGrana[1].isStrujni()==false)
                                {
                                    Komponenta k = new StrujniGenerator(r.Next(6) + 1, "J" + brojacStrujni.ToString(), true);
                                    brojacStrujni++;
                                    if (r.Next(2) == 0)
                                    {
                                        k.polaritet = p.superGrana[1].izvor;
                                        k.frontPolaritet = p.superGrana[1].izvor;
                                    }
                                    else
                                    {
                                        k.polaritet = p.superGrana[1].odrediste;
                                        k.frontPolaritet = p.superGrana[1].odrediste;
                                    }
                                    k.namestiSliku(p.superGrana[1].izvor, p.superGrana[1].odrediste);
                                    p.superGrana[1].komponente.Add(k);
                                    p.superGrana[1].brojkom++;
                                }
                            }
                        }
                        else 
                        {
                            if (r.Next(3) == 0)
                            {
                                Komponenta k = new Otpornik(r.Next(10) + 1, "R" + brojacOtp.ToString());
                                k.namestiSliku(p.superGrana[ee].izvor, p.superGrana[ee].odrediste);
                                brojacOtp++;
                                p.superGrana[ee].komponente.Add(k);
                                p.superGrana[ee].brojkom++;
                            }
                        }
                    }
                }
            }
            this.p.godina = idBrojac;
            this.p.tezina = idGra;
                this.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.p.godina = idBrojac;
            this.p.tezina = idGra;
            this.Close();
        }
    }
}
