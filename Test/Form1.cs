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
    public partial class Form1 : Form
    {
        Cvor izvorni;
        Cvor odredisni;
        int idBrojac = 1;
        Sema sema;
        //Sema backEndSema;
        public Form1()
        {
            sema = new Sema();
           // backEndSema = new Sema();
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point p1 = this.Location;//tacka za formu;
           Point p= Cursor.Position;//u odnosu na pocetak ekrana;
           p.X -= (p1.X + pictureBox1.Location.X+8);
           p.Y -= (p1.Y+pictureBox1.Location.Y+30);
           bool da_li_ima = false;
           foreach (Cvor c in sema.cvorovi)
           {
               if (c.x == p.X / 20 && c.y == p.Y / 20)
               {
                   da_li_ima = true;
                   if (izvorni == null)
                   {
                       izvorni = c;
                   }
                   else
                   {
                       odredisni = c;
                   }
                   break;
               }
           }
           if (da_li_ima == false)
           {
                MessageBox.Show((p.X/20).ToString()+", "+(p.Y/20).ToString());
               Cvor noviCvor = new Cvor(idBrojac++, p.X / 20, p.Y / 20);
              // Cvor backCvor = new Cvor(idBrojac, p.X / 20, p.Y / 20);
               sema.cvorovi.Add(noviCvor);
               pictureBox1.Invalidate();
               listBox1.Items.Add(noviCvor.uString());
           }
           else
           {
               if (izvorni != null && odredisni != null && izvorni != odredisni)
               {
                   FormaGrana fg = new FormaGrana(izvorni,odredisni,null);
                   fg.ShowDialog();
                   pictureBox1.Invalidate();
                   sema.grane.Add(izvorni.grane[izvorni.grane.Count - 1]);
                   listBox2.Items.Add(izvorni.grane[izvorni.grane.Count - 1].uString());
                   izvorni = null;
                   odredisni = null;
               }
               else if (izvorni == odredisni)
               {
                   izvorni = null;
                   odredisni = null;
               }
           }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Cvor c in sema.cvorovi)
            {
                e.Graphics.FillEllipse(c.boja,c.x*20+7,c.y*20+7,5,5);
                foreach (Grana g in c.grane)
                {
                    e.Graphics.DrawLine(g.boja, g.izvor.x * 20 + 9, g.izvor.y * 20 + 9, g.odrediste.x * 20 + 9, g.odrediste.y * 20 + 9);
                    for (int i = 0; i < g.komponente.Count; i++)
                    {
                        int x = (g.brojkom * (g.izvor.x * 20 ) + g.odrediste.x * 20 ) / (g.brojkom + 1)+(((g.odrediste.x-g.izvor.x)*20)/(g.brojkom+1))*i+7;
                        int y = (g.brojkom * (g.izvor.y * 20 ) + g.odrediste.y * 20 ) / (g.brojkom + 1) + (((g.odrediste.y - g.izvor.y) * 20 ) / (g.brojkom + 1)) * i+7;      
                        e.Graphics.DrawImage(g.komponente[i].slika,x-15,y-15,35,35);       
                        Font font=new Font("Arial",8);
                        e.Graphics.DrawString(g.komponente[i].uString(),font,Brushes.Black,new Point(x-25,y+18));
                    }
                }
            }
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                foreach (Cvor c in sema.cvorovi)
                {
                    c.boja = Brushes.Black;
                }
                sema.cvorovi[listBox1.SelectedIndex].boja = Brushes.Red;
                pictureBox1.Invalidate();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Grana g in sema.grane)
            {
                g.boja = Pens.Black;
            }
            sema.grane[listBox2.SelectedIndex].boja = Pens.Red;
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                sema.obrisiGraneCvora(sema.cvorovi[listBox1.SelectedIndex]);
                pictureBox1.Invalidate();
                osveziListBoxove();
            }
        }

        private void osveziListBoxove()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (Cvor c in sema.cvorovi)
            {
                listBox1.Items.Add(c.uString());
            }
            foreach (Grana g in sema.grane)
            {
                listBox2.Items.Add(g.uString());
            }
            izvorni = null;
            odredisni = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                sema.obrisiGranu(sema.grane[listBox2.SelectedIndex]);
                pictureBox1.Invalidate();
                osveziListBoxove();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sema.reset();
            pictureBox1.Invalidate();
            osveziListBoxove();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                FormaGrana fg = new FormaGrana(null,null,sema.grane[listBox2.SelectedIndex]);
                fg.ShowDialog();
                pictureBox1.Invalidate();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sema.izbaciPipkeIOstrva();
            pictureBox1.Invalidate();
        }
    }
}
