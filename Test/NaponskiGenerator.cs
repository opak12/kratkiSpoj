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
    public class NaponskiGenerator : Komponenta
    {
        public bool orijentacija;
        public float ug = 0;
        public NaponskiGenerator(int velicina, string ime, bool or)
            : base(velicina, ime)
        {
            orijentacija = or;
            vrsta = Tip.naponskiGenerator;
        }
        public override string uString()
        {
            return base.uString() + " V";
        }
        public override void namestiSliku(Cvor izvorni, Cvor odredisni)
        {
            if (frontPolaritet == izvorni)
                slika = slika = rotirajSliku((float)Math.Atan2(odredisni.y - izvorni.y, odredisni.x - izvorni.x) * 57.29577f, Image.FromFile("napon6.png"));
            else if (frontPolaritet == odredisni)
                slika = slika = rotirajSliku((float)Math.Atan2(odredisni.y - izvorni.y, odredisni.x - izvorni.x) * 57.29577f, Image.FromFile("napon7.png"));
        }
    }
}
