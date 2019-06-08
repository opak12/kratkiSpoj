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
using System.Diagnostics;

namespace Test
{
    public partial class konfiguracijaNaloga : Form
    {


        TextBox trenutni;
        private bool t1 = false;
        private bool t2 = false;
        private bool t3 = false;
        private bool t4 = false;
        private bool t5 = false;
        private bool t6 = false;

        private bool pass1=true;
        private bool pass2 = true;
        private Prenos preneseno;
        public konfiguracijaNaloga(Prenos pr)
        {
            InitializeComponent();
            this.CenterToScreen();
            Color lightRed = ControlPaint.Light(Color.Red);
            this.textBox5.LostFocus += (source, e) =>
            {
                t5 = true;
                if (string.IsNullOrWhiteSpace(textBox5.Text))
                    panel9.BackColor = lightRed;
                else
                {
                    panel9.BackColor = Color.CornflowerBlue;
                }
                timer1.Stop();
            };
            this.textBox6.LostFocus += (source, e) =>
            {
                t6 = true;
                if (string.IsNullOrWhiteSpace(textBox6.Text))
                    panel10.BackColor = lightRed;
                else
                {
                    panel10.BackColor = Color.CornflowerBlue;
                }
                timer1.Stop();
            };
            this.textBox3.LostFocus += (source, e) =>
            {
                t3 = true;
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                    panel4.BackColor = lightRed;
                else
                {
                    panel4.BackColor = Color.LightSkyBlue;
                }
                timer1.Stop();
            };
            this.textBox4.LostFocus += (source, e) =>
            {
                t4 = true;
                if (string.IsNullOrWhiteSpace(textBox4.Text))
                    panel6.BackColor = lightRed;
                else
                {
                    panel6.BackColor = Color.LightSkyBlue;
                }
                timer1.Stop();
            };
            this.textBox1.LostFocus += (source, e) =>
            {
                t1 = true;
                if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    if (textBox1.Text!=textBox2.Text)
                    {
                        panel11.BackColor = lightRed;
                        panel12.BackColor = lightRed;
                    }
                    else
                    {
                        panel11.BackColor = Color.LightSkyBlue;
                        panel12.BackColor = Color.LightSkyBlue;
                    }
                }
                timer2.Stop();
            };
            this.textBox2.LostFocus += (source, e) =>
            {
                               t2= true;
                if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    if (textBox1.Text != textBox2.Text)
                    {
                        panel11.BackColor = lightRed;
                        panel12.BackColor = lightRed;
                    }
                    else
                    {
                        panel11.BackColor = Color.LightSkyBlue;
                        panel12.BackColor = Color.LightSkyBlue;
                    }
                }
                timer2.Stop();
               
            };
            this.textBox1.GotFocus += (source, e) =>
            {
                trenutni = textBox1;
                if (t1 && t2)
                timer2.Start();
            };
            this.textBox2.GotFocus += (source, e) =>
            {
                trenutni = textBox2;
                if (t2 && t1)
                    timer2.Start();
            };
            this.textBox3.GotFocus += (source, e) =>
            {
                trenutni = textBox3;
                if (t3)
                    timer1.Start();
            };
            this.textBox4.GotFocus += (source, e) =>
            {
                trenutni = textBox4;
                if (t4)
                    timer1.Start();
            };
            this.textBox5.GotFocus += (source, e) =>
            {
                trenutni = textBox5;
                if (t5)
                    timer1.Start();
            };
            this.textBox6.GotFocus += (source, e) =>
            {
                trenutni = textBox6;
                if (t6)
                    timer1.Start();
            };
            


            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
            preneseno = pr;
            label7.Text = "WELCOME " + pr.ime;
            this.ActiveControl = label7;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            preneseno.tezina = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Nije uneto ime!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Nije uneto prezime!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Nije unet fakultet!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Nije uneta katedra!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Nije uneta sifra!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                return;
                
            }
            if (textBox1.Text == textBox2.Text)
            {
                OracleConnection con = null;
                string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;"; ;

                try
                {



                    con = new OracleConnection(conString);
                    con.Open();
                    //Debugger.Break();

                    string SQL = " UPDATE PROFESOR SET FAKULTET = " + "'" + textBox5.Text.ToString() + "'" + " where korisnicki_nalog = " + "'" + preneseno.ime.ToString() + "'";
                    OracleCommand cmd = new OracleCommand(SQL, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();


                    SQL = " UPDATE PROFESOR SET KATEDRA = " + "'" + textBox6.Text.ToString() + "'" + " where korisnicki_nalog = " + "'" + preneseno.ime.ToString() + "'";
                    cmd = new OracleCommand(SQL, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    string ime_prezime = "";
                    ime_prezime = textBox3.Text + " " + textBox4.Text;

                    SQL = " UPDATE PROFESOR SET IME_PREZIME = " + "'" + ime_prezime.ToString() + "'" + " where korisnicki_nalog = " + "'" + preneseno.ime.ToString() + "'";
                    cmd = new OracleCommand(SQL, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    //  Debugger.Break();
                    SQL = " UPDATE PROFESOR SET Sifra = " + "'" + textBox1.Text.ToString() + "'" + " where korisnicki_nalog = " + "'" + preneseno.ime.ToString() + "'";
                    cmd = new OracleCommand(SQL, con);
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

                
                preneseno.tezina = 1;
                this.Close();
            }
            // primer
            else MessageBox.Show("Ne podudaraju se sifre!","Greska",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pass1 = !pass1;
            textBox1.PasswordChar = pass1 ? '*' : '\0';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pass2 = !pass2;
            textBox2.PasswordChar = pass2 ? '*' : '\0';
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Color lightRed = ControlPaint.Light(Color.Red);
            if (trenutni==textBox3)
                {
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                    panel9.BackColor = lightRed;
                else
                {
                    panel9.BackColor = Color.CornflowerBlue;
                }
                }

            if (trenutni == textBox6)
            {


                if (string.IsNullOrWhiteSpace(textBox6.Text))
                    panel10.BackColor = lightRed;
                else
                {
                    panel10.BackColor = Color.CornflowerBlue;
                }
            }
            if (trenutni == textBox6)
            {
                if (string.IsNullOrWhiteSpace(textBox6.Text))
                    panel10.BackColor = lightRed;
                else
                {
                    panel10.BackColor = Color.CornflowerBlue;
                }
            }
            if (trenutni == textBox4)
                if (string.IsNullOrWhiteSpace(textBox4.Text))
                    panel6.BackColor = lightRed;
                else
                {
                    panel6.BackColor = Color.LightSkyBlue;
                }
            if (trenutni == textBox5)
            {
                if (string.IsNullOrWhiteSpace(textBox5.Text))
                    panel9.BackColor = lightRed;
                else
                {
                    panel9.BackColor = Color.CornflowerBlue;
                }
            }

        }
            
        

        private void timer2_Tick(object sender, EventArgs e)
        {
            Color lightRed = ControlPaint.Light(Color.Red);
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (textBox1.Text != textBox2.Text)
                {
                    panel11.BackColor = lightRed;
                    panel12.BackColor = lightRed;
                }
                else
                {
                    panel11.BackColor = Color.LightSkyBlue;
                    panel12.BackColor = Color.LightSkyBlue;
                }
            }
        }
    }
}
