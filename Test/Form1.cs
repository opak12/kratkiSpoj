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
using projekat_forme_izgled;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Test
{
    public partial class Form1 : Form
    {
        bool gasi = true;
        Cvor Masa = null;
        List<Cvor> potencijalneMase = new List<Cvor>();
        Komponenta klikSelektovana = null;
        Grana klikSelektovanaGrana = null;
        List<Rectangle> slike = new List<Rectangle>();
        List<Komponenta> sveKomponente = new List<Komponenta>();
        SaveFileDialog sfd;
        Cvor pokretni;
        bool resenoKolo = false;
        bool mreza = true;
        bool edit = false;
        Cvor izvorni;
        Cvor odredisni;
        int brsema = 1;
        int idBrojac = 1;
        int idBrojacGrane = 1;
        int idKOmponente = 0;
        Sema sema;
        string sifra;
        string korisnicki_nalog;
        int brojacZaCrtanje = 1;
        public int pomZaEDIT = 0;
        Form1 k;
        bool select = false;
        Point prva = new Point(-5,-5);
        Point druga = new Point(-5, -5);
        bool mrdaDruga = false;
        bool selectMrda = false;
        List<Cvor> SelektovaniCvorovi = new List<Cvor>();
        public Form1(Loading l)
        {
            InitializeComponent();
            l.Close();
        }
        public Form1(int i, string s, string sif, string korisni,Loading l)
        {
            this.Focus();
            this.sfd = new SaveFileDialog();    
            WindowState = FormWindowState.Maximized;
            this.Size = new Size( Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            sema = new Sema();
            InitializeComponent();
            panel3.Visible = false;
            this.CenterToScreen();
            sifra = sif;
            k = this;
            korisnicki_nalog = korisni;
            pictureBox1.Enabled = true;
            pictureBox1.Visible = true;
            tabPage1.Text = @"OutPut";
            tabPage2.Text = @"Error List";
            postaviBordere();
            ugasiMenu();
            if (i == 0)
            {
                saveToolStripButton.Available = false;
                saveToolStripButton.Visible = false;
                saveToolStripButton.Enabled = false;
                myWorksToolStripMenuItem.Visible = false;
                myWorksToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Visible = false;
                saveToolStripMenuItem.Enabled = false;
                accountSettingsToolStripMenuItem.Visible = false;
                accountSettingsToolStripMenuItem.Enabled = false;
            }
            if (i != 0)
            {
                upaliMenu();
            }
            OracleConnection con = null;
            if (edit == false)
            {
                string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                try
                {
                    con = new OracleConnection(conString);
                    con.Open();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.Append("select max(id)+1 ");
                    strSQL.Append("FROM SEMA ");
                    OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int pom = dr.GetInt32(0);
                            brsema = pom;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Clan nema nijedan iznajmljeni film!");
                    }
                    dr.Close();
                }
                catch (Exception ec)
                {
                    Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
                }
                finally
                {
                    if (con != null && con.State == System.Data.ConnectionState.Open)
                        con.Close();

                    con = null;
                }
                con = null;
                 conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                try
                {
                    con = new OracleConnection(conString);
                    con.Open();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.Append("select max(id)+1 ");
                    strSQL.Append("FROM GRANA ");
                    OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int pom = dr.GetInt32(0);
                            idBrojacGrane = pom;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Clan nema nijedan iznajmljeni film!");
                    }
                    dr.Close();
                }
                catch (Exception ec)
                {
                    Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
                }
                finally
                {
                    if (con != null && con.State == System.Data.ConnectionState.Open)
                        con.Close();
                    con = null;
                }
                con = null;
                 conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                try
                {
                    con = new OracleConnection(conString);
                    con.Open();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.Append("select max(id)+1 ");
                    strSQL.Append("FROM CVOR ");
                    OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int pom = dr.GetInt32(0);
                            idBrojac = pom;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Clan nema nijedan iznajmljeni film!");
                    }
                    dr.Close();
                }
                catch (Exception ec)
                {
                    Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
                }
                finally
                {
                    if (con != null && con.State == System.Data.ConnectionState.Open)
                        con.Close();
                    con = null;
                }
                con = null;
                 conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                try
                {
                    con = new OracleConnection(conString);
                    con.Open();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.Append("select max(id)+1 ");
                    strSQL.Append("FROM KOMPONENTA ");
                    OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int pom = dr.GetInt32(0);
                            idKOmponente = pom;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Clan nema nijedan iznajmljeni film!");
                    }
                    dr.Close();
                }
                catch (Exception ec)
                {
                    Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
                }
                finally
                {
                    if (con != null && con.State == System.Data.ConnectionState.Open)
                        con.Close();
                    con = null;
                }
            }
            l.zatvoriSe();
            flowLayoutPanel1.Location = new Point(10,50);
            pictureBox1.Size = new Size((918*Screen.PrimaryScreen.Bounds.Width)/1000,(918*Screen.PrimaryScreen.Bounds.Height)/1000);
            tabControl1.Location = new Point(10,(850*Screen.PrimaryScreen.Bounds.Height)/1000); 
            double xx=(this.Width-tabControl1.Location.X)*0.95;
            tabControl1.Size = new Size(Convert.ToInt32(xx)-130, 120);
            double a = (this.tabControl1.Size.Width);
            listBox3.Location = new Point(0,0);
            listBox4.Location = new Point(0, 0);
            listBox3.Size = new Size(Convert.ToInt32(a*0.5), 100);
            listBox4.Size = new Size(Convert.ToInt32(a) - 5, 100);
            listBox1.Size = new Size(Convert.ToInt32(a / 2) - 5, 100);
            double noviX = (this.Width - flowLayoutPanel1.Location.X) * 0.95;
            double noviY = (this.Height - flowLayoutPanel1.Location.Y) * 0.90;
            flowLayoutPanel1.Size = new Size(Convert.ToInt32(noviX)-130, Convert.ToInt32(noviY)-100);
            tabControl1.Location = new Point(10, this.flowLayoutPanel1.Location.Y+Convert.ToInt32(noviY)-100);
            listBox1.Location = new Point(this.tabControl1.Size.Width / 2 , 0);
            int procPomeraj = (this.Width - 10 - this.flowLayoutPanel1.Size.Width) / 7;
            panel1.Location=new Point(this.flowLayoutPanel1.Location.X+this.flowLayoutPanel1.Size.Width+procPomeraj,50);
            panel2.Location = new Point(this.flowLayoutPanel1.Location.X + this.flowLayoutPanel1.Size.Width + procPomeraj, 190);
            panel3.Location = new Point(this.flowLayoutPanel1.Location.X + this.flowLayoutPanel1.Size.Width + procPomeraj, 453);
            flowLayoutPanel1.Controls.Add(pictureBox1);
        }
        private void ugasiMenu()
        {
            newToolStripMenuItem.Visible = true;
            newToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Visible = false;
            saveToolStripMenuItem.Enabled = false;
            loadToolStripMenuItem1.Visible = true;
            loadToolStripMenuItem1.Enabled = true;       
        }
        private void upaliMenu()
        {
            newToolStripMenuItem.Visible = true;
            newToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Visible = true;
            saveToolStripMenuItem.Enabled = true;
            loadToolStripMenuItem1.Visible = true;
            loadToolStripMenuItem1.Enabled = true;
            statisticsToolStripMenuItem1.Visible = true;
            statisticsToolStripMenuItem1.Enabled = true;
        }
        private void postaviBordere()
        {      
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {          
                if (select == true && prva.X!=-5 && druga.X!=-5 )
                {
                    if (selectMrda == false)
                    {
                        Point p12 = this.Location;//tacka za formu;
                        Point p11 = Cursor.Position;//u odnosu na pocetak ekrana;
                        p11.X -= (p12.X + pictureBox1.Location.X + 8);
                        p11.Y -= (p12.Y + pictureBox1.Location.Y + 30);
                        p11.X -= this.flowLayoutPanel1.Location.X;
                        p11.Y -= this.flowLayoutPanel1.Location.Y;
                        if (Math.Abs(p11.X-prva.X) <20 && Math.Abs(p11.Y-prva.Y)<20)
                        {
                            SelektovaniCvorovi.Clear();
                            int deltaX1 = druga.X - prva.X;
                            int deltaY1 = druga.Y - prva.Y;
                            Point iz = new Point(prva.X, prva.Y);
                            if (deltaX1 < 0)
                                iz.X += deltaX1;
                            if (deltaY1 < 0)
                                iz.Y += deltaY1;
                            int visina = Math.Abs(prva.Y - druga.Y) + 20;
                            int sirina = Math.Abs(prva.X - druga.X) + 20;
                            visina /= 20;
                            sirina /= 20;
                            iz.X /= 20;
                            iz.Y /= 20;
                            foreach (Cvor c in sema.cvorovi)
                            {
                                if (c.x >= iz.X && c.x <= iz.X + sirina && c.y >= iz.Y && c.y <= iz.Y + visina)
                                {
                                    SelektovaniCvorovi.Add(c);
                                }
                            }
                            selectMrda = true;
                        }
                        return;
                    }
                    else
                    {
                        selectMrda = false;
                    }
                }
                else if (pokretni != null)
                {
                    pokretni.boja = Brushes.Black;
                    pokretni = null;
                    foreach (Grana g in sema.grane)
                    {
                        foreach (Komponenta k in g.komponente)
                        {
                            k.namestiSliku(g.izvor, g.odrediste);
                        }
                    }
                    pictureBox1.Invalidate();
                    return;
                }
                Point p1 = this.Location;//tacka za formu;
                Point p = Cursor.Position;//u odnosu na pocetak ekrana;
                p.X -= (p1.X + pictureBox1.Location.X + 8);
                p.Y -= (p1.Y + pictureBox1.Location.Y + 30);
                p.X -= this.flowLayoutPanel1.Location.X;
                p.Y -= this.flowLayoutPanel1.Location.Y;
                foreach (Cvor c in sema.cvorovi)
                {
                    if (c.x == p.X / 20 && c.y == p.Y / 20)
                    {
                        pokretni = c;
                        pokretni.boja = Brushes.Red;
                    }
                }
                if (select == false)
                {
                    Point point1 = this.Location;//tacka za formu;
                    Point point = Cursor.Position;//u odnosu na pocetak ekrana;
                    point.X -= (point1.X + pictureBox1.Location.X + 8);
                    point.Y -= (point1.Y + pictureBox1.Location.Y + 30);
                    point.X -= this.flowLayoutPanel1.Location.X;
                    point.Y -= this.flowLayoutPanel1.Location.Y;
                    for (int i = 0; i < slike.Count;i++)
                    {
                        if (slike[i].X < point.X && slike[i].X + 36 > point.X && slike[i].Y < point.Y && slike[i].Y + 36 > point.Y)
                        {
                            klikSelektovana = sveKomponente[i];
                            textBox1.Text = klikSelektovana.ime;
                            numericUpDown1.Value = klikSelektovana.velicina;
                            pictureBox1.Invalidate();
                            return;
                        }
                    }
                    for (int i = 0; i < sema.grane.Count; i++)
                    {
                        int minX=-1, maxX=-1, minY=-1, maxY = -1;
                        int deltaX, deltaY = 0;
                        deltaX = Math.Abs(sema.grane[i].izvor.x-sema.grane[i].odrediste.x)*20;
                        deltaY = Math.Abs(sema.grane[i].izvor.y - sema.grane[i].odrediste.y)*20;
                        if (sema.grane[i].izvor.x >= sema.grane[i].odrediste.x)
                        {
                            maxX = sema.grane[i].izvor.x*20+10;
                            minX = sema.grane[i].odrediste.x * 20 + 10;
                        }
                        else
                        {
                            minX = sema.grane[i].izvor.x * 20 + 10;
                            maxX = sema.grane[i].odrediste.x * 20 + 10;
                        }
                        if (sema.grane[i].izvor.y >= sema.grane[i].odrediste.y)
                        {
                            maxY = sema.grane[i].izvor.y * 20 + 10;
                            minY = sema.grane[i].odrediste.y * 20 + 10;
                        }
                        else
                        {
                            minY = sema.grane[i].izvor.y * 20 + 10;
                            maxY = sema.grane[i].odrediste.y * 20 + 10;
                        }
                        if (point.X < maxX && point.X > minX)
                        {
                            if (Math.Abs(point.Y - ((sema.grane[i].izvor.y * 20 + 10) + (sema.grane[i].odrediste.y * 20+10)) / 2) <= 10 && deltaY<=20)
                            {
                                if (klikSelektovanaGrana != null)
                                {
                                    klikSelektovanaGrana.boja = Pens.Black;
                                }
                                klikSelektovanaGrana = sema.grane[i];
                                odradiGranu(klikSelektovanaGrana);
                                return;
                            }
                            if(point.Y > minY && point.Y < maxY)
                            {
                                if (Math.Abs(sema.grane[i].odrediste.x - sema.grane[i].izvor.x) >= Math.Abs(sema.grane[i].odrediste.y - sema.grane[i].izvor.y))
                                {
                                    double a = sema.grane[i].izvor.y * 20 + 10;
                                    double b = (sema.grane[i].odrediste.y * 20 + 10) - (sema.grane[i].izvor.y * 20 + 10);
                                    double c= (sema.grane[i].odrediste.x * 20+10) - (sema.grane[i].izvor.x * 20+10);
                                    double d = point.X - (sema.grane[i].izvor.x * 20 + 10);
                                    double ppp = a + (b / c) * d;
                                     if (Math.Abs(ppp - point.Y)<=10)
                                      {
                                        if (klikSelektovanaGrana != null)
                                        {
                                            klikSelektovanaGrana.boja = Pens.Black;
                                        }
                                        klikSelektovanaGrana = sema.grane[i];
                                        
                                        odradiGranu(klikSelektovanaGrana);
                                        return;   
                                    }
                                }
                                else
                                {
                                    double a = sema.grane[i].izvor.x * 20 + 10;
                                    double b = (sema.grane[i].odrediste.x * 20 + 10) - (sema.grane[i].izvor.x * 20 + 10);
                                    double c = (sema.grane[i].odrediste.y * 20 + 10) - (sema.grane[i].izvor.y * 20 + 10);
                                    double d = point.Y - (sema.grane[i].izvor.y * 20 + 10);
                                    double ppp = a + (b / c) * d;
                                    if (Math.Abs(ppp - point.X) <= 10)
                                    {
                                        if (klikSelektovanaGrana != null)
                                        {
                                            klikSelektovanaGrana.boja = Pens.Black;
                                        }
                                        klikSelektovanaGrana = sema.grane[i];
                                        odradiGranu(klikSelektovanaGrana);
                                        return;
                                    }
                                }
                            }
                        }
                        if (point.Y < maxY && point.Y > minY)
                        {
                            if (Math.Abs(point.X - ((sema.grane[i].izvor.x*20+10) + (sema.grane[i].odrediste.x*20+10)) / 2) <= 10 && deltaX<=20)
                            {
                                if (klikSelektovanaGrana != null)
                                {
                                    klikSelektovanaGrana.boja = Pens.Black;
                                }
                                klikSelektovanaGrana = sema.grane[i];
                                odradiGranu(klikSelektovanaGrana);
                                return;
                            }
                        }
                    }
                    if (klikSelektovanaGrana != null)
                    {
                        klikSelektovanaGrana.boja = Pens.Black;
                        klikSelektovanaGrana = null;
                       
                    }
                    if (klikSelektovana != null)
                    {
                        klikSelektovana = null;
                    }
                    pictureBox1.Invalidate();
                        return;
                }
            }
            else if (me.Button == MouseButtons.Left)
            {
                if (select == true)
                {
                    if (prva.X == -5)
                    {
                        Point p1 = this.Location;//tacka za formu;
                        Point p = Cursor.Position;//u odnosu na pocetak ekrana;
                        p.X -= (p1.X + pictureBox1.Location.X + 8);
                        p.Y -= (p1.Y + pictureBox1.Location.Y + 30);
                        p.X -= this.flowLayoutPanel1.Location.X;
                        p.Y -= this.flowLayoutPanel1.Location.Y;
                        prva.X = p.X;
                        prva.Y = p.Y;
                        mrdaDruga = true;
                    }
                    else
                    {
                        mrdaDruga = false;
                            int deltaX1 = druga.X - prva.X;
                            int deltaY1 = druga.Y - prva.Y;
                            Point iz = new Point(prva.X, prva.Y);
                            if (deltaX1 < 0)
                                iz.X += deltaX1;
                            if (deltaY1 < 0)
                                iz.Y += deltaY1;
                            int visina = Math.Abs(prva.Y - druga.Y) + 20;
                            int sirina = Math.Abs(prva.X - druga.X) + 20;
                            visina /= 20;
                            sirina /= 20;
                            iz.X /= 20;
                            iz.Y /= 20;
                            foreach (Cvor c in sema.cvorovi)
                            {
                                if (c.x >= iz.X && c.x <= iz.X + sirina && c.y >= iz.Y && c.y <= iz.Y + visina)
                                {
                                    SelektovaniCvorovi.Add(c);
                                }
                            }
                    }
                }
                else
                {
                    Point p1 = this.Location;//tacka za formu;
                    Point p = Cursor.Position;//u odnosu na pocetak ekrana;
                    p.X -= (p1.X + pictureBox1.Location.X + 8);
                    p.Y -= (p1.Y + pictureBox1.Location.Y + 30);
                    bool da_li_ima = false;
                    p.X -= this.flowLayoutPanel1.Location.X;
                    p.Y -= this.flowLayoutPanel1.Location.Y;
                    foreach (Cvor c in sema.cvorovi)
                    {
                        if (c.x == p.X / 20 && c.y == p.Y / 20)
                        {
                            da_li_ima = true;
                            if (izvorni == null)
                            {
                                izvorni = c;
                                izvorni.boja = Brushes.MediumSeaGreen;
                                pictureBox1.Invalidate();
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
                        int customBrojac = 0;
                        customBrojac = generisiZaCrtanje();
                        Cvor noviCvor = new Cvor(idBrojac++, p.X / 20, p.Y / 20);
                        noviCvor.zaCrtanje = customBrojac;
                        brojacZaCrtanje++;
                        sema.cvorovi.Add(noviCvor);
                        pictureBox1.Invalidate();
                        listBox1.Items.Add(noviCvor.zaString());
                    }
                    else
                    {
                        if (izvorni != null && odredisni != null && izvorni != odredisni)
                        {
                            izvorni.boja = Brushes.Black;
                            FormaGrana fg = new FormaGrana(this,idBrojacGrane++, izvorni, odredisni, null);
                            fg.ShowDialog();
                            pictureBox1.Invalidate();
                            sema.grane.Add(izvorni.grane[izvorni.grane.Count - 1]);
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
            }
        }

        private int generisiZaCrtanje()
        {
            //throw new NotImplementedException();
            int vrati = 1;
            bool flag = false;
            while (flag == false)
            {
                flag = true;
                foreach (Cvor c in sema.cvorovi)
                {
                    if (c.zaCrtanje == vrati)
                    {
                        vrati++;
                        flag = false;
                        break;
                    }
                }
            }
            return vrati;
        }

        private void odradiGranu(Grana klikSelektovanaGrana)
        {
            if (klikSelektovanaGrana != null)
            {
                klikSelektovanaGrana.boja = Pens.Red;
                listBox5.Items.Clear();
                foreach (Komponenta k in klikSelektovanaGrana.komponente)
                {
                    listBox5.Items.Add(k.uString());
                }
                label4.Text ="Atributi Grane "+klikSelektovanaGrana.izvor.zaCrtanje + " - " + klikSelektovanaGrana.odrediste.zaCrtanje;
                pictureBox1.Invalidate();
            }
            else
            {
                listBox5.Items.Clear();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (klikSelektovana != null)
            {
                panel1.Visible = true;
                panel1.Enabled = true;
                textBox1.Text = klikSelektovana.ime;
                numericUpDown1.Value = klikSelektovana.velicina;
            }
            else
            {
                panel1.Visible = true;
                panel1.Enabled = false;
                resetPanel1();
            }
            if (klikSelektovanaGrana != null)
            {
                panel2.Visible = true;
                panel2.Enabled = true;
            }
            else 
            {
                panel2.Visible = true;
                panel2.Enabled = false;
                resetPanel2();
            }
            sveKomponente.Clear();
            slike.Clear();
            Font font = new Font("Arial", 8);
            if (mreza == true)
            {
                for (int i = 0; i <pictureBox1.Width/20; i++)
                {
                    e.Graphics.DrawLine(Pens.LightGray, new Point(10 + i * 20, 0), new Point(10 + i * 20, pictureBox1.Height));
                }
                for (int i = 0; i < pictureBox1.Height/20; i++)
                {
                    e.Graphics.DrawLine(Pens.LightGray, new Point(0, 10 + i * 20), new Point(pictureBox1.Width, 10 + i * 20));
                }
            }
          
            foreach (Cvor c in sema.cvorovi)
            {
                e.Graphics.FillEllipse(c.boja, c.x * 20 + 7, c.y * 20 + 7, 5, 5);
                e.Graphics.DrawString(c.zaCrtanje.ToString(), font, c.boja, new Point(c.x * 20 - 4, c.y * 20 - 4));
                foreach (Grana g in c.grane)
                {
                    e.Graphics.DrawLine(g.boja, g.izvor.x * 20 + 10, g.izvor.y * 20 + 10, g.odrediste.x * 20 + 10, g.odrediste.y * 20 + 10);
                    for (int i = 0; i < g.komponente.Count; i++)
                    {
                        int x = (g.brojkom * (g.izvor.x * 20) + g.odrediste.x * 20) / (g.brojkom + 1) + (((g.odrediste.x - g.izvor.x) * 20) / (g.brojkom + 1)) * i + 7;
                        int y = (g.brojkom * (g.izvor.y * 20) + g.odrediste.y * 20) / (g.brojkom + 1) + (((g.odrediste.y - g.izvor.y) * 20) / (g.brojkom + 1)) * i + 7;
                        e.Graphics.DrawImage(g.komponente[i].slika, x - 14, y - 14, 35, 35);
                        sveKomponente.Add(g.komponente[i]);
                        slike.Add(new Rectangle(x-14,y-14,35,35));
                        e.Graphics.DrawString(g.komponente[i].uString(), font, Brushes.Black, new Point(x - 25, y + 19));
                    }
                }
            }
            if (select == true && prva.X != -5)
            {
                e.Graphics.FillEllipse(Brushes.Blue, prva.X-1, prva.Y-1, 4, 4);
                if (druga.X != -5)
                {
                    int deltaX = druga.X - prva.X;
                    int deltaY = druga.Y - prva.Y;
                    Point iz = new Point(prva.X, prva.Y);

                    if (deltaX < 0)
                        iz.X += deltaX;
                    if (deltaY < 0)
                        iz.Y += deltaY;
                    e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(iz.X , iz.Y , Math.Abs(deltaX), Math.Abs(deltaY)));
                }
            }
        }

        private void resetPanel2()
        {
            listBox5.Items.Clear();
            label4.Text = "Atributi Grane";
        }

        private void resetPanel1()
        {
            textBox1.Text = "";
            numericUpDown1.Value = 0;
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

        public void osveziListBoxove()
        {
            listBox1.Items.Clear();
            foreach (Cvor c in sema.cvorovi)
            {
                listBox1.Items.Add(c.zaString());
            }
            izvorni = null;
            odredisni = null;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            sema.reset();
            pictureBox1.Invalidate();
            osveziListBoxove();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            this.listBox4.Items.Clear();
            this.listBox4.Items.Add("Errors List:");
            resenoKolo = true;
            foreach (Grana g in sema.grane)
            {
                g.obradjena = false;
                g.struja = 0;
            }
            foreach (Cvor c in sema.cvorovi)
            {
                c.bot = true;
                c.indexer = -2;
                c.napon = 0;
            }
            sema.izbaciPipkeIOstrva(listBox4);
            prikaziPotege();
            osveziListBoxove();
            foreach (Poteg p in sema.pot)
            {
                int broj_kom = 0;
                foreach (Grana g in p.superGrana)
                {
                    foreach (Komponenta k in g.komponente)
                    {
                        broj_kom++;
                    }
                }
                if (broj_kom == 0)
                {
                    listBox4.Items.Add("Warning : Grana izmedju cvora " + p.izvor.zaCrtanje + " i cvora " + p.odrediste.zaCrtanje + ", nema prikjucen nijedan element.");
                }
                if (p.struja > 900)
                {
                    listBox4.Items.Add("Warning : Desio se kratak spoj izmedju cvora "+p.izvor.zaCrtanje+" i cvora "+p.odrediste.zaCrtanje+", predlazemo da ubacite otpornik kako bi ste ogranicili struju.");
                }
            }
            pictureBox1.Invalidate();
        }

        private void prikaziPotege()
        {
            listBox3.Items.Clear();
            if (sema.pot != null)
            {
                foreach (Poteg p in sema.pot)
                {
                    listBox3.Items.Add(p.info());
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sema.reset();
            pictureBox1.Invalidate();
            osveziListBoxove();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formlogin f = new Formlogin();
            f.Show();
            gasi = false;
            this.Hide();
            //this.Close();
        }
        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            sema.reset();
            listBox3.Items.Clear();
            pictureBox1.Invalidate();
            osveziListBoxove();
            brojacZaCrtanje = 1;
            pomZaEDIT = 0;
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Account a = new Account(sifra, korisnicki_nalog);
            a.StartPosition = FormStartPosition.Manual;
            a.Location = this.Location;
            a.Left += this.ClientSize.Width / 2 - a.Width / 2;
            a.Top += this.ClientSize.Height / 2 - a.Height / 2;
            a.Show();
        }

        private void snimiPodatke(Prenos pr)
        {
            OracleConnection con = null;
            string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
            try
            {
                sema.Idseme = brsema;
                con = new OracleConnection(conString);
                con.Open();
                int nik = 1;
                if (k.pomZaEDIT == 1)
                {
                    foreach (Cvor k1 in sema.cvorovi)
                    {
                        k1.id = idBrojac++;
                    }
                    foreach (Grana k2 in sema.grane)
                    {
                        k2.grana = idBrojacGrane++;
                        for (int i = 0; i < k2.komponente.Count; i++)
                        {
                            k2.komponente[i].id = idKOmponente++;
                        }
                    }
                }
                string strSQL = "INSERT INTO SEMA (ID,DAN,MESEC,GODINA,IME,OCENA,PROFESOR) VALUES(" + brsema.ToString() + "," + nik.ToString() + "," + nik.ToString() + "," + "'" + pr.godina.ToString() + "'" + "," + "'" + pr.ime.ToString() + "'" + "," + pr.tezina.ToString() +","+"'"+korisnicki_nalog+"'" +") ";
                OracleCommand cmd = new OracleCommand(strSQL, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ec)
            {
                Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
            }
            finally
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                con = null;
            }
            foreach (Cvor k in sema.cvorovi)
            {
                con = null;
                 conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                try
                {
                    int xxx = k.x;
                    int yyy = k.y;
                    con = new OracleConnection(conString);
                    con.Open();
                    string strSQL = "INSERT INTO CVOR (ID_SEMA,ID,X,Y,ZA_CRTANJE) VALUES(" + sema.Idseme.ToString() + "," + k.id.ToString() + "," + xxx.ToString() + "," + yyy.ToString() + "," + k.zaCrtanje.ToString() + ") ";         
                    OracleCommand cmd = new OracleCommand(strSQL, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ec)
                {
                    Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
                }
                finally
                {
                    if (con != null && con.State == System.Data.ConnectionState.Open)
                        con.Close();

                    con = null;
                }
            }
            foreach (Grana k in sema.grane)
            {
                con = null;
                 conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                try
                {
                    int izvor = k.izvor.id;
                    int odrediste = k.odrediste.id;
                    con = new OracleConnection(conString);
                    con.Open();
                    int mk = 45;
                    string strSQL = "INSERT INTO GRANA (ID_SEMA,ID,UGAO,IZVOR,ODREDISTE) VALUES (" + sema.Idseme.ToString() + "," + k.grana.ToString()
                        + "," + mk.ToString() + "," + izvor.ToString() + "," + odrediste.ToString() + ") ";
                    OracleCommand cmd = new OracleCommand(strSQL, con);
                    cmd.CommandType = System.Data.CommandType.Text;                    
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ec)
                {
                    Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
                }
                finally
                {
                    if (con != null && con.State == System.Data.ConnectionState.Open)
                        con.Close();

                    con = null;
                }
            }
            foreach (Grana g in sema.grane)
            {
                for (int mik = 0; mik < g.komponente.Count; mik++)
                {
                    g.komponente[mik].id = idKOmponente;
                    idKOmponente++;
                    con = null;
                     conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                    try
                    {
                        int smer = -1;
                        int velicina = -1; ;
                        int tip = -1;
                        if (g.komponente[mik].vrsta == Tip.naponskiGenerator)
                            tip = 0;
                        else if (g.komponente[mik].vrsta == Tip.strujniGenerator)
                            tip = 1;
                        else
                            tip = 2;
                        if (g.izvor == g.komponente[mik].frontPolaritet)
                            smer = 0;
                        else if (g.odrediste == g.komponente[mik].frontPolaritet)
                            smer = 1;
                        else
                            smer = 2;
                        velicina = g.komponente[mik].velicina;
                        int idgranebaz = g.grana;
                        con = new OracleConnection(conString);
                        con.Open();
                        string igor = g.komponente[mik].ime;
                        string strSQL = "INSERT INTO KOMPONENTA (ID_SEMA,ID,ID_GRANA,TIP,VELICINA,SMER,NAZIV) VALUES (" + sema.Idseme.ToString() + "," + g.komponente[mik].id.ToString() + "," + (idgranebaz).ToString()
                            + "," + tip.ToString() + "," + velicina.ToString() + "," + smer.ToString() + "," + "'" + igor.ToString() + "'" + ") ";                  
                        OracleCommand cmd = new OracleCommand(strSQL, con);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ec)
                    {
                        Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
                    }
                    finally
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Open)
                            con.Close();

                        con = null;
                    }
                }
            }
            brsema++;
            pomZaEDIT = 0;
            MessageBox.Show("Usnimljeno je u bazu podataka","Obavestenje",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit = true;
            sema = new Sema();
            osveziListBoxove();
            Load l = new Load(sema, k);
            l.StartPosition = FormStartPosition.Manual;
            l.Location = this.Location;
            l.Left += this.ClientSize.Width / 2 - l.Width / 2;
            l.Top += this.ClientSize.Height / 2 - l.Height / 2;
            l.ShowDialog();
            int maxid = 0;
            foreach (Cvor k3 in sema.cvorovi)
            {
                if (k3.zaCrtanje > maxid)
                    maxid = k3.zaCrtanje;
            }
            brojacZaCrtanje = maxid + 1;
            osveziListBoxove();
            pictureBox1.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prenos p = new Prenos();
            SaveForma sf = new SaveForma(this.korisnicki_nalog,p);
            sf.StartPosition = FormStartPosition.Manual;
            sf.Location = this.Location;
            sf.ShowDialog();
      //      Debugger.Break();
            if (p.godina != -1)
                snimiPodatke(p);
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
            {
                foreach (Grana g in sema.grane)
                {
                    g.boja = Pens.Black;
                }
                foreach (Cvor c in sema.cvorovi)
                {
                    c.boja = Brushes.Black;
                }
                foreach (Grana g in sema.pot[listBox3.SelectedIndex].superGrana)
                {
                    g.boja = Pens.Red;
                }
                sema.pot[listBox3.SelectedIndex].izvor.boja = Brushes.Red;
                pictureBox1.Invalidate();
            }
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void graphStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resenoKolo == true)
            {
                StatistickaForma sf = new StatistickaForma(sema.pot);
                sf.Show();
            }
        }

        private void pieStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resenoKolo == true)
            {
                PitaForma pf = new PitaForma(sema.pot);
                pf.Show();
            }
        }

        private void tevenenovEkvivalentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tevenenova t = new Tevenenova(sema, this);
            t.Show();
        }

        private void takeASnapshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
                sfd.Title = "Save Dialog";
                sfd.Filter = "Jpg Images (*.jpg)|*.jpg|All files(*.*)|*.*";
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
                    {
                        pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

                        Bitmap imgLogo = new Bitmap("watermark2.png");
                        Graphics img = Graphics.FromImage(bmp);
                        img.DrawImage(imgLogo, (bmp.Width - imgLogo.Width - 10), (bmp.Height - imgLogo.Height - 10), imgLogo.Width, imgLogo.Height);

                        bmp.Save(sfd.FileName, ImageFormat.Jpeg);
                        MessageBox.Show("Zapamceno " + Path.GetFullPath(sfd.FileName), "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
        }

        private void doodlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Image img = (Image)(new Bitmap((Image)bmp, new Size(1057, 595)));
                Doodler d = new Doodler(sema, img);
                d.Show();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            mreza = !mreza;
            pictureBox1.Invalidate();
        }

        private void pripremiZaCrtanje(int indeks, Color color)
        {
            foreach (Grana g in sema.pot[indeks].superGrana)
            {
                Pen p = new Pen(color);
                g.boja = p;
            }
            SolidBrush b = new SolidBrush(color);
            sema.pot[indeks].izvor.boja = b;

        }
        private void vratiPrvobitno()
        {
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                foreach (Grana g in sema.grane)
                {
                    g.boja = Pens.Black;
                }
                sema.grane[i].boja = Pens.Black;
                SolidBrush b = new SolidBrush(Color.Black);
                sema.pot[i].izvor.boja = b;

            }
            pictureBox1.Invalidate();
        }

        private void izvestajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resenoKolo == true)
            {
                using (SaveFileDialog sfdlg = new SaveFileDialog())
                {
                    sfdlg.Title = "Save Dialog";

                    sfdlg.Filter = "Jpg Images (*.jpg)|*.jpg|All files(*.*)|*.*";
                    if (sfdlg.ShowDialog(this) == DialogResult.OK)
                    {
                        Bitmap bmp = new Bitmap(pictureBox1.Width + 100, pictureBox1.Height + 100);
                        Graphics grp = Graphics.FromImage(bmp);
                        grp.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                        Color[] color = new Color[listBox3.Items.Count];
                        Random rand = new Random();
                        for (int i = 0; i < listBox3.Items.Count; i++)
                        {
                            if (i % 5 == 0)
                            {

                                color[i] = Color.FromArgb(255, 0, 12, 252);
                            }
                            if (i % 5 == 1)
                            {
                                color[i] = Color.FromArgb(255, 0, 252, 0);
                            }
                            if (i % 5 == 2)
                            {
                                color[i] = Color.FromArgb(255, 255, 17, 17);
                            }
                            if (i % 5 == 3)
                            {
                                color[i] = Color.FromArgb(255, 255, 108, 180);
                            }
                            if (i % 5 == 4)
                            {
                                color[i] = Color.FromArgb(255, 234, 200, 7);
                            }

                            pripremiZaCrtanje(i, color[i]);
                        }
                        pictureBox1.DrawToBitmap(bmp, new Rectangle(100, 100, bmp.Width, bmp.Height));
                        Font font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
                        Point pt = new Point(10, 5);
                        SolidBrush brush;
                        Graphics graphics = Graphics.FromImage(bmp);
                        for (int i = 0; i < listBox3.Items.Count; i++)
                        {
                            brush = new SolidBrush(color[i]);
                            graphics.DrawString(listBox3.Items[i].ToString(), font, brush, pt);
                            pt.Y += 15;
                            brush.Color = color[i];
                        }
                        graphics.Dispose();
                        pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        Bitmap imgLogo = new Bitmap("watermark2.png");
                        Graphics img = Graphics.FromImage(bmp);
                        img.DrawImage(imgLogo, (bmp.Width - imgLogo.Width - 10), (bmp.Height - imgLogo.Height - 10), imgLogo.Width, imgLogo.Height);
                        bmp.Save(sfdlg.FileName, ImageFormat.Jpeg);
                        MessageBox.Show("Zapamceno");
                        this.vratiPrvobitno();
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectMrda == true)
            {
                Point p1 = Cursor.Position;//u odnosu na pocetak ekrana;
                p1.X -= (this.Location.X + pictureBox1.Location.X + 8);
                p1.Y -= (this.Location.Y + pictureBox1.Location.Y + 30);
                      p1.X -= this.flowLayoutPanel1.Location.X;
                        p1.Y -= this.flowLayoutPanel1.Location.Y;
                int deltaX=p1.X / 20-prva.X/20;
                int deltaY = p1.Y / 20 - prva.Y / 20;
                if (deltaX!=0 || deltaY!= 0)
                {
                    prva.X += (deltaX * 20);
                    prva.Y += (deltaY * 20);
                    druga.X += (deltaX * 20);
                    druga.Y += (deltaY * 20);
                    int deltaX1 = druga.X - prva.X;
                    int deltaY1 = druga.Y - prva.Y;
                    foreach (Cvor c in SelektovaniCvorovi)
                    {
                            c.x += deltaX;
                            c.y += deltaY;
                    }
                    for (int i = 0; i < sema.grane.Count; i++)
                    {

                        for (int j = 0; j < sema.grane[i].komponente.Count; j++)
                        {
                            sema.grane[i].komponente[j].namestiSliku(sema.grane[i].izvor, sema.grane[i].odrediste);

                        }
                    }
                    pictureBox1.Invalidate();
                }
                return;
            }
            if (select == true && prva.X != -5 && mrdaDruga==true)
            {
                Point p2 = Cursor.Position;//u odnosu na pocetak ekrana;
                p2.X -= (this.Location.X + pictureBox1.Location.X + 8);
                p2.Y -= (this.Location.Y + pictureBox1.Location.Y + 30);
                p2.X -= this.flowLayoutPanel1.Location.X;
                p2.Y -= this.flowLayoutPanel1.Location.Y;
                druga.X = p2.X;
                druga.Y = p2.Y;
        
                pictureBox1.Invalidate();
                return;
            }
            else if (pokretni != null)
            {
                Point p = Cursor.Position;//u odnosu na pocetak ekrana;
                p.X -= (this.Location.X + pictureBox1.Location.X + 8);
                p.Y -= (this.Location.Y + pictureBox1.Location.Y + 30);
                p.X -= this.flowLayoutPanel1.Location.X;
                p.Y -= this.flowLayoutPanel1.Location.Y;
                if (p.X / 20 != pokretni.x || p.Y / 20 != pokretni.y)
                {
                    pokretni.x = p.X / 20;
                    pokretni.y = p.Y / 20;
                    for (int i = 0; i < sema.grane.Count; i++)
                    {
                        if (sema.grane[i].izvor == pokretni || sema.grane[i].odrediste == pokretni)
                        {
                            for (int j = 0; j < sema.grane[i].komponente.Count; j++)
                            {
                                sema.grane[i].komponente[j].namestiSliku(sema.grane[i].izvor, sema.grane[i].odrediste);
                            }
                        }
                    }
                        pictureBox1.Invalidate();
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                switch (e.KeyCode) { 
                    case Keys.N:{
                        newToolStripButton_Click(sender, (EventArgs)e);
                        break;
                    }
                    case Keys.G:{
                        toolStripButton3_Click(sender, (EventArgs)e);
                        break;
                        }
                    case Keys.A:{
                        toolStripButton2_Click(sender, (EventArgs)e);
                        return;
                    }
                    case Keys.S: {
                        if(korisnicki_nalog!="")
                        saveToolStripButton_Click(sender, (EventArgs)e);
                        return;
                    }
                    case Keys.L: {
                        openToolStripButton_Click(sender, (EventArgs)e);
                        break;
                    }
                    case Keys.T: {
                        toolStripButton4_Click(sender, (EventArgs)e);
                        break;
                    }
                    case Keys.I: {
                        toolStripButton1_Click(sender, (EventArgs)e);
                        break;
                    }
                    case Keys.H: {
                        helpToolStripButton_Click(sender, (EventArgs)e);
                        break;
                    }
                    case Keys.R:{
                        if (klikSelektovanaGrana != null)
                        {
                            klikSelektovanaGrana.boja = Pens.Black;
                            klikSelektovanaGrana = null;
                        }
                        klikSelektovana = null;
                        if (pokretni != null)
                        {
                            pokretni.boja = Brushes.Black;
                            pokretni = null;
                        }
                        pictureBox1.Invalidate();
                        return;
                        }
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (SelektovaniCvorovi != null && SelektovaniCvorovi.Count > 0)
                {
                    panel3.Visible = false;
                    for (int i = 0; i < SelektovaniCvorovi.Count; i++)
                    {
                        sema.obrisiGraneCvora(SelektovaniCvorovi[i]);
                    }
                    prva.X = -5;
                    druga.X = -5;
                    select = false;
                    SelektovaniCvorovi.Clear();
                    pictureBox1.Invalidate();

                    osveziListBoxove();
                }
                if(klikSelektovanaGrana!=null)
                {
                    panel3.Visible = false;
                    sema.obrisiGranu(klikSelektovanaGrana);
                    klikSelektovanaGrana = null;
                    odradiGranu(klikSelektovanaGrana);
                    pictureBox1.Invalidate();
                    osveziListBoxove();
                }
                if (klikSelektovana != null)
                {
                    panel3.Visible = false;
                    foreach (Grana g in sema.grane)
                    {
                        for (int i = 0; i < g.komponente.Count; i++)
                        {
                            if (g.komponente[i] == klikSelektovana)
                            {
                                g.komponente.Remove(klikSelektovana);
                                g.brojkom--;
                                if (g == klikSelektovanaGrana)
                                {
                                    odradiGranu(klikSelektovanaGrana);
                                }
                                i--;
                                klikSelektovana = null;
                                this.textBox1.Text = "";
                                this.numericUpDown1.Value = 0;
                            }
                        }
                    }
                    pictureBox1.Invalidate();
                }
                if (pokretni != null)
                {
                    panel3.Visible = false;
                    sema.obrisiGraneCvora(pokretni);
                    this.textBox1.Text = "";
                    this.numericUpDown1.Value = 0;
                    osveziListBoxove();
                    pictureBox1.Invalidate();
                    pokretni = null;
                }
                return;
            }
            if (e.KeyCode == Keys.F)
            {
                selectMrda = false;
                if (select == true)
                {
                    prva.X = -5;
                    druga.X = -5;
                    SelektovaniCvorovi.Clear();
                }
                select = !select;
                pictureBox1.Invalidate();
                return;
            }
            if (e.KeyCode == Keys.Q)//zoom in 
            {
                bool daLiMoze=true;
                foreach (Cvor c in sema.cvorovi)
                {
                    foreach (Cvor c1 in sema.cvorovi)
                    {
                        if (c != c1 && Math.Abs(c.x - c1.x) <= 4 && (Math.Abs(c.y - c1.y) <= 4) )
                        {
                           
                            daLiMoze = false;
                        }
                    }
                }
                if(daLiMoze==true){
                int xMin = 50;
                int xMax = -1;
                int yMin = 30;
                int yMax = -1;
                foreach(Cvor c in sema.cvorovi)
                {
                    if (c.x < xMin)
                        xMin = c.x;
                    if (c.x > xMax)
                        xMax = c.x;
                    if (c.y < yMin)
                        yMin = c.y;
                    if (c.y > yMax)
                        yMax = c.y;
                }
                foreach (Cvor c in sema.cvorovi)
                {
                    if (c.x-xMin <= (xMax - xMin) / 2)
                    {
                        c.x++;
                    }
                    else
                    {
                        c.x--;
                    }
                    if (c.y-yMin <= (yMax - yMin) / 2)
                    {
                        c.y++;
                    }
                    else
                    {
                        c.y--;
                    }
                }
                for (int i = 0; i < sema.grane.Count; i++)
                {
               
                        for (int j = 0; j < sema.grane[i].komponente.Count; j++)
                        {
                            sema.grane[i].komponente[j].namestiSliku(sema.grane[i].izvor, sema.grane[i].odrediste);
                        }
                    
                }
                pictureBox1.Invalidate();
                }
                return;
            }
            if (e.KeyCode == Keys.E)//zoom out
            {
                int xMin = 50;
                int xMax = -1;
                int yMin = 30;
                int yMax = -1;
                foreach (Cvor c in sema.cvorovi)
                {
                    if (c.x < xMin)
                        xMin = c.x;
                    if (c.x > xMax)
                        xMax = c.x;
                    if (c.y < yMin)
                        yMin = c.y;
                    if (c.y > yMax)
                        yMax = c.y;
                }
                foreach (Cvor c in sema.cvorovi)
                {
                    if (c.x-xMin <= (xMax - xMin) / 2)
                    {
                        c.x--;
                    }
                    else
                    {
                        c.x++;
                    }
                    if (c.y-yMin <= (yMax - yMin) / 2)
                    {
                        c.y--;
                    }
                    else
                    {
                        c.y++;
                    }
                }
                for (int i = 0; i < sema.grane.Count; i++)
                {

                        for (int j = 0; j < sema.grane[i].komponente.Count; j++)
                        {
                            sema.grane[i].komponente[j].namestiSliku(sema.grane[i].izvor, sema.grane[i].odrediste);
                        
                    }
                }
                pictureBox1.Invalidate();
                return;
            }
            if (e.KeyCode == Keys.A)
            {
                foreach (Cvor c in sema.cvorovi)
                {
                    c.x--;
                }
                pictureBox1.Invalidate();
                return;
            }
            if (e.KeyCode == Keys.D)
            {
                foreach (Cvor c in sema.cvorovi)
                {
                    c.x++;
                }
                pictureBox1.Invalidate();
                return;
            }
            if (e.KeyCode == Keys.W)
            {
                foreach (Cvor c in sema.cvorovi)
                {
                    c.y--;
                }
                pictureBox1.Invalidate();
                return;
            }
            if (e.KeyCode == Keys.S)
            {
                foreach (Cvor c in sema.cvorovi)
                {
                    c.y++;
                }
                pictureBox1.Invalidate();
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Focus();
            this.KeyPreview = true;
        }

        private void randomShemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prenos p = new Prenos();
            sema = new Sema();
            RandomForma rf = new RandomForma(sema,idBrojac,idBrojacGrane,p);
            rf.ShowDialog();
            brojacZaCrtanje = sema.cvorovi.Count + 1;
            idBrojac = p.godina;
            idBrojacGrane = p.tezina;
            osveziListBoxove();
            pictureBox1.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool flag=true;
            while(flag==true)
            {
                flag=false;
             foreach (Grana g in sema.grane)
             {
                if (g.izvor.x != g.odrediste.x && g.izvor.y != g.odrediste.y)
                {
                    int deltaX = Math.Abs(g.izvor.x-g.odrediste.x);
                    int deltaY = Math.Abs(g.izvor.y - g.odrediste.y);
                    if (deltaX > deltaY * 3.5)
                    {
                        if (g.izvor.y > g.odrediste.y)
                        {
                            g.izvor.y = g.odrediste.y;
                        }
                        else 
                        {
                            g.odrediste.y = g.izvor.y;
                        }
                        flag = true;
                    }
                    if (deltaY > deltaX * 3.5)
                    {
                        if (g.izvor.x > g.odrediste.x)
                        {
                            g.izvor.x = g.odrediste.x;
                        }
                        else
                        {
                            g.odrediste.x = g.izvor.x;
                        }
                        flag = true;
                    }

                }
             }
            }
            for (int i = 0; i < sema.grane.Count; i++)
            {
                for (int j = 0; j < sema.grane[i].komponente.Count; j++)
                {
                    sema.grane[i].komponente[j].namestiSliku(sema.grane[i].izvor, sema.grane[i].odrediste);
                }
            }
            pictureBox1.Invalidate();
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            edit = true;
            sema = new Sema();
            osveziListBoxove();
            Load l = new Load(sema, k);
            l.StartPosition = FormStartPosition.Manual;
            l.Location = this.Location;
            l.Left += this.ClientSize.Width / 2 - l.Width / 2;
            l.Top += this.ClientSize.Height / 2 - l.Height / 2;
            l.ShowDialog();
            int maxid = 0;
            foreach (Cvor k3 in sema.cvorovi)
            {
                if (k3.zaCrtanje > maxid)
                    maxid = k3.zaCrtanje;
            }
            brojacZaCrtanje = maxid + 1;          
            osveziListBoxove();
            pictureBox1.Invalidate();
        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resenoKolo == true)
            {
                StatistickaForma sf = new StatistickaForma(sema.pot);
                sf.Show();
            }
        }

        private void pieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resenoKolo == true)
            {
                PitaForma pf = new PitaForma(sema.pot);
                pf.Show();
            }
        }

        private void izvestajToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (resenoKolo == true)
            {           
                using (SaveFileDialog sfdlg = new SaveFileDialog())
                {
                    sfdlg.Title = "Save Dialog";
                    sfdlg.Filter = "Jpg Images (*.jpg)|*.jpg|All files(*.*)|*.*";
                    if (sfdlg.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bmp = new Bitmap(pictureBox1.Width + 100, pictureBox1.Height + 100);
                        Graphics grp = Graphics.FromImage(bmp);
                        grp.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                        Color[] color = new Color[listBox3.Items.Count];
                        Random rand = new Random();
                        for (int i = 0; i < listBox3.Items.Count; i++)
                        {
                            if (i % 5 == 0)
                            {
                                color[i] = Color.FromArgb(255, 0, 12, 252);
                            }
                            if (i % 5 == 1)
                            {
                                color[i] = Color.FromArgb(255, 0, 252, 0);
                            }
                            if (i % 5 == 2)
                            {
                                color[i] = Color.FromArgb(255, 255, 17, 17);
                            }
                            if (i % 5 == 3)
                            {
                                color[i] = Color.FromArgb(255, 255, 108, 180);
                            }
                            if (i % 5 == 4)
                            {
                                color[i] = Color.FromArgb(255, 234, 200, 7);
                            }
                            pripremiZaCrtanje(i, color[i]);
                        }
                        pictureBox1.DrawToBitmap(bmp, new Rectangle(100, 100, bmp.Width, bmp.Height));
                        Font font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
                        Point pt = new Point(10, 5);
                        SolidBrush brush;
                        Graphics graphics = Graphics.FromImage(bmp);
                        for (int i = 0; i < listBox3.Items.Count; i++)
                        {
                            brush = new SolidBrush(color[i]);
                            graphics.DrawString(listBox3.Items[i].ToString(), font, brush, pt);
                            pt.Y += 15;
                            brush.Color = color[i];
                        }
                        graphics.Dispose();
                        pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        Bitmap imgLogo = new Bitmap("watermark2.png");
                        Graphics img = Graphics.FromImage(bmp);
                        img.DrawImage(imgLogo, (bmp.Width - imgLogo.Width - 10), (bmp.Height - imgLogo.Height - 10), imgLogo.Width, imgLogo.Height);
                        bmp.Save(sfdlg.FileName, ImageFormat.Jpeg);
                        this.vratiPrvobitno();
                    }
                }
            }
        }

        private void snapshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
                sfd.Title = "Save Dialog";
                sfd.Filter = "Jpg Images (*.jpg)|*.jpg|All files(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
                    {
                        pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        Bitmap imgLogo = new Bitmap("watermark2.png");
                        Graphics img = Graphics.FromImage(bmp);
                        img.DrawImage(imgLogo, (bmp.Width - imgLogo.Width - 10), (bmp.Height - imgLogo.Height - 10), imgLogo.Width, imgLogo.Height);
                        bmp.Save(sfd.FileName, ImageFormat.Jpeg);
                        MessageBox.Show("Zapamceno " + Path.GetFullPath(sfd.FileName), "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoForma iff=new InfoForma();
            iff.Show();
        }

        private void myWorksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RadoviForma rf =new RadoviForma(k,this.sema,korisnicki_nalog);
            rf.ShowDialog();
            pictureBox1.Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            if (c.Height < 600)
            {
                c.Height = 605;
            }
            if (c.Width < 600)
                c.Width = 605;
            double noviY = ((c.Height - flowLayoutPanel1.Location.Y) * 0.90)-100;
            tabControl1.Location = new Point(10, Convert.ToInt32(noviY)+this.flowLayoutPanel1.Location.Y);
            double xx = (this.Width - tabControl1.Location.X) * 0.90;
            tabControl1.Size = new Size(Convert.ToInt32(xx)-130, 120);
            listBox3.Size = new Size(Convert.ToInt32(tabControl1.Size.Width*0.5),100);    
            double noviX = (c.Width -flowLayoutPanel1.Location.X)*0.90;
            flowLayoutPanel1.Size = new Size(Convert.ToInt32(noviX)-130, Convert.ToInt32(noviY));
            int procPomeraj = (c.Width-10-this.flowLayoutPanel1.Size.Width)/8;
            panel1.Location = new Point(this.flowLayoutPanel1.Location.X + this.flowLayoutPanel1.Size.Width + procPomeraj, 50);
            panel2.Location = new Point(this.flowLayoutPanel1.Location.X + this.flowLayoutPanel1.Size.Width + procPomeraj, 190);
            panel3.Location = new Point(this.flowLayoutPanel1.Location.X + this.flowLayoutPanel1.Size.Width + procPomeraj, 453);
            listBox1.Location = new Point(this.tabControl1.Size.Width / 2 , 0);
            double a = (this.tabControl1.Size.Width);
            listBox4.Size = new Size(Convert.ToInt32((a) - 5), 100);
            listBox1.Size = new Size(Convert.ToInt32((a / 2) - 5), 100);
            pictureBox1.Invalidate();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
        }
        private void okrugloDugme1_Click(object sender, EventArgs e)
        {
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (klikSelektovana != null)
            {
                klikSelektovana.ime = textBox1.Text;
                klikSelektovana.velicina = Convert.ToInt32(numericUpDown1.Value);
                if (klikSelektovanaGrana != null)
                {
                    this.odradiGranu(klikSelektovanaGrana);
                }
                pictureBox1.Invalidate();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (klikSelektovana != null)
            {
                panel3.Visible = false;
                foreach (Grana g in sema.grane)
                {
                    for (int i = 0; i < g.komponente.Count; i++)
                    {
                        if (g.komponente[i] == klikSelektovana)
                        {
                            g.komponente.Remove(klikSelektovana);
                            g.brojkom--;
                            if (g == klikSelektovanaGrana)
                            {
                                odradiGranu(klikSelektovanaGrana);
                            }
                            i--;
                            klikSelektovana = null;
                            this.textBox1.Text="";
                            this.numericUpDown1.Value=0;
                        }
                    }
                }
                pictureBox1.Invalidate();
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox5.SelectedIndex != -1 && klikSelektovanaGrana != null)
            {
                panel3.Visible = false;
                if (klikSelektovanaGrana.komponente[listBox5.SelectedIndex] == klikSelektovana)
                {
                    klikSelektovana = null;
                    this.textBox1.Text = "";
                    this.numericUpDown1.Value = 0;
                }
                klikSelektovanaGrana.komponente.RemoveAt(listBox5.SelectedIndex);
                klikSelektovanaGrana.brojkom--;
                pictureBox1.Invalidate();
                odradiGranu(klikSelektovanaGrana);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (klikSelektovanaGrana != null)
            {
                Form2 f2 = new Form2(this,klikSelektovanaGrana);
                f2.ShowDialog();
                odradiGranu(klikSelektovanaGrana);
                pictureBox1.Invalidate();
            }
        }
        public void ispisiError(String s)
        {
            listBox4.Items.Add("Error:" +s);
        }
        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox5.SelectedIndex != -1 && klikSelektovanaGrana != null)
            {
               // MessageBox.Show("Komponenta "+ klikSelektovanaGrana.komponente[listBox5.SelectedIndex].ime+" "+ klikSelektovanaGrana.komponente[listBox5.SelectedIndex].velicina);
               // MessageBox.Show("Index je "+listBox5.SelectedIndex.ToString());
               // MessageBox.Show(klikSelektovanaGrana.izvor.zaCrtanje+" "+klikSelektovanaGrana.odrediste.zaCrtanje+" broj je"+klikSelektovanaGrana.komponente.Count);
                //this.textBox1.Text = klikSelektovanaGrana.komponente[listBox5.SelectedIndex].ime;
               // this.numericUpDown1.Value = klikSelektovanaGrana.komponente[listBox5.SelectedIndex].velicina;
                this.klikSelektovana = klikSelektovanaGrana.komponente[listBox5.SelectedIndex];
                pictureBox1.Invalidate();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (klikSelektovanaGrana != null)
            {
                form_struja f2 = new form_struja(1,klikSelektovanaGrana);
                f2.ShowDialog();
                odradiGranu(klikSelektovanaGrana);
                pictureBox1.Invalidate();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (klikSelektovanaGrana != null)
            {
                form_struja f2 = new form_struja(0,klikSelektovanaGrana);
                f2.ShowDialog();
                odradiGranu(klikSelektovanaGrana);
                pictureBox1.Invalidate();
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            potencijalneMase.Clear();
            refListbox.Items.Clear();
            this.listBox4.Items.Clear();
            this.listBox4.Items.Add("Errors List:");
            resenoKolo = true;
            foreach (Grana g in sema.grane)
            {
                g.obradjena = false;
                g.struja = 0;
            }
            foreach (Cvor c in sema.cvorovi)
            {
                c.bot = true;
                c.indexer = -2;
                c.napon = 0;
            }
            sema.izbaciPipkeIOstrva(listBox4);
            prikaziPotege();
            osveziListBoxove();
            foreach (Poteg p in sema.pot)
            {
                int broj_kom = 0;
                foreach (Grana g in p.superGrana)
                {
                    foreach (Komponenta k in g.komponente)
                    {
                        broj_kom++;
                    }
                }
                if (broj_kom == 0)
                {
                    listBox4.Items.Add("Warning : Grana izmedju cvora " + p.izvor.zaCrtanje + " i cvora " + p.odrediste.zaCrtanje + ", nema prikjucen nijedan element.");
                }
                if (Math.Abs(p.struja) > 900)
                {
                    listBox4.Items.Add("Warning : Desio se kratak spoj izmedju cvora " + p.izvor.zaCrtanje + " i cvora " + p.odrediste.zaCrtanje + ", predlazemo da ubacite otpornik kako bi ste ogranicili struju.");
                }
            }
            bool flaggg = false;
            foreach (string s in listBox1.Items)
            {
                if (s == "Error : Greska! Nema Struje, proverite da li je kolo zatvoreno!!!")
                    flaggg = true;
                
            }
            if (flaggg==false)
            {
                label6.Text = "Referentni je Cvor ";
                panel3.Visible = true;
                foreach(Cvor c in sema.cvorovi)
                {
                    if (c.bot == false)
                    {
                        refListbox.Items.Add("Cvor "+c.zaCrtanje);
                        potencijalneMase.Add(c);
                        if (c.napon == 0)
                        {
                            label6.Text += c.zaCrtanje;
                        }
                    }                   
                }
            }
            else
            {
                panel3.Visible = false;
            }
            pictureBox1.Invalidate();
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mreza = !mreza;
            pictureBox1.Invalidate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            bool flag = true;
            while (flag == true)
            {
                flag = false;
                foreach (Grana g in sema.grane)
                {
                    if (g.izvor.x != g.odrediste.x && g.izvor.y != g.odrediste.y)
                    {
                        int deltaX = Math.Abs(g.izvor.x - g.odrediste.x);
                        int deltaY = Math.Abs(g.izvor.y - g.odrediste.y);
                        if (deltaX > deltaY * 3.5)
                        {
                            if (g.izvor.y > g.odrediste.y)
                            {
                                g.izvor.y = g.odrediste.y;
                            }
                            else
                            {
                                g.odrediste.y = g.izvor.y;
                            }
                            flag = true;
                        }
                        if (deltaY > deltaX * 3.5)
                        {
                            if (g.izvor.x > g.odrediste.x)
                            {
                                g.izvor.x = g.odrediste.x;
                            }
                            else
                            {
                                g.odrediste.x = g.izvor.x;
                            }
                            flag = true;
                        }
                    }
                }
            }
            for (int i = 0; i < sema.grane.Count; i++)
            {

                for (int j = 0; j < sema.grane[i].komponente.Count; j++)
                {
                    sema.grane[i].komponente[j].namestiSliku(sema.grane[i].izvor, sema.grane[i].odrediste);
                }
            }
            pictureBox1.Invalidate();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            klikSelektovana = null;
            klikSelektovanaGrana = null;
            pokretni = null;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            listBox3.Items.Clear();
            this.brojacZaCrtanje = 1;
            this.sema = new Sema();
            pictureBox1.Invalidate();
            this.osveziListBoxove();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (klikSelektovana != null)
            {
                klikSelektovana.ime = textBox1.Text;
               
                if (klikSelektovanaGrana != null)
                {
                    this.odradiGranu(klikSelektovanaGrana);
                }
                pictureBox1.Invalidate();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (klikSelektovana != null)
            {           
                klikSelektovana.velicina = Convert.ToInt32(numericUpDown1.Value);
                if (klikSelektovanaGrana != null)
                {
                    this.odradiGranu(klikSelektovanaGrana);
                }
                pictureBox1.Invalidate();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (klikSelektovana != null)
            {
                klikSelektovana.ime = textBox1.Text;
                klikSelektovana.velicina = Convert.ToInt32(numericUpDown1.Value);
                pictureBox1.Invalidate();
            }
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (refListbox.SelectedIndex != -1)
            {
                Masa = potencijalneMase[refListbox.SelectedIndex];
                label6.Text = "Referentni je Cvor " + Masa.zaCrtanje;
                foreach (Cvor c in potencijalneMase)
                {
                    if(c!=Masa)
                    c.napon -= Masa.napon;
                }
                Masa.napon = 0;
                osveziListBoxove();
                pictureBox1.Invalidate();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Tevenenova t = new Tevenenova(sema, this);
            t.Show();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            edit = true;
            //sema = new Sema();
            
            osveziListBoxove();
            Load l = new Load(sema, k);
            l.StartPosition = FormStartPosition.Manual;
            l.Location = this.Location;
            l.Left += this.ClientSize.Width / 2 - l.Width / 2;
            l.Top += this.ClientSize.Height / 2 - l.Height / 2;
            l.ShowDialog();
            int maxid = 0;
            foreach (Cvor k3 in sema.cvorovi)
            {
                if (k3.zaCrtanje > maxid)
                    maxid = k3.zaCrtanje;
            }
            brojacZaCrtanje = maxid + 1;          
                osveziListBoxove();
            pictureBox1.Invalidate();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Prenos p = new Prenos();
            SaveForma sf = new SaveForma(this.korisnicki_nalog,p);
            sf.StartPosition = FormStartPosition.Manual;
            sf.Location = this.Location;
            sf.ShowDialog();
           // Debugger.Break();
            if (p.godina != -1)
                snimiPodatke(p);
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("index.html");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("index.html");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            sfd.Title = "Save Dialog";
            sfd.Filter = "Jpg Images (*.jpg)|*.jpg|All files(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
                {
                    pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    Bitmap imgLogo = new Bitmap("watermark2.png");
                    Graphics img = Graphics.FromImage(bmp);
                    img.DrawImage(imgLogo, (bmp.Width - imgLogo.Width - 10), (bmp.Height - imgLogo.Height - 10), imgLogo.Width, imgLogo.Height);
                    bmp.Save(sfd.FileName, ImageFormat.Jpeg);
                    MessageBox.Show("Zapamceno " + Path.GetFullPath(sfd.FileName), "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void myAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(gasi==true)
            Application.Exit();
        }
    }
}