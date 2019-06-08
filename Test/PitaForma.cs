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
    public partial class PitaForma : Form
    {
        List<Poteg> listaPotega;
        public PitaForma(List<Poteg> potezi)
        {
            listaPotega = potezi;
            InitializeComponent();
            chart1.Series["s1"].IsValueShownAsLabel = true;
            foreach (Poteg p in listaPotega)
            {
                foreach (Grana g in p.superGrana)
                {
                    foreach (Komponenta k in g.komponente)
                    {
                        if (k.vrsta == Tip.Otpornik)
                        {
                            chart1.Series["s1"].Points.AddXY(k.ime, k.snaga.ToString("0.000"));
                        }
                    }
                }
            }
        }
    }
}
