using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class StrujniGenerator : Komponenta
    {
        public bool orijentacija;
        public StrujniGenerator(int velicina, string ime, bool or) : base(velicina,ime)
        {
            orijentacija = or;
            vrsta = Tip.strujniGenerator;
        }
        public override string uString()
        {
            return base.uString() + " A";
        }
    }
}
