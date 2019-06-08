using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace Test
{
    public partial class Doodler : Form
    {
        Font font = new Font("Arial", 10);
        Sema sema;
        bool crta = false;
        Color boja = Color.Red;
        List<Dodatak> lista;
        List<List<Dodatak>> redoLista = new List<List<Dodatak>>();
        List<List<Dodatak>> ukupnaLista = new List<List<Dodatak>>();
        bool pise = false;
        Image sl;
        public Doodler()
        {
            InitializeComponent();         
        }
        public Doodler(Sema i, Image img)
        {
            sema = i;
            InitializeComponent();
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = img;
            sl = img;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (List<Dodatak> lista in ukupnaLista)
                foreach (Dodatak dod in lista)
                    dod.nacrtaj(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {
                boja = colorDialog1.Color;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pise)
            {
                pise = false;
                Point p1 = this.Location;//tacka za formu;
                Point p = Cursor.Position;//u odnosu na pocetak ekrana;
                p.X -= (p1.X + pictureBox1.Location.X + 8);
                p.Y -= (p1.Y + pictureBox1.Location.Y + 30);
                Dodatak novi = new Komentar(textBox1.Text, font, p, boja, trackBar1.Value * 10 + 5);
                List<Dodatak> childLista = new List<Dodatak>();
                childLista.Add(novi);
                ukupnaLista.Add(childLista);
                redoLista.Clear();
                pictureBox1.Invalidate();
                return;
            }
            crta = !crta;
            if (crta)
            {
                List<Dodatak> novaLista = new List<Dodatak>();
                lista = novaLista;
                ukupnaLista.Add(novaLista as List<Dodatak>);
                redoLista.Clear();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (crta)
            {
                dodajSprej();
                pictureBox1.Invalidate();
            }
        }

        private void dodajSprej()
        {
            Point p1 = this.Location;//tacka za formu;
            Point p = Cursor.Position;//u odnosu na pocetak ekrana;
            p.X -= (p1.X + pictureBox1.Location.X + 8);
            p.Y -= (p1.Y + pictureBox1.Location.Y + 30);
            if (lista.Count > 0)
            {
                int deltaX = (p.X - lista[lista.Count - 1].tacka.X) / 3;
                int deltaY = (p.Y - lista[lista.Count - 1].tacka.Y) / 3;
                Point botTacka = new Point(lista[lista.Count - 1].tacka.X + deltaX, lista[lista.Count - 1].tacka.Y + deltaY);
                lista.Add(new Sprej(botTacka, boja, (int)numericUpDown1.Value, trackBar1.Value * 10 + 5));
                Point noviBot = new Point(botTacka.X + deltaX, botTacka.Y + deltaY);
                lista.Add(new Sprej(noviBot, boja, (int)numericUpDown1.Value, trackBar1.Value * 10 + 5));
            }
            lista.Add(new Sprej(p, boja, (int)numericUpDown1.Value, trackBar1.Value * 10 + 5));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfdlg = new SaveFileDialog())
            {
                sfdlg.Title = "Save Dialog";
                sfdlg.Filter = "Jpg Images (*.jpg)|*.jpg|All files(*.*)|*.*";
                if (sfdlg.ShowDialog(this) == DialogResult.OK)
                {
                    using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
                    {
                        pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        Bitmap imgLogo = new Bitmap("watermark2.png");
                        Graphics img = Graphics.FromImage(bmp);
                        img.DrawImage(imgLogo, (bmp.Width - imgLogo.Width - 10), (bmp.Height - imgLogo.Height - 10), imgLogo.Width, imgLogo.Height);
                        bmp.Save(sfdlg.FileName, ImageFormat.Jpeg);
                    }
                }
            }
            pictureBox1.Image = sl;
            pictureBox1.Invalidate();
        }
        void undic()
        {
            if (ukupnaLista.Count > 0)
            {
                redoLista.Add(ukupnaLista[ukupnaLista.Count - 1]);
                ukupnaLista.RemoveAt(ukupnaLista.Count - 1);
                pictureBox1.Invalidate();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            undic();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (redoLista.Count > 0)
            {
                ukupnaLista.Add(redoLista[redoLista.Count - 1]);
                redoLista.RemoveAt(redoLista.Count - 1);
                pictureBox1.Invalidate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pise = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == fontDialog1.ShowDialog())
            {
                font = fontDialog1.Font;
            }
        }    
    }
}
