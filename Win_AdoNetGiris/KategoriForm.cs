using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win_AdoNetGiris
{
    public partial class KategoriForm : Form
    {
        public KategoriForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           



        }

        private void KategoriForm_Load(object sender, EventArgs e)
        {

            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-C6NAGE9\\SQLEXPRESS;Database=Northwind;Integrated Security=true;");


            SqlCommand komut = new SqlCommand("Select * from Kategoriler", baglanti);

            baglanti.Open();

            SqlDataReader rdr = komut.ExecuteReader();

            while (rdr.Read())
            {


                string adi = rdr["KategoriAdi"].ToString();
                string tanimi = rdr["Tanimi"].ToString();

                listBox1.Items.Add(string.Format("{0}--{1}",adi,tanimi));

            }


            baglanti.Close();

        }
    }
}
