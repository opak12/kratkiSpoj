using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Test
{
    public partial class SaveForma : Form
    {
        private Prenos p;
        private string user;
        public SaveForma(string korisnicko,Prenos s)
        {
            p = s;
            InitializeComponent();
            this.user = korisnicko;
            textBox1.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.godina = -1;
            p.tezina = -1;
            p.ime = "";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "" || textBox1.Text != " ") && comboBox2.SelectedIndex != -1)
            {
                //Kod za sacuvaj();
                string godina = "";
                int tezina = 0;
                int sel = comboBox1.SelectedIndex;
                switch (sel)
                {
                    case 0:
                        {
                            godina = "2013";
                            break;
                        }
                    case 1:
                        {
                            godina = "2014";
                            break;
                        }
                    case 2:
                        {
                            godina = "2015";
                            break;
                        }
                    case 3:
                        {
                            godina = "2016";
                            break;
                        }
                    case 4:
                        {
                            godina = "2017";
                            break;
                        }
                    case 5:
                        {
                            godina = "2018";
                            break;
                        }
                    default: { godina = "2019"; break; }
                }
                sel = comboBox2.SelectedIndex;
                switch (sel)
                {
                    case 0:
                        {
                            tezina = 6;
                            break;
                        }
                    case 1:
                        {
                            tezina = 7;
                            break;
                        }
                    case 2:
                        {
                            tezina = 8;
                            break;
                        }
                    case 3:
                        {
                            tezina = 9;
                            break;
                        }
                    case 4:
                        {
                            tezina = 10;
                            break;
                        }
                    default: { tezina = 8; ; break; }
                }
                p.godina = Int32.Parse(godina);
                p.tezina = tezina;
                p.ime = textBox1.Text;
                this.Close();
            }
            else {
                MessageBox.Show("Nije uneto ime seme!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }
    }
}
