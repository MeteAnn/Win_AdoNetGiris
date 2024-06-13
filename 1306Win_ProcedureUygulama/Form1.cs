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

namespace _1306Win_ProcedureUygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=DESKTOP-C6NAGE9\\SQLEXPRESS;Database=Northwind;Integrated security=true");


        private void btnEkle_Click(object sender, EventArgs e)
        {


            string adi = txtUrunAdi.Text;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Insert Urunler (UrunAdi,BirimFiyati,HedefStokDuzeyi) values(@UrunAdi,@BirimFiyati,@HedefStokDuzeyi)";
            cmd.Connection = baglanti;

            cmd.Parameters.AddWithValue("@UrunAdi",adi);
            cmd.Parameters.AddWithValue("@BirimFiyati",NumFiyat.Value);
            cmd.Parameters.AddWithValue("@HedefStokDuzeyi",NumStok.Value);

            baglanti.Open();
            int etk = cmd.ExecuteNonQuery();
            baglanti.Close();

            if (etk>0 && !string.IsNullOrEmpty(txtUrunAdi.Text))
            {

                MessageBox.Show("Kayıt Başarıyla Eklenmiştir");
                UrunListesi();
              
            }
            else
            {
                MessageBox.Show("Ürün Eklenirken Sorun Oluşmuştur.");
            }


            
    





        }


        private void UrunListesi()
        {

            SqlDataAdapter adp = new SqlDataAdapter("Select * from Urunler",baglanti);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            dataGridView1.DataSource = dt;


        }



        private void Form1_Load(object sender, EventArgs e)
        {


            UrunListesi();



        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            




        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Delete Urunler where UrunID=@urunıd";
                cmd.Connection = baglanti;

                cmd.Parameters.AddWithValue("@urunıd", dataGridView1.CurrentRow.Cells[0].Value);

                baglanti.Open();
                int etk = cmd.ExecuteNonQuery();
                baglanti.Close();


                if (etk > 0)
                {

                    MessageBox.Show("Kayıt Silinmiştir");
                    UrunListesi();
                }
                else
                {
                    MessageBox.Show("Silinemedi Tekrar Deneyin");
                }



            }
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {

            KategoriForm kt = new KategoriForm();

            kt.ShowDialog();


        }
    }
}
