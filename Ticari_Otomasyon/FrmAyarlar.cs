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
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_ADMIN", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }


        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            TxtKullaniciAd.Text = "";
            TxtSifre.Text = "";
        }

        private void Btnislem_Click(object sender, EventArgs e)
        {
            if (Btnislem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBL_ADMIN VALUES (@P1, @P2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtKullaniciAd.Text);
                komut.Parameters.AddWithValue("@P2", TxtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }

            if(Btnislem.Text=="Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("UPDATE TBL_ADMIN SET sifre=@P2 WHERE kullaniciad=@P1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@P1", TxtKullaniciAd.Text);
                komut1.Parameters.AddWithValue("@P2", TxtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtKullaniciAd.Text = dr["KullaniciAd"].ToString();
                TxtSifre.Text = dr["Sifre"].ToString();
            }
        }

        private void TxtKullaniciAd_TextChanged(object sender, EventArgs e)
        {
            if (TxtKullaniciAd.Text != "")
            {
                Btnislem.Text = "Güncelle";
                Btnislem.BackColor = Color.GreenYellow;
            }
            else
            {
                Btnislem.Text = "Kaydet";
                Btnislem.BackColor = Color.MediumTurquoise;
            }
        }
    }
}
