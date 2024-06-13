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
    public partial class KategoriForm : Form
    {
        public KategoriForm()
        {
            InitializeComponent();
        }

        //StoreProcedure Kullanılarak Listeleme işlemi

        SqlConnection baglanti = new SqlConnection("Server=DESKTOP-C6NAGE9\\SQLEXPRESS;Database=Northwind;Integrated security=true");


        
        private void KategoriForm_Load(object sender, EventArgs e)
        {

            KategoriListele();


        }


        private void KategoriListele()
        {

            SqlDataAdapter adp = new SqlDataAdapter("prc_KategoriListele",baglanti);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();

            adp.Fill(dt);

            dataGridView1.DataSource = dt;  

        

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("prc_KategoriEkle",baglanti);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@adi", txtKategori.Text);
            cmd.Parameters.AddWithValue("@tanim", txtTanim.Text);

            baglanti.Open();

            int etk = cmd.ExecuteNonQuery();

            baglanti.Close();

            if (etk>0)
            {

                MessageBox.Show("Kategori Eklenmiştir");
                KategoriListele();
            }

            else
            {
                MessageBox.Show("Kategori Hata");

            }

        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow !=null) //seçili satır null değilse
            {

          

            SqlCommand cmd = new SqlCommand("prc_KategoriSil", baglanti);
            cmd.CommandType= CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@kID", dataGridView1.CurrentRow.Cells[0].Value);

            baglanti.Open();
            int etk = cmd.ExecuteNonQuery();
            baglanti.Close();



            if (etk>0)
            {

                MessageBox.Show("Kategori Silinmiştir.");
                KategoriListele();

            }
            else
            {
                MessageBox.Show("Kategori Hata");
            }

            }



        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
         






        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("prc_KategoriGuncelle", baglanti);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("kID", dataGridView1.CurrentRow.Cells[0].Value);
            cmd.Parameters.AddWithValue("@Kadi",txtKategori.Text);
            cmd.Parameters.AddWithValue("@Tanim",txtTanim.Text);

            baglanti.Open();
            int etk = cmd.ExecuteNonQuery();
            baglanti.Close();


            if (etk>0)
            {

                MessageBox.Show("Güncellendi");
                KategoriListele();

            }
            else
            {
                MessageBox.Show("Hata Update");
            }

        }
    }
}
