using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public class Sprej : Dodatak
    {
        public int debljina;
        public Sprej(Point p, Color c, int deb, int transpa)
            : base(p, c, transpa)
        {
            debljina = deb;
        }
        public override void nacrtaj(PaintEventArgs e)
        {
            Color novaBoja = Color.FromArgb(transparentnost, boja);
            Brush b = new SolidBrush(novaBoja);
            e.Graphics.FillEllipse(b, tacka.X, tacka.Y, debljina, debljina);
        }
    }
}
