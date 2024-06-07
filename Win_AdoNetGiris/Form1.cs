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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

                SqlConnection baglanti = new SqlConnection();

                //baglanti.ConnectionString = "Server=localhost;Database=KuzeyYeli;User=sa;Pwd=123"; //Sql Authentication ile bağlanma

                baglanti.ConnectionString = "Server=DESKTOP-C6NAGE9\\SQLEXPRESS;Database=Northwind;Integrated Security=true;"; //Windows Authentication ile bağlanma

                SqlCommand komut = new SqlCommand();

                komut.CommandText = " Select * from Urunler";
                komut.Connection = baglanti;

                baglanti.Open();

                SqlDataReader rdr = komut.ExecuteReader();

                while (rdr.Read())
                {


                    string adi = rdr["UrunAdi"].ToString();
                    string fiyat = rdr["BirimFiyati"].ToString();
                    string stok = rdr["HedefStokDuzeyi"].ToString();


                    ListUrunler.Items.Add(string.Format("{0}--{1}--{2}", adi, fiyat, stok));


                }

                baglanti.Close();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            KategoriForm kf = new KategoriForm();
            kf.ShowDialog();
        }
    }
}
