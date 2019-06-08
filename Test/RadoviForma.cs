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
    public partial class RadoviForma : Form
    {
        Sema sema;
        string username;
        List<int> listaId = new List<int>();
        List<string> listaImena = new List<string>();
        Form1 forma;
        public RadoviForma(Form1 forma,Sema sema, string username)
        {
            this.sema = sema;
            this.username = username;
            this.forma = forma;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)//ucitavanje selektovane seme u Voltrix
        {
            napuniListBox();
        }
        private void napuniListBox()
        {
            listaId.Clear();
            listaImena.Clear();
            //listaImena.Add("MArko");
            //kod za pristup bazi tipa select id, ime from sema where sema.profesor='username';
            //napuni listu imena sema i onda to punimo u listbox;
            //paralelno puni listaId sa idejem seme

            OracleConnection con = null;
            string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";

            try
            {


                //otvaramo konekciju ka bazi podataka
                con = new OracleConnection(conString);
                con.Open();

                StringBuilder strSQL = new StringBuilder();

                strSQL.Append("SELECT ID,DAN,MESEC,GODINA,IME,OCENA,PROFESOR ");
                strSQL.Append(" FROM SEMA ");
                strSQL.Append(" where PROFESOR= " + "'" + username + "'");

                // Debugger.Break();

                OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                cmd.CommandType = System.Data.CommandType.Text;






                //izvrsavamo komandu i u DataReader prihvatamo informacija o filmovima
                OracleDataReader dr = cmd.ExecuteReader();
                //Debugger.Break();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        int id = dr.GetInt32(0);
                        int dan = dr.GetInt32(1);
                        int mesec = dr.GetInt32(2);
                        int godinaa = dr.GetInt32(3);
                        string ime = dr.GetString(4);
                        int pom = dr.GetInt32(5);
                        listaImena.Add(ime);
                        listaId.Add(id);

                    }
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Greska " + ec.Message);
                Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
            }
            finally
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                    con.Close();

                con = null;
            }



            listBox1.Items.Clear();
            for (int i = 0; i < listaImena.Count; i++)
            {
                listBox1.Items.Add(listaImena[i]);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)//ako je neka sema selektovana
            {
                //kod za vracanje sledece seme iz baze : select * from sema where sema.id= listaId[listbox1.selectedItem];
                //i onda sema = ucitana sema

                OracleConnection con = null;
                string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";
                int broj = listaId[listBox1.SelectedIndex];

                try
                {

                    sema.Idseme = broj;
                    //otvaramo konekciju ka bazi podataka
                    con = new OracleConnection(conString);
                    con.Open();


                    StringBuilder strSQL = new StringBuilder();

                    strSQL.Append("SELECT CVOR.ID_SEMA,CVOR.ID, CVOR.X, CVOR.Y,CVOR.ZA_CRTANJE ");
                    strSQL.Append(" FROM CVOR ");
                    strSQL.Append(" where ID_SEMA=:broj ");

                    OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    //kreiramo odgovarajuci parametar
                    OracleParameter paramBroj = new OracleParameter("broj", OracleDbType.Int32);
                    paramBroj.Value = broj;

                    //dodajemo parametar u listu
                    cmd.Parameters.Add(paramBroj);


                    //izvrsavamo komandu i u DataReader prihvatamo informacija o filmovima
                    OracleDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //  brsema = dr.GetInt32(0);
                            int idCvora = dr.GetInt32(1);
                            int korX = dr.GetInt32(2);
                            int korY = dr.GetInt32(3);
                            int zaCrtis = dr.GetInt32(4);

                            Cvor k = new Cvor(idCvora, korX, korY);
                            k.zaCrtanje = zaCrtis;
                            // MessageBox.Show(idCvora.ToString() + " " + korX.ToString() + " " + korY.ToString());
                            sema.cvorovi.Add(k);
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
                    MessageBox.Show(ec.Message, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("Greska " + ec.Message);
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
                    //ucitavamo informacije o broju clana
                    // Console.WriteLine("Uneti broj clana za koga se kreira spisak:");
                    //  int broj = Int32.Parse(Console.ReadLine());

                    //otvaramo konekciju ka bazi podataka
                    con = new OracleConnection(conString);
                    con.Open();

                    StringBuilder strSQL = new StringBuilder();

                    strSQL.Append("SELECT GRANA.ID_SEMA, GRANA.ID, GRANA.UGAO,GRANA.IZVOR,GRANA.ODREDISTE ");
                    strSQL.Append(" FROM GRANA ");
                    strSQL.Append(" where ID_SEMA=:broj ");


                    OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    //kreiramo odgovarajuci parametar
                    OracleParameter paramBroj = new OracleParameter("broj", OracleDbType.Int32);
                    paramBroj.Value = broj;

                    //dodajemo parametar u listu
                    cmd.Parameters.Add(paramBroj);


                    //izvrsavamo komandu i u DataReader prihvatamo informacija o filmovima
                    OracleDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int id_sema = dr.GetInt32(0);
                            int idGrana = dr.GetInt32(1);
                            int ugao = dr.GetInt32(2);
                            int izvorcvor = dr.GetInt32(3);
                            int odredistecvor = dr.GetInt32(4);
                            //  Debugger.Break();
                            int i1 = -1;
                            int i2 = -1;
                            for (int io = 0; io < sema.cvorovi.Count; io++)
                            {
                                if (sema.cvorovi[io].id == izvorcvor)
                                    i1 = io;
                                if (sema.cvorovi[io].id == odredistecvor)
                                    i2 = io;

                            }
                            //  Debugger.Break();
                            Grana k = new Grana(idGrana, sema.cvorovi[i1], sema.cvorovi[i2], ugao);
                            sema.cvorovi[i1].grane.Add(k);
                            // Debugger.Break();
                            // MessageBox.Show(idCvora.ToString() + " " + korX.ToString() + " " + korY.ToString());
                            sema.grane.Add(k);
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


                    //otvaramo konekciju ka bazi podataka
                    con = new OracleConnection(conString);
                    con.Open();

                    StringBuilder strSQL = new StringBuilder();

                    strSQL.Append("SELECT KOMPONENTA.ID_SEMA,KOMPONENTA.ID, KOMPONENTA.ID_GRANA, KOMPONENTA.TIP,KOMPONENTA.VELICINA,KOMPONENTA.SMER,KOMPONENTA.NAZIV ");
                    strSQL.Append(" FROM KOMPONENTA ");
                    strSQL.Append(" WHERE KOMPONENTA.ID_SEMA=:broj ");


                    OracleCommand cmd = new OracleCommand(strSQL.ToString(), con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    //kreiramo odgovarajuci parametar
                    OracleParameter paramBroj = new OracleParameter("broj", OracleDbType.Int32);
                    paramBroj.Value = broj;

                    //dodajemo parametar u listu
                    cmd.Parameters.Add(paramBroj);


                    //izvrsavamo komandu i u DataReader prihvatamo informacija o filmovima
                    OracleDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        //Debugger.Break();
                        while (dr.Read())
                        {
                            int kk = dr.GetInt32(0);
                            int idkomponente = dr.GetInt32(1);
                            int id_grane = dr.GetInt32(2);
                            int tipkompone = dr.GetInt32(3);
                            int velicina = dr.GetInt32(4);
                            int smer = dr.GetInt32(5);
                            string nazv = dr.GetString(6);
                            int pom = -1;
                            for (int i = 0; i < sema.grane.Count; i++)
                            {
                                if (sema.grane[i].grana == id_grane)
                                    pom = i;
                            }

                            //  Debugger.Break();
                            // Komponenta k = new Otpornik(10, "R1");
                            // k.slika = Image.FromFile("opt.png");
                            Komponenta novaKomponenta = null;
                            if (tipkompone == 0)
                            {
                                novaKomponenta = new NaponskiGenerator(velicina, nazv, true);
                                novaKomponenta.id = idkomponente;
                            }

                            else if (tipkompone == 1)
                            {
                                novaKomponenta = new StrujniGenerator(velicina, nazv, true);
                                novaKomponenta.id = idkomponente;
                            }
                            else
                            {
                                novaKomponenta = new Otpornik(velicina, nazv);
                                novaKomponenta.id = idkomponente;
                            }
                            sema.grane[pom].komponente.Add(novaKomponenta);
                            sema.grane[pom].brojkom++;
                            int komp = -1;
                            for (int i = 0; i < sema.grane[pom].komponente.Count; i++)
                            {
                                if (sema.grane[pom].komponente[i].id == idkomponente)
                                    komp = i;
                            }
                            if (smer == 0)
                            {
                                sema.grane[pom].komponente[komp].polaritet = sema.grane[pom].izvor;//ako je 0 prema izvor
                                sema.grane[pom].komponente[komp].frontPolaritet = sema.grane[pom].izvor;
                            }
                            else if (smer == 1)
                            {
                                sema.grane[pom].komponente[komp].polaritet = sema.grane[pom].odrediste;
                                sema.grane[pom].komponente[komp].frontPolaritet = sema.grane[pom].odrediste;
                            }

                            sema.grane[pom].komponente[komp].namestiSliku(sema.grane[pom].izvor, sema.grane[pom].odrediste);

                            //if(sema.grane[pom].komponente[komp].polaritet==sema.grane[pom].izvor)
                            //    smer=0
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
                    MessageBox.Show(ec.Message, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("Greska " + ec.Message);
                    Console.WriteLine("Doslo je do greske prilikom pristupanja bazi podataka: " + ec.Message);
                }
                finally
                {
                    if (con != null && con.State == System.Data.ConnectionState.Open)
                        con.Close();

                    con = null;
                }
                // Debugger.Break();
                // MessageBox.Show("Ucitava se sema " + listBox1.SelectedIndex);
                forma.pomZaEDIT = 1;
            }

       
            else
            {
                MessageBox.Show("Nije selektovana nijedna sema!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)//ako je neka sema selektovana
            {
                //kod za proveru odluke :
                if (MessageBox.Show("Da li sigrno zelite da obrisete selektovanu semu?", "Pitanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                  //  MessageBox.Show("Brise!");
                    //kod za brisanje sledece seme iz baze : delete from sema where sema.id= listaId[listbox1.selectedItem];
                    //i onda opet se refreshuje prikaz;
                    int broj = listaId[listBox1.SelectedIndex];
                    OracleConnection con = null;
                    string conString = "Data Source = gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB; User Id = S16171; Password = S16171;";

                    try
                    {

                        con = new OracleConnection(conString);
                        con.Open();


                        string strSQL = "DELETE FROM KOMPONENTA WHERE ID_SEMA=:broj";
                        // string strSQL = "UPDATE  KOMPONENTA SET ID_SEMA= "+brsema.ToString()+" where ID_SEMA= "+sema.Idseme;

                        OracleCommand cmd = new OracleCommand(strSQL, con);
                        cmd.CommandType = System.Data.CommandType.Text;
                        OracleParameter paramBroj = new OracleParameter("broj", OracleDbType.Int32);
                        paramBroj.Value = broj;

                        //dodajemo parametar u listu
                        cmd.Parameters.Add(paramBroj);

                        //izvrsavamo komandu
                        cmd.ExecuteNonQuery();


                        strSQL = "DELETE FROM GRANA WHERE ID_SEMA=:broj";

                        //                   Debugger.Break();
                        cmd = new OracleCommand(strSQL, con);
                        cmd.CommandType = System.Data.CommandType.Text;

                        paramBroj = new OracleParameter("broj", OracleDbType.Int32);
                        paramBroj.Value = broj;

                        //dodajemo parametar u listu
                        cmd.Parameters.Add(paramBroj);

                        //izvrsavamo komandu
                        cmd.ExecuteNonQuery();


                        strSQL = "DELETE FROM CVOR WHERE ID_SEMA=:broj";
                        //     + " SET BROJ_DISKOVA = BROJ_DISKOVA + " + brojKopija.ToString()
                        //     + " WHERE BROJ = " + brojFilma.ToString();

                        cmd = new OracleCommand(strSQL, con);
                        cmd.CommandType = System.Data.CommandType.Text;

                        paramBroj = new OracleParameter("broj", OracleDbType.Int32);
                        paramBroj.Value = broj;

                        //dodajemo parametar u listu
                        cmd.Parameters.Add(paramBroj);

                        //izvrsavamo komandu
                        cmd.ExecuteNonQuery();


                        strSQL = "DELETE FROM SEMA WHERE ID=:broj";


                        cmd = new OracleCommand(strSQL, con);
                        cmd.CommandType = System.Data.CommandType.Text;

                        paramBroj = new OracleParameter("broj", OracleDbType.Int32);
                        paramBroj.Value = broj;

                        //dodajemo parametar u listu
                        cmd.Parameters.Add(paramBroj);

                        //izvrsavamo komandu
                        cmd.ExecuteNonQuery();

                        napuniListBox();
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
                else
                {
                    MessageBox.Show("Nije selektovana nijedna sema!", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
