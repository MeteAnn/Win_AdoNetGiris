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

namespace DisconnectedMimari
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=DESKTOP-C6NAGE9\\SQLEXPRESS;Database=Northwind;Integrated security=true");

        private void UrunListesi()
        {

            //Disconnected Mimari Yöntemi ile Veri Listeleme İşlemidir.
            SqlDataAdapter adp = new SqlDataAdapter("select * from Urunler", baglanti);

            DataTable dt = new DataTable(); //Bellekte verileri saklamak için kullanılan bir tablo yapısıdır. Veritabanından alınan veriler bu nesneye yüklenir.

            adp.Fill(dt);

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["UrunID"].Visible = false;
            dataGridView1.Columns["KategoriID"].Visible = false;
            dataGridView1.Columns["TedarikciID"].Visible = false;


        }
        private void Form1_Load(object sender, EventArgs e)
        {



            UrunListesi();
            


        }

        private void btnEkle_Click(object sender, EventArgs e)
        {


            string adi = txtUrunAdi.Text;
            decimal fiyat = nudFiyat.Value;
            decimal stok = nudFiyat.Value;

            if (adi==""||fiyat==null||stok==null)
            {

                MessageBox.Show("Lütfen Geçerli Bir Değer Giriniz");

            }

            SqlCommand komut = new SqlCommand();
            komut.CommandText = string.Format("insert Urunler (UrunAdi, BirimFiyati, HedefStokDuzeyi) values('{0}', {1}, {2})",adi,fiyat,stok);

            komut.Connection = baglanti;

            baglanti.Open();


            int etkilenen = komut.ExecuteNonQuery();
            if (etkilenen>0 && fiyat>0 && stok > 0)
            {

                MessageBox.Show("Kayıt Başarılı bir şeklde eklendi!");
                UrunListesi();

            }
            else
            {
                MessageBox.Show("Kayıt başarısızdır.");
            }

            baglanti.Close();

            



        }

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            KategoriForm kf = new KategoriForm();
            kf.ShowDialog();




        }
    }
}
