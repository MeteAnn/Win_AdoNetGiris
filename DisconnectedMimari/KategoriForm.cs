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

            baglanti.Open();


            SqlCommand komut = new SqlCommand("insert Kategoriler (KategoriAdi, Tanimi) values(@KategoriAdi, @Tanimi)", baglanti);

            komut.Parameters.AddWithValue("@KategoriAdi", txtKategoriAdi.Text);
            komut.Parameters.AddWithValue("@Tanimi",txtTanimi.Text);

            komut.ExecuteNonQuery();


            baglanti.Close();

            MessageBox.Show("Ürün Eklendi");

            KategoriListesi();


        }
    }
}
