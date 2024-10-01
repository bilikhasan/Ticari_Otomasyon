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
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void stoklar()
        {
            SqlDataAdapter da = new SqlDataAdapter("select urunad, sum(adet) as'Adet' from TBL_URUNLER group by URUNAD having sum(adet)<=20 order by sum(adet)", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridControlStoklar.DataSource = dt;
        }


        void ajanda()
        {
            SqlDataAdapter da = new SqlDataAdapter("select top 10 tarıh,saat,baslık from tbl_notlar order by ıd desc", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridControlAjanda.DataSource = dt;
        }



        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();

            ajanda();
        }

    }
}
