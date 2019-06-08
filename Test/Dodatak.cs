using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Test
{
    public abstract class Dodatak
    {
        public Point tacka;
        public Color boja;
        public int transparentnost;
        public Dodatak()
        {
        }
        public Dodatak(Point p, Color b, int t)
        {
            this.tacka = p;
            this.boja = b;
            this.transparentnost = t;
        }
        public virtual void nacrtaj(PaintEventArgs e)
        {
        }
    }
}
