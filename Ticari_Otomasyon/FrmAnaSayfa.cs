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
using DevExpress.XtraBars.Ribbon.BackstageView.Accessible;
using System.Xml;
using DevExpress.CodeParser;

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

        void FirmaHareketleri()
        {
            SqlDataAdapter da = new SqlDataAdapter("execute FirmaHareket2", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlFirmaHareket.DataSource = dt;
        }

        void fihrist ()
        {
            SqlDataAdapter da = new SqlDataAdapter("select ad,telefon1 from tbl_Fırmalar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlFihrist.DataSource = dt;
        }

        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if(xmloku.Name =="title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }
       
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();

            ajanda();

            FirmaHareketleri();

            fihrist();

            webBrowser1.Navigate("https://tcmb.gov.tr/kurlar/today.xml");

            haberler();
        }
    }
}
