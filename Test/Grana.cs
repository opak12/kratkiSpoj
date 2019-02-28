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
       public Cvor odrediste;
        public List<Komponenta> komponente;
        public int brojkom = 0;
        public int ugao;
        public Pen boja;
        public Grana(Cvor i, Cvor o,int ug)
        {
            komponente = new List<Komponenta>();
            izvor = i;
            odrediste = o;
            ugao = ug;
            boja = Pens.Black;
        }
        public string uString()
        {
            string s="Grana " + izvor.uString() + "-" + odrediste.uString();
           /* foreach (Komponenta k in komponente)
            {
                s += k.uString();
            }*/
            return s;
        }
    }
}
