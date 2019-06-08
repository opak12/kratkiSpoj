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
    public class Grana
    {
        public Cvor izvor;
        public int grana;
        public Cvor odrediste;
        public List<Komponenta> komponente;
        public int brojkom = 0;
        public int ugao;
        public Pen boja;
        public bool obradjena = false;
        public decimal struja;
        public Grana(int id,Cvor i, Cvor o, int ug)
        {
            struja = 0;
            komponente = new List<Komponenta>();
            izvor = i;
            odrediste = o;
            ugao = ug;
            boja = Pens.Black;
            grana = id;
        }
        public string uString()
        {
            string s = "Grana " + izvor.uString() + "-" + odrediste.uString();
            return s;
        }
        public bool isStrujni()
        {
            foreach (Komponenta k in komponente)
            {
                if (k.vrsta == Tip.strujniGenerator)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
