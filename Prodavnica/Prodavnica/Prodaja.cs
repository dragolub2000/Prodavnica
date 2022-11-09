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

namespace Prodavnica
{
    public partial class Prodaja : Form
    {
        public int Prodavac_id;
        private List<string> Artikli = new List<string>();
        private string izabranaSifra;
        public Prodaja()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Prodaja_Load(object sender, EventArgs e)
        {
            this.prikaziArtikle();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0;ReadOnly=False;HDR=Yes;\"";

                string querystring = "INSERT INTO Promet (Artikl_id,Kolicina,Prodavac_id)  VALUES (@Artikl_id,@Kolicina,@Prodavac_id)";

                conn.Open();
                OleDbCommand cmd = new OleDbCommand(querystring, conn);

                cmd.Parameters.AddWithValue("@Artikl_id", this.izabranaSifra);
                cmd.Parameters.AddWithValue("@Kolicina", Int32.Parse(this.txtKolicina.Text));
                cmd.Parameters.AddWithValue("@Prodavac_id", this.Prodavac_id);

                // izvrsi sql upit
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Podatak o prometu uspesno unet u bazu podataka!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void prikaziArtikle()
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0\"";
                String strSQL = "Select * from Artikli;";
                OleDbCommand newComm = new OleDbCommand(strSQL, conn);
                OleDbDataReader reader;
                conn.Open();
                reader = newComm.ExecuteReader();
                int i = 0;
                this.cbArtikli.Items.Clear();
                this.Artikli.Clear();
                while (reader.Read())
                {
                    this.Artikli.Add(reader["Sifra"].ToString());
                    this.cbArtikli.Items.Add(reader["Naziv"].ToString());
                    i++;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
        }

        private void cbArtikli_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.cbArtikli.SelectedIndex;
            if (selectedIndex == -1)
                return;
            this.izabranaSifra = this.Artikli[selectedIndex].ToString();
        }
    }
}
