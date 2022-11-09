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
    public partial class IzaberiProdavca : Form
    {
        private string punoImeProdavca;
        private int Prodavac_id;
        public IzaberiProdavca()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void IzaberiProdavca_Load(object sender, EventArgs e)
        {

            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\Prodavnica.xls;Extended Properties=\"Excel 8.0\"";
                String strSQL = "Select * from Prodavci;";
                OleDbCommand newComm = new OleDbCommand(strSQL, conn);
                OleDbDataReader reader;
                conn.Open();
                reader = newComm.ExecuteReader();
                int i = 0;
                this.lvIzaberiProdavca.Items.Clear();
                while (reader.Read())
                {
                    this.lvIzaberiProdavca.Items.Add(reader["id"].ToString());

                    this.lvIzaberiProdavca.Items[i].SubItems.Add(reader["Ime"].ToString());
                    this.lvIzaberiProdavca.Items[i].SubItems.Add(reader["Prezime"].ToString());
                    i++;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
        }

        private void lvIzaberiProdavca_DoubleClick(object sender, EventArgs e)
        {
            this.btnNext.Enabled = true;
            var item = this.lvIzaberiProdavca.SelectedItems[0];
            this.punoImeProdavca = item.SubItems[1].Text + " " + item.SubItems[2].Text;
            this.Prodavac_id = Int32.Parse(item.SubItems[0].Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Prodaja f = new Prodaja();
            f.Text = this.punoImeProdavca;
            f.Prodavac_id = this.Prodavac_id;
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                f.Dispose();
            }
        }

        private void lvIzaberiProdavca_Click(object sender, EventArgs e)
        {
            this.btnNext.Enabled = true;
            var item = this.lvIzaberiProdavca.SelectedItems[0];
            this.punoImeProdavca = item.SubItems[1].Text + " " + item.SubItems[2].Text;
            this.Prodavac_id = Int32.Parse(item.SubItems[0].Text);
        }
    }
}
