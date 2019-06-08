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
    public class StrujniGenerator : Komponenta
    {
        public bool orijentacija;
        public float ug;

        public StrujniGenerator(int velicina, string ime, bool or)
            : base(velicina, ime)
        {
            orijentacija = or;
            vrsta = Tip.strujniGenerator;
        }
        public override string uString()
        {
            return base.uString() + " A";
        }
        public override void namestiSliku(Cvor izvorni, Cvor odredisni)
        {
            if (frontPolaritet == izvorni)
                slika = slika = rotirajSliku((float)Math.Atan2(odredisni.y - izvorni.y, odredisni.x - izvorni.x) * 57.29577f, Image.FromFile("struja3.png"));
            else  if (frontPolaritet == odredisni)
                slika = slika = rotirajSliku((float)Math.Atan2(odredisni.y - izvorni.y, odredisni.x - izvorni.x) * 57.29577f, Image.FromFile("struja1.png"));
        }
    }
}
