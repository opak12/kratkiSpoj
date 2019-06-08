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
using System.Drawing.Drawing2D;

namespace Test
{
    public enum Tip
    {
        naponskiGenerator,
        strujniGenerator,
        Otpornik
    }
    public class Komponenta
    {
        public Cvor frontPolaritet;
        public int id;
        public Tip vrsta;
        public int velicina;
        public string ime;
        public Image slika;
        public Cvor polaritet;
        public decimal snaga;
        public Komponenta()
        {
        }
        public Komponenta(int v, string s)
        {
            snaga = 0;
            velicina = v;
            ime = s;
        }
        public virtual string uString()
        {
            return this.ime + "=" + this.velicina;
        }
        public virtual void namestiSliku(Cvor izvorni, Cvor odredisni)
        {

        }
        public Image rotirajSliku(float ugao, Image slika)
        {
            Bitmap result = new Bitmap(slika.Width, slika.Height);
            Matrix rotate_at_center = new Matrix();
            rotate_at_center.RotateAt(ugao, new PointF(slika.Width / 2f, slika.Height / 2f));
            using (Graphics gr = Graphics.FromImage(result))
            {
                gr.InterpolationMode = InterpolationMode.Low;
                gr.Transform = rotate_at_center;
                gr.DrawImage(slika, 0, 0);
            }
            return result;
        }
    }
}
