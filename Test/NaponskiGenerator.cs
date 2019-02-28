using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class NaponskiGenerator : Komponenta
    {
        public bool orijentacija;
        public NaponskiGenerator(int velicina, string ime, bool or) : base(velicina,ime)
        {
            orijentacija = or;
            vrsta = Tip.naponskiGenerator;
        }
        public override string uString()
        {
            return base.uString() + " V";
        }
    }
}
