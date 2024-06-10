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

namespace AdoTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        SqlConnection baglanti = new SqlConnection();

        private void LoadPr()
        {


            baglanti.ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ETrade;Integrated Security=true;";

            SqlDataAdapter dta = new SqlDataAdapter("Select * from Products", baglanti);

            DataTable dt = new DataTable();

            dta.Fill(dt);

            dataGridView1.DataSource = dt;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            
            baglanti.ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ETrade;Integrated Security=true;";

            SqlDataAdapter dta = new SqlDataAdapter("Select * from Products",baglanti);

            DataTable dt = new DataTable();

            dta.Fill(dt);

            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {


            if(dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz bir satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int Id = (int)row.Cells["Id"].Value; // Burada "ProductId" DataGridView'deki ürün ID sütununun adı olmalıdır
                Sil(Id);
            }

            LoadPr();   
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {






        }


        private void Sil(int Id)
        {


            string sorgu = "DELETE FROM Products WHERE Id = @id";

            // SqlCommand oluşturma
            SqlCommand komut = new SqlCommand(sorgu, baglanti);

            // Parametre ekleme
            komut.Parameters.AddWithValue("@id", Id);

            // Bağlantıyı açma
            baglanti.Open();

            // Komutu çalıştırma
            komut.ExecuteNonQuery();

            // Bağlantıyı kapatma
            baglanti.Close();


        }

    }
}
