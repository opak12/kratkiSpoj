using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Poteg
    {
        public List<Grana> superGrana;
        public Cvor izvor;
        public Cvor odrediste;
        public float admitansa;
        public decimal struja;
        public bool odradjena;
        public Poteg()
        {
            odradjena = false;
            struja = 0;
            izvor = null;
            odrediste = null;
            admitansa = 10000000;
            superGrana = new List<Grana>();
        }
        public string uString()
        {
            string s = "";
            foreach (Grana g in superGrana)
            {
                s += g.uString();
            }
            return s;
        }
        public decimal izracunajAdmitansu()
        {
            decimal otpornost = 0.001M;
            if (imaStrujni())
            {
                return 0;
            }
            foreach (Grana g in superGrana)
            {
                foreach (Komponenta k in g.komponente)
                {
                    if (k.vrsta == Tip.Otpornik)
                        otpornost += (decimal)k.velicina;
                }
            }
            return 1 / otpornost;
        }
        public bool imaStrujni()
        {
            foreach (Grana g in superGrana)
            {
                foreach (Komponenta k in g.komponente)
                {
                    if (k.vrsta == Tip.strujniGenerator)
                        return true;
                }
            }
            return false;
        }
        public decimal vratiStrujniGen()
        {
            foreach (Grana g in superGrana)
            {
                foreach (Komponenta k in g.komponente)
                {
                    if (k.vrsta == Tip.strujniGenerator)
                        return k.velicina;
                }
            }
            return -1;
        }
        public decimal vratiEkvNaponski()
        {
            decimal ukNapon = 0;
            foreach (Grana g in superGrana)
            {
                foreach (Komponenta k in g.komponente)
                {
                    if (k.vrsta == Tip.naponskiGenerator)
                    {
                        ukNapon += k.velicina;
                    }
                }
            }
            return ukNapon;
        }
        public void namestiIzvorIOdrediste()
        {
            foreach (Grana g in superGrana)
            {
                if (g.odrediste.bot == false)
                {
                    if (izvor == null)
                        izvor = g.odrediste;
                    else
                        odrediste = g.odrediste;
                }
                if (g.izvor.bot == false)
                {
                    if (izvor == null)
                        izvor = g.izvor;
                    else
                        odrediste = g.izvor;
                }
            }
        }
        public string info()
        {
            return "Od cvora "+izvor.zaCrtanje + " ka cvoru " + odrediste.zaCrtanje + " tece stuja od " + struja.ToString("0.000") + " A.";
        }
    }
}
