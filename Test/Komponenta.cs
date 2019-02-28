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
    public enum Tip
    {
        naponskiGenerator,
        strujniGenerator,
        Otpornik
    }
    public class Komponenta
    {
        public Tip vrsta;
        public int velicina;
        public string ime;
        public Image slika;
        public Komponenta()
        {
 
        }
        public Komponenta(int v,string s)
        {
            velicina = v;
            ime = s;
        }
        public virtual string uString()
        {
            return this.ime + "=" + this.velicina;
        }
    }
}
