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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.prikaziProdavce();
                this.prikaziArtikle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
        }
        private void prikaziProdavce()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0\"";
            String strSQL = "Select * from Prodavci;";
            OleDbCommand newComm = new OleDbCommand(strSQL, conn);
            OleDbDataReader reader;
            conn.Open();
            reader = newComm.ExecuteReader();
            int i = 0;
            this.lvProdavci.Items.Clear();
            while (reader.Read())
            {
                this.lvProdavci.Items.Add(reader["id"].ToString());

                this.lvProdavci.Items[i].SubItems.Add(reader["Ime"].ToString());
                this.lvProdavci.Items[i].SubItems.Add(reader["Prezime"].ToString());
                i++;
            }
            conn.Close();
        }
        private void prikaziArtikle()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0\"";
            String strSQL = "Select * from Artikli;";
            OleDbCommand newComm = new OleDbCommand(strSQL, conn);
            OleDbDataReader reader;
            conn.Open();
            reader = newComm.ExecuteReader();
            int i = 0;
            this.lvArtikli.Items.Clear();
            while (reader.Read())
            {
                this.lvArtikli.Items.Add(reader["Sifra"].ToString());

                this.lvArtikli.Items[i].SubItems.Add(reader["Naziv"].ToString());
                this.lvArtikli.Items[i].SubItems.Add(reader["Mera"].ToString());
                i++;
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NoviProdavac f = new NoviProdavac();
            // novi prodavac
            f.Mode = 0;
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                f.Dispose();
                this.Form1_Load(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NoviArtikl f = new NoviArtikl();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                f.Dispose();
                this.Form1_Load(sender, e);
            }
        }

        private void lvProdavci_DoubleClick(object sender, EventArgs e)
        {
            var item = this.lvProdavci.SelectedItems[0];
            NoviProdavac f = new NoviProdavac();
            f.Mode = 1;
            f.prikaziProdavca(Int32.Parse(item.SubItems[0].Text));
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                f.Dispose();
                this.Form1_Load(sender, e);
            }
        }

        private void lvArtikli_DoubleClick(object sender, EventArgs e)
        {
            var item = this.lvArtikli.SelectedItems[0];
            NoviArtikl f = new NoviArtikl();
            f.Mode = 1;
            f.prikaziArtikl(item.SubItems[0].Text);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                f.Dispose();
                this.Form1_Load(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IzaberiProdavca f = new IzaberiProdavca();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                f.Dispose();
                this.Form1_Load(sender, e);
            }
        }
    }
}
