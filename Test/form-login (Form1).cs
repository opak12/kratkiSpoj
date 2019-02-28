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

namespace projekat_forme_izgled
{
    public partial class Formlogin : Form
    {
        private string sifra;

        public String Sifra
        {get{ return sifra;}set { sifra = password.Text; } }

        private bool prikazSifre=true; // 1 prikazujemo *, 0 prikazujemo slova
        public Formlogin()
        {
            InitializeComponent();
            password.PasswordChar = '*';
          
            button2.FlatStyle = FlatStyle.Flat;

            button2.FlatAppearance.BorderColor = Color.Navy;
            button2.FlatAppearance.BorderSize = 1;
           
           

          
            
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
          /*  Sifra = password.Text;
            string k = Sifra;
            int c = Convert.ToInt32(k);
            OracleConnection con = null;
            string conString = "Data Source = localhost:1521/SAFAA; User Id = hr; Password = aca;";
            try
            {
                //korisnik sa tastature unosi tip filmova
                //  Console.WriteLine("Uneti tip za koji zelite da odredite broj filmova");
                // string tip = Console.ReadLine();

                //otvaramo konekciju ka bazi podataka
                con = new OracleConnection(conString);
                con.Open();

                //pripremamo komandu koja ce za zadati tip odrediti broj filmova

                StringBuilder strSQL = new StringBuilder();

                strSQL.Append("SELECT COUNT(*) ");
                strSQL.Append(" FROM COUNTRIES ");
                strSQL.Append(" WHERE REGION_ID= :c ");
                
               // strSQL.Append(" AND IZNAJMLJIVANJE.CLAN = :k ");
              //  string strSQL = "SELECT COUNT(*) FROM COUNTRIES where REGION_ID= 2";

                OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                cmd.CommandType = System.Data.CommandType.Text;

                OracleParameter param = new OracleParameter("c", OracleDbType.Int32);
                param.Value = c;
                cmd.Parameters.Add(param);

                //izvrsavamo komandu i prihvatamo skalarnu vrednost
                decimal brojFilmova = (decimal)cmd.ExecuteScalar();


                
                if (brojFilmova >= 1)
                {
                    MessageBox.Show("Imamo Profesora" + " " + brojFilmova.ToString());
                    // pozvati Markovu form1 1
                   // i.Show();

                }
                else
                {
                    MessageBox.Show("POgresna sifra");
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
            }*/
        }

        private void label4_MouseClick(object sender, MouseEventArgs e)
        {
           // form_struja i = new form_struja(2);
           // i.Show();markova forma1
           
        }
    }
}
