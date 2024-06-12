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
    public partial class KategoriForm : Form
    {
        public KategoriForm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=DESKTOP-C6NAGE9\\SQLEXPRESS;Database=Northwind;Integrated security=true");



        private void KategoriForm_Load(object sender, EventArgs e)
        {


            KategoriListesi();



        }



        private void KategoriListesi()
        {

            SqlDataAdapter adp = new SqlDataAdapter("Select * from Kategoriler",baglanti);

            DataTable dt = new DataTable();

            adp.Fill(dt);   

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["KategoriID"].Visible=false;


        }

        private void btnEkle2_Click(object sender, EventArgs e)
        {

            

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = string.Format("insert Kategoriler (KategoriAdi, Tanimi) values ('{0}', '{1}')",txtKategoriAdi.Text,txtTanimi.Text);
            cmd.Connection = baglanti;

            baglanti.Open();

            int etk = cmd.ExecuteNonQuery();

            baglanti.Close();

            if (etk>0)
            {

                MessageBox.Show("Kayıt Eklenmiştir.");
                KategoriListesi();
            }
            else
            {

                MessageBox.Show("Kayıt Eklenemedi Hata Oluştur");


            }


        }
    }
}
