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
        public int TevenenIndexer=-2;
        public List<Grana> grane;
        public int id;
        public int x;
        public int y;
        public Brush boja;
        public bool bot;
        public int indexer;
        public decimal napon;
        public int zaCrtanje;
        public Cvor(int a, int x, int y)
        {
            napon = 0;
            indexer = -2;
            bot = true;
            this.x = x;
            this.y = y;
            id = a;
            grane = new List<Grana>();
            boja = Brushes.Black;
        }
        public string zaString()
        {
            string s = "Cvor " + zaCrtanje;
            if (bot == false)
                s += " " + napon.ToString("0.000") + " V";
            return s;
        }
        public string uString()
        {
            string s = "Cvor " + zaCrtanje;
            return s;
        }
    }
}
