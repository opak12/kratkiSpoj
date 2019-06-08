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
using Test;

namespace projekat_forme_izgled
{
    public partial class Formlogin : Form
    {
        private string sifra;
        private string user;
        private bool watermark1 = true;
        private bool watermark2 = true;
        public String Sifra
        {get{ return sifra;}set { sifra = password.Text; } }
        private bool prikazSifre=true; // 1 prikazujemo *, 0 prikazujemo slova
        public Formlogin()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.ActiveControl = label3;
            panel4.Visible = false;
            password.PasswordChar = '\0';            
            button2.Visible = false;         
            textBox1.Text = "User name";
            password.Text = "Password";
            textBox1.ForeColor = Color.Gray;
            password.ForeColor = Color.Gray;
            password.Visible = false;
            button1.Visible = false;
            this.textBox1.GotFocus += (source, e) =>
            {
                if (watermark1)
                {
                    watermark1 = false;
                    textBox1.Text = "";
                    textBox1.ForeColor = Color.Black;
                }
            };
            this.textBox1.LostFocus += (source, e) =>
            {
                if (!watermark1 && string.IsNullOrEmpty(textBox1.Text))
                {
                    watermark1 = true;
                    textBox1.Text = "User name";
                    textBox1.ForeColor = Color.Gray;
                }
            };

           this.password.GotFocus += (source, e) =>
            {
                if (watermark2)
                {                  
                   watermark2 = false;
                    password.Text = "";
                    password.PasswordChar = '*';
                    password.ForeColor = Color.Black;
                }
            };
            this.textBox1.LostFocus += (source, e) =>
            {
                if (!watermark2 && string.IsNullOrEmpty(password.Text))
                {
                    password.PasswordChar = '\0';
                    watermark1 = true;
                    password.Text = "Password";
                    password.ForeColor = Color.Gray;
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prikazSifre = !prikazSifre;
            password.PasswordChar = prikazSifre ? '*' : '\0';
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {           
                Sifra = password.Text;
                string k = Sifra;
                OracleConnection con = null;
                string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                try
                {
                    con = new OracleConnection(conString);
                    con.Open();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.Append("SELECT COUNT(*) ");
                    strSQL.Append(" FROM PROFESOR ");
                    strSQL.Append(" WHERE Sifra= :k and KORISNICKI_NALOG= " + "'" + textBox1.Text + "'");
                    OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    OracleParameter param = new OracleParameter("k", OracleDbType.Varchar2);
                    param.Value = k;
                    cmd.Parameters.Add(param);
                    decimal brojFilmova = (decimal)cmd.ExecuteScalar();
                    if (brojFilmova >= 1)
                    {                       
                        Loading f = new Loading(1, "profa", password.Text, textBox1.Text,this);
                        f.Show();
                    }
                    else
                    {
                    MessageBox.Show("Pogresna sifra ili korisnicki nalog!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    }
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
        private void label4_Click(object sender, EventArgs e)
        {
           Loading f = new Loading(0,"student","","",this);            
            f.Show();           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int pomZANalog = -1;
            OracleConnection con = null;
            string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
            try
            {
                con = new OracleConnection(conString);
                con.Open();
                string k = textBox1.Text;
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT KORISNICKI_NALOG,SIFRA ");
                strSQL.Append(" FROM PROFESOR ");
                strSQL.Append(" WHERE KORISNICKI_NALOG=:k ");
                OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleParameter param = new OracleParameter("k", OracleDbType.Varchar2);
                param.Value = k;
                cmd.Parameters.Add(param);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string korisnicko = dr.GetString(0);
                        string sifricaa = dr.GetString(1);
                        if (sifricaa == "nebitno")
                            pomZANalog = 0;
                        else
                            pomZANalog = 1;
                    }
                }
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
            if (pomZANalog == 0)
            {
                Prenos p = new Prenos();
                p.ime = textBox1.Text;
                p.tezina = 1;
                konfiguracijaNaloga k = new konfiguracijaNaloga(p);
                k.ShowDialog();
                if (p.tezina == 1)
                {
                    this.ActiveControl = label3;
                    panel4.Visible = true;
                    panel3.Visible = false;
                    button4.Visible = false;
                    button2.Visible = true;
                    password.Visible = true;
                    button1.Visible = true;
                }
            }
            else if (pomZANalog == 1)
            {
                this.ActiveControl = label3;
                panel4.Visible = true;
                panel3.Visible = false;
                button4.Visible = false;
                button2.Visible = true;
                password.Visible = true;
                button1.Visible = true;
            }
            else
            {
                MessageBox.Show("Ne postoji korisnicki nalog!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Loading f = new Loading(1, "", "", "", this);
            f.Show();
        }

        private void Formlogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
