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
    public class Otpornik : Komponenta
    {
        public Otpornik(int velicina, string ime)
            : base(velicina, ime)
        {
            vrsta = Tip.Otpornik;
        }
        public override string uString()
        {
            return base.uString() + " Om";
        }
        public override void namestiSliku(Cvor izvorni, Cvor odredisni)
        {
            slika = rotirajSliku((float)Math.Atan2(odredisni.y - izvorni.y, odredisni.x - izvorni.x) * 57.29577f, Image.FromFile("opt6.png"));
        }
    }
}
