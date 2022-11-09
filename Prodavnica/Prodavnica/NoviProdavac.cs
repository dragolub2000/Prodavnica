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
    public partial class NoviProdavac : Form
    {
        // Mode=0 novi prodavac, Mode=1 edit prodavca
        public int Mode = 0;
        private int ProdavacId;
        public NoviProdavac()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void NoviProdavac_Load(object sender, EventArgs e)
        {
            this.dtp.CustomFormat = "yyyy-MM-dd";
            this.dtp.Format = DateTimePickerFormat.Custom;
            if(Mode == 0)
            {
                this.btnOk.Text = "Dodaj";
            }
            else
            {
                this.btnOk.Text = "Izmeni";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mode == 0)
                {
                    this.unesiNovogProdavca();
                }
                else
                {
                    this.izmeniProdavca(this.ProdavacId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void unesiNovogProdavca()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0;ReadOnly=False;HDR=Yes;\"";
            int id = Int32.Parse(txtID.Text);
            string Ime = txtIme.Text;
            string Prezime = txtPrezime.Text;
            string dr = this.dtp.Text;
            string Telefon = txtTelefon.Text;
            string querystring = "INSERT INTO Prodavci (id,Ime,Prezime,DatumRodjenja,Telefon)  VALUES (@id,@Ime,@Prezime,@dr,@Telefon)";

            conn.Open();
            OleDbCommand cmd = new OleDbCommand(querystring, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@Ime", Ime);
            cmd.Parameters.AddWithValue("@Prezime", Prezime);
            cmd.Parameters.AddWithValue("@dr", dr);
            cmd.Parameters.AddWithValue("@Telefon", Telefon);
            // izvrsi sql upit
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Prodavac uspesno unet u bazu podataka!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void izmeniProdavca(int ProdavacId)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0;ReadOnly=False;HDR=Yes;\"";
                int id = Int32.Parse(txtID.Text);
                string Ime = txtIme.Text;
                string Prezime = txtPrezime.Text;
                string dr = this.dtp.Text;
                string Telefon = txtTelefon.Text;
                string querystring = "UPDATE Prodavci SET Ime=@Ime,Prezime=@Prezime,DatumRodjenja=@DatumRodjenja,Telefon=@Telefon WHERE id=" + ProdavacId;
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(querystring, conn);
               
                cmd.Parameters.AddWithValue("@Ime", Ime);
                cmd.Parameters.AddWithValue("@Prezime", Prezime);
                cmd.Parameters.AddWithValue("@dr", dr);
                cmd.Parameters.AddWithValue("@Telefon", Telefon);
                // izvrsi sql upit
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Podaci o prodavci uspesno izmenjeni!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
        }
        public void prikaziProdavca(int ProdavacId)
        {
            try
            {
                this.ProdavacId = ProdavacId;
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0\"";
                String strSQL = "Select * from Prodavci where id=" + ProdavacId + ";";
                OleDbCommand newComm = new OleDbCommand(strSQL, conn);
                OleDbDataReader reader;
                conn.Open();
                reader = newComm.ExecuteReader();
                int i = 0;

                while (reader.Read())
                {
                    this.txtID.Text = reader["id"].ToString();
                    this.txtIme.Text = reader["Ime"].ToString();
                    this.txtPrezime.Text = reader["Prezime"].ToString();
                    this.txtTelefon.Text = reader["Telefon"].ToString();
                    this.dtp.Text = reader["DatumRodjenja"].ToString();
                    i++;
                }
                conn.Close();
                if (i != 1)
                {
                    MessageBox.Show("Postoji greska u tabeli Prodavci!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
        }
    }
    
}
