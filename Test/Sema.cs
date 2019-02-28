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
        public List<Cvor> cvorovi;
        public List<Grana> grane;
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
                        return;
                    }
                }
            }
            grane.Remove(grana);
        }
        public void obrisiGraneCvora(Cvor cvor)
        {
            foreach (Cvor c in cvorovi)
            {
                if (c != cvor)
                {
                    for(int i=0;i<c.grane.Count;i++)
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
            for (int i = 0; i < cvorovi.Count; i++)
            {
                obrisiGraneCvora(cvorovi[i]);
            }
           cvorovi.Clear();
        }
        public void izbaciPipkeIOstrva()
        {
            List<Cvor> backCvorovi = new List<Cvor>();
            List<Grana> backGrane = new List<Grana>();
            List<Cvor> protoCvorovi = new List<Cvor>();
            List<Grana> protoGrane = new List<Grana>();
            
            foreach (Cvor c in cvorovi)
            {
                c.bot = false;
                protoCvorovi.Add(c);
            }
            bool rekurzija=true;
            while (rekurzija)
            {
                rekurzija = false;
                for (int i = 0; i < protoCvorovi.Count;i++ )
                {
                    if (odrediBrojGrana(protoCvorovi[i], protoCvorovi) < 2)
                    {
                        rekurzija = true;
                        protoCvorovi.RemoveAt(i);
                        i--;
                    }
                }
            }
            foreach (Cvor c in protoCvorovi)
            {
                //MessageBox.Show(odrediBrojGrana(c).ToString());
                int brojGrana = odrediBrojGrana(c,protoCvorovi);
                if (brojGrana == 2)
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
                if (odrediBrojGrana(g.izvor,protoCvorovi) >= 2 && odrediBrojGrana(g.odrediste,protoCvorovi) >= 2)
                {
                    backGrane.Add(g);
                }
            }
            string s = "";
            int brojBackCvorova=0;
            int brojCvorova = 0;
            foreach (Cvor c in backCvorovi)
            {
                s += c.uString()+".\n";
                brojCvorova++;
                if (c.bot == false)
                    brojBackCvorova++;
            }
            s += "Broj Pravih Cvorova je " + brojBackCvorova.ToString();
            if (brojCvorova == 0)
            {
                MessageBox.Show("Greska! nema Struje");
            }
            else if (brojBackCvorova == 0)
            {
                resiKonturu();
            }
            MessageBox.Show(s);
            s = "";
            foreach (Grana g in backGrane)
            {
                s += g.uString()+".\n";
            }
            MessageBox.Show(s);
            kondenzuj(backCvorovi,backGrane);
        }

        private void kondenzuj(List<Cvor> cvori,List<Grana> granee)
        {
            foreach (Grana g in granee)
            {
 
            }
        }

        private void resiKonturu()
        {

            MessageBox.Show("Kontura!");
        }
        public int odrediBrojGrana(Cvor cvor,List<Cvor> cvori)
        {
            int brojGrana = 0;
            foreach (Cvor c in cvori)
            {
                if (c == cvor)
                {
                    foreach (Grana g in c.grane)
                    {
                        if(da_li_pripada(g.odrediste,cvori))
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
