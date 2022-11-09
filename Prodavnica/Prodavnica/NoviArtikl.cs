using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Prodavnica
{
    public partial class NoviArtikl : Form
    {
        // Mode=0 novi artikl, Mode=1 edit artikla
        public int Mode = 0;
        private string ArtiklId;
        public NoviArtikl()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void NoviArtikl_Load(object sender, EventArgs e)
        {
            if (Mode == 0)
            {
                this.btnOk.Text = "Dodaj";
            }
            else
            {
                this.btnOk.Text = "Izmeni";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Mode == 0)
            {
                this.unesiNoviArtikl();
            }
            else
            {
                this.izmeniArtikl(this.ArtiklId);
            }
        }
        private void unesiNoviArtikl()
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0;ReadOnly=False;HDR=Yes;\"";
                string Sifra = txtSifra.Text;
                string Naziv = txtNaziv.Text;
                string Mera = txtMera.Text;

                string querystring = "INSERT INTO Artikli (Sifra,Naziv,Mera)  VALUES (@Sifra,@Naziv,@Mera)";

                conn.Open();
                OleDbCommand cmd = new OleDbCommand(querystring, conn);

                cmd.Parameters.AddWithValue("@Sifra", Sifra);
                cmd.Parameters.AddWithValue("@Naziv", Naziv);
                cmd.Parameters.AddWithValue("@Mera", Mera);

                // izvrsi sql upit
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Artikl uspesno unet u bazu podataka!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void izmeniArtikl(String ArtiklId)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0;ReadOnly=False;HDR=Yes;\"";
                string Sifra = txtSifra.Text;
                string Naziv = txtNaziv.Text;
                string Mera = txtMera.Text;

                string querystring = "UPDATE Artikli SET Sifra=@Sifra,Naziv=@Naziv,Mera=@Mera WHERE Sifra='" + ArtiklId+"' ;";

                conn.Open();
                OleDbCommand cmd = new OleDbCommand(querystring, conn);

                cmd.Parameters.AddWithValue("@Sifra", Sifra);
                cmd.Parameters.AddWithValue("@Naziv", Naziv);
                cmd.Parameters.AddWithValue("@Mera", Mera);

                // izvrsi sql upit
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Artikl uspesno izmenjen u bazi podataka!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void prikaziArtikl(string ArtiklId)
        {
            try
            {
                // setovanje artikla za editovanje
                this.ArtiklId = ArtiklId;
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0\"";
                String strSQL = "Select * from Artikli where Sifra='" + ArtiklId + "';";
                OleDbCommand newComm = new OleDbCommand(strSQL, conn);
                OleDbDataReader reader;
                conn.Open();
                reader = newComm.ExecuteReader();
                int i = 0;

                while (reader.Read())
                {
                    this.txtSifra.Text = reader["Sifra"].ToString();
                    this.txtNaziv.Text = reader["Naziv"].ToString();
                    this.txtMera.Text = reader["Mera"].ToString();
                    
                    i++;
                }
                conn.Close();
                if (i != 1)
                {
                    MessageBox.Show("Postoji greska u tabeli Artikli!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
        }
    }
}
