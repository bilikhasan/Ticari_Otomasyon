using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Ticari_Otomasyon
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_ADMIN",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }


        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void Btnislem_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_ADMIN VALUES (@P1, @P2)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtKullaniciAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
    }
}
