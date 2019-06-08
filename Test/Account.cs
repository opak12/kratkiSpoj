using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Test
{
    public partial class Account : Form
    {
       private bool prvi = true;
       private bool drugi = true;
       private bool treci = true;
        private string sifra;
        private string korisnicki_nalo;
        private string ime_prezime;
        private string katedra;
        private string fakultet;
        public Account(string sifrica, string korisnicko)
        {
            InitializeComponent();

           


            okrugloDugme1.FlatStyle = FlatStyle.Flat;
            okrugloDugme1.FlatAppearance.BorderSize = 0;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
            button2.Visible = false;
            panelSifra.Visible = false;
           textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';
            textBox5.PasswordChar = '*';

            sifra = sifrica;
            korisnicki_nalo = korisnicko;

            OracleConnection con = null;
            string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";

            try
            {

                //otvaramo konekciju ka bazi podataka
                con = new OracleConnection(conString);
                con.Open();

                StringBuilder strSQL = new StringBuilder();
                //Debugger.Break();
                strSQL.Append(" select PROFESOR.IME_PREZIME,PROFESOR.KATEDRA,PROFESOR.FAKULTET ");
                strSQL.Append(" FROM PROFESOR ");
                strSQL.Append(" where KORISNICKI_NALOG = " + "'" + korisnicki_nalo.ToString() + "'");

                OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                cmd.CommandType = System.Data.CommandType.Text;


                //izvrsavamo komandu i u DataReader prihvatamo informacija o filmovima
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ime_prezime = dr.GetString(0);
                        katedra = dr.GetString(1);
                        fakultet = dr.GetString(2);

                    }
                }
                else
                {
                    MessageBox.Show("Greska!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }

                dr.Close();
                label1.Text = ime_prezime;
                textBox1.Text = fakultet;
                textBox2.Text = katedra;


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
       
        private void okrugloDugme1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(okrugloDugme1, new Point(-80, okrugloDugme1.Height));
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panelSifra.Visible = false;
            button2.Visible = true;
            button2.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OracleConnection con = null;
            string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";

            try
            {



                con = new OracleConnection(conString);
                con.Open();
                //Debugger.Break();

                string SQL = " UPDATE PROFESOR SET FAKULTET = " + "'" + textBox1.Text.ToString() + "'" + " where korisnicki_nalog = " + "'" + korisnicki_nalo.ToString() + "'";
                OracleCommand cmd = new OracleCommand(SQL, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();


                SQL = " UPDATE PROFESOR SET KATEDRA = " + "'" + textBox2.Text.ToString() + "'" + " where korisnicki_nalog = " + "'" + korisnicki_nalo.ToString() + "'";
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

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
            button2.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            prvi = !prvi;
          
            textBox3.PasswordChar = prvi ? '*' : '\0';
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            drugi = !drugi;
            textBox4.PasswordChar = drugi ? '*' : '\0';
        }

        private void button6_Click(object sender, EventArgs e)
        {
          treci = !treci;
            textBox5.PasswordChar = treci ? '*' : '\0';
        }

        private void asdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panelSifra.Visible = true;
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            prvi = true;
            drugi = true;
            treci = true;
            textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';
            textBox5.PasswordChar = '*';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Debugger.Break();
            if (textBox3.Text == sifra)
            {

                if (string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Nije uneta sifra!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    return;

                }
                else if (string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Nije potvrdjena sifra!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBox4.Text != textBox5.Text)
                {
                    MessageBox.Show("Nije lepo potvrdjena sifra!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    return;
                }

                OracleConnection con = null;
                string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";

                try
                {
                  

                    //otvaramo konekciju ka bazi podataka
                    con = new OracleConnection(conString);
                    con.Open();

                    StringBuilder strSQL = new StringBuilder();

                    //Debugger.Break();
                    string novaSifra = textBox4.Text;
                    strSQL.Append(" UPDATE PROFESOR ");
                    strSQL.Append(" PROFESOR.SIFRA=:novaSifra ");
                    strSQL.Append(" where KORISNICKI_NALOG=:korisnicki_nalo ");
                   
                    string SQL = " UPDATE PROFESOR SET SIFRA = " + "'" + novaSifra.ToString() + "'" + " where korisnicki_nalog = " + "'" + korisnicki_nalo.ToString() + "'";
                    // OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    OracleCommand cmd = new OracleCommand(SQL, con);
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
                panel2.Visible = true;
                panelSifra.Visible = false;
                return;
            }
            MessageBox.Show("Unesite tacnu trenutnu sifru!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            return;
        }

        private void Account_Load(object sender, EventArgs e)
        {        
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panelSifra.Visible = false;
            panel2.Visible = true;
        }
    }
}
