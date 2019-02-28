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
    public class Cvor
    {
        public List<Grana> grane;
        public int id;
        public int x;
        public int y;
        public Brush boja;
        public bool bot;
        public Cvor(int a,int x,int y)
        {
            bot = false;
            this.x = x;
            this.y = y;
            id = a;
            grane = new List<Grana>();
            boja = Brushes.Black;
        }
        public string uString()
        {
            string s = "Cvor " + id;
            return s;
        }
    }
}
