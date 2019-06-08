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
    public class Komentar : Dodatak
    {
        public string text;
        public Font f;
        public Komentar(string text, Font f, Point t, Color boja, int transpa)
            : base(t, boja, transpa)
        {
            this.text = text;
            this.f = f;
        }
        public override void nacrtaj(PaintEventArgs e)
        {
            Color novaBoja = Color.FromArgb(transparentnost, boja);
            Brush b = new SolidBrush(novaBoja);
            e.Graphics.DrawString(text, f, b, new Point(tacka.X, tacka.Y));
        }
    }
}
