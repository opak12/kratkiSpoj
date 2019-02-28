using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
   public class Otpornik : Komponenta
    {
       public Otpornik(int velicina,string ime) : base(velicina,ime)
       {     
       }
       public override string uString()
       {
           return base.uString()+" Om";
       }
    }
}
