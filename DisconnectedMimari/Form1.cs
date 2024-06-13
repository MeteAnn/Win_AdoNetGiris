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

        //Integrated Security : Windows Authentication ile server'a bağlanmayı sağlar.


        //SqlConnection baglanti = new SqlConnection("Server=DESKTOP-C6NAGE9\\SQLEXPRESS;Database=Northwind; User=sa; Pwd:as");





        private void UrunListesi()
        {

            //Disconnected Mimari Yöntemi ile Veri Listeleme İşlemidir.
            SqlDataAdapter adp = new SqlDataAdapter("select * from Urunler", baglanti);

            DataTable dt = new DataTable(); //Bellekte verileri saklamak için kullanılan bir tablo yapısıdır. Veritabanından alınan veriler bu nesneye yüklenir.

               
            //Tek Select sorgusu çalıştıracaksak DataSet'e gerek yoktur.


            adp.Fill(dt); //Buradaki Fill doldur demektir. Yani gelen tablo çıktısını doldur diyoruz.

            dataGridView1.DataSource = dt;  //Burada Veri Kaynağına tabloyu olduğu gibi veriyoruz.

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
                return; //burayı girmemizin sebebi uyarı verip dönmesi gerek yoksa aşağı girer çalışmaya devam eder.return ifadesi, geçersiz değerler girildiğinde metodu sonlandırmak için kullanılır. 
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //datagridview'Dan seçili satırı alma işlemi

            txtUrunAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            nudFiyat.Value = (decimal)dataGridView1.CurrentRow.Cells[5].Value;

            nudStok.Value = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[6].Value);





        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText ="Update Urunler set UrunAdi=@UrunAdi, BirimFiyati=@BirimFiyati,HedefStokDuzeyi=@HedefStokDuzeyi where UrunID=@UrunID";

            cmd.Connection = baglanti;

            cmd.Parameters.AddWithValue("@UrunID",id);
            cmd.Parameters.AddWithValue("@UrunAdi",txtUrunAdi.Text);
            cmd.Parameters.AddWithValue("@BirimFiyati",nudFiyat.Value);
            cmd.Parameters.AddWithValue("@HedefStokDuzeyi",nudStok.Value);

            baglanti.Open();

            



            try
            {
                int etk = cmd.ExecuteNonQuery();
                baglanti.Close();
                if(etk > 0 && !string.IsNullOrEmpty(txtUrunAdi.Text))
            {


                    MessageBox.Show("Kayıt Güncellenmiştir.");
                    UrunListesi();
                    return;
                }
            else
                {
                    MessageBox.Show("Kayıt Güncellenirken Hata Oluştu");
                }
            }
            catch (Exception ex)
            {
                baglanti.Close();
                MessageBox.Show("Hata",ex.Message);
                
            }


            

            

            




        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow !=null)
            {

                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                SqlCommand cmd = new SqlCommand(string.Format("delete Urunler where UrunID={0}", id), baglanti);
                baglanti.Open();

                int etk = cmd.ExecuteNonQuery();

                baglanti.Close();

                if (etk>0)
                {


                    MessageBox.Show("Kayıt Silinmiştir");
                    UrunListesi();

                }
                else
                {
                    MessageBox.Show("Kayıt Silinirken Hata Oluşmuştur.");
                }


            }

            



        }
    }
}
