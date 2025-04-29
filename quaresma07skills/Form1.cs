using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quaresma07skills
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        int urunid, turid, musteriid, satisid;
        dbMarketEntities1 db = new dbMarketEntities1();
        private void button1_Click(object sender, EventArgs e)
        {
            tblUrunler tbl = new tblUrunler();
            tbl.adi1 = txtadi.Text;
            tbl.durum = checkBox1.Checked;
            tbl.fiyati = int.Parse(txtfiyati.Text);
            tbl.turu = int.Parse(cmbTuru.SelectedValue.ToString());
            db.tblUrunler.Add(tbl);
            db.SaveChanges();
            Doldur();
            
        }   
        private void Doldur(){
            var urunler = db.tblUrunler.Select(x => new
            {


                id = x.id,
                ad = x.adi1,
                x.tblTurler.turAdi,
                x.durum,
                x.fiyati,

            }).ToList();
            cmburun.DataSource = urunler;
            cmburun.DisplayMember = "ad";
            cmburun.ValueMember = "id";
            dataGridView1.DataSource = urunler;

            var turler = db.tblTurler.Select(x => new
            {

                id = x.id,
                tur = x.turAdi


            }).ToList();
          
            cmbTuru.DataSource = turler;
            cmbTuru.DisplayMember = "tur";
            cmbTuru.ValueMember = "id";
            dataGridView2.DataSource = turler;
             
            var musteri = db.tblMusteriler.Select(x => new
        {


            id = x.id,
            adi1 = x.adi,
            x.tel

        }).ToList();
            cmbmusteri.DataSource = musteri;
            cmbmusteri.DisplayMember = "adi1";
            cmbmusteri.ValueMember = "id";
            dataGridView3.DataSource = musteri;

            var satis = db.tblSatis.Select(x => new
            {
                x.id,
                x.tblMusteriler.adi,
                x.tblUrunler.adi1,
                x.miktar,
                x.odenen



            }).ToList();
            dataGridView4.DataSource = satis;


        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            Doldur();
        }

        

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            urunid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var urun = db.tblUrunler.Where(x => x.id == urunid).FirstOrDefault();
            txtadi.Text = urun.adi1;
            cmbTuru.Text = urun.tblTurler.turAdi;
            txtfiyati.Text = urun.fiyati.ToString();
            checkBox1.Checked = (bool)urun.durum;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            tblTurler turler = new tblTurler();
            turler.turAdi = txtturadi.Text;
            db.tblTurler.Add(turler);
            db.SaveChanges();
            Doldur();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var sil = (from x in db.tblTurler where x.id == turid select x).FirstOrDefault();
            db.tblTurler.Remove(sil);
            db.SaveChanges();
            Doldur();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var guncelle = (from x in db.tblTurler where x.id == turid select x).FirstOrDefault();
            guncelle.turAdi = txtturadi.Text;
            db.SaveChanges();
            Doldur();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            turid = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            var tur = db.tblTurler.Where(x => x.id == turid).FirstOrDefault();
            txtturadi.Text = tur.turAdi;

        }

        private void txtara2_TextChanged(object sender, EventArgs e)
        {
            var ara = from x in db.tblTurler
                      where x.turAdi.Contains(txtara2.Text)
                      select new
                      {
                          x.id,
                          x.turAdi




                      };
            dataGridView2.DataSource = ara.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tblMusteriler musteri = new tblMusteriler();
            musteri.adi = txtmusteriadi.Text;
            musteri.tel = txttel.Text;
            db.tblMusteriler.Add(musteri);
            db.SaveChanges();
            Doldur();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var sil = (from x in db.tblMusteriler where x.id == musteriid select x).FirstOrDefault();
            db.tblMusteriler.Remove(sil);
            db.SaveChanges();
            Doldur();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var guncelle = (from x in db.tblMusteriler where x.id == musteriid select x).FirstOrDefault();
            guncelle.adi = txtmusteriadi.Text;
            guncelle.tel = txttel.Text;
            db.SaveChanges();
            Doldur();
        }

        private void txtara3_TextChanged(object sender, EventArgs e)
        {
            var ara = from x in db.tblMusteriler
                      where x.adi.Contains(txtara3.Text)
                      select new
                      {
                          x.id,
                          x.adi,
                         x.tel




                      };
            dataGridView3.DataSource = ara.ToList();
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
             musteriid = int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString());
            var muster = db.tblMusteriler.Where(x => x.id == musteriid).FirstOrDefault();
            txtmusteriadi.Text = muster.adi;
            txttel.Text = muster.tel;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tblSatis satis = new tblSatis();
            satis.musteri= int.Parse(cmbmusteri.SelectedValue.ToString());
            satis.urun= int.Parse(cmburun.SelectedValue.ToString());
            satis.miktar = int.Parse(txtmiktar.Text);
            satis.odenen = float.Parse(txtodenen.Text);
            db.tblSatis.Add(satis);
            db.SaveChanges();
            Doldur();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var guncelle = (from x in db.tblSatis where x.id == satisid select x).FirstOrDefault();
            guncelle.musteri= int.Parse(cmbmusteri.SelectedValue.ToString());
            guncelle.urun= int.Parse(cmburun.SelectedValue.ToString());
            guncelle.miktar = int.Parse(txtmiktar.Text);
            guncelle.odenen = int.Parse(txtodenen.Text);
            db.SaveChanges();
            Doldur();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var sil = (from x in db.tblSatis where x.id == satisid select x).FirstOrDefault();
            db.tblSatis.Remove(sil);
            db.SaveChanges();
            Doldur();
        }

        private void txtara4_TextChanged(object sender, EventArgs e)
        {
            var ara = from x in db.tblSatis
                      where x.tblMusteriler.adi.Contains(txtara4.Text)
                      select new
                      {
                          x.id,
                          x.tblMusteriler.adi,
                          x.tblUrunler.adi1,
                          x.miktar,
                          x.odenen





                      };
            dataGridView4.DataSource = ara.ToList();
        }

        private void dataGridView4_DoubleClick(object sender, EventArgs e)
        {
            satisid = int.Parse(dataGridView4.CurrentRow.Cells[0].Value.ToString());
            var satis = db.tblSatis.Where(x => x.id == satisid).FirstOrDefault();
            txtodenen.Text = satis.odenen.ToString();
            txtmiktar.Text = satis.miktar.ToString();
            cmbmusteri.Text = satis.tblMusteriler.adi;
            cmburun.Text = satis.tblUrunler.adi1;
        }

        private void txtara1_TextChanged(object sender, EventArgs e)
        {
            var ara = from x in db.tblUrunler
                      where x.adi1.Contains(txtara1.Text)
                      select new
                      {
                          x.id,
                          x.adi1,
                          x.tblTurler.turAdi,
                          x.durum,
                          x.fiyati




                      };
            dataGridView1.DataSource = ara.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sil = (from x in db.tblUrunler where x.id == urunid select x).FirstOrDefault();
            db.tblUrunler.Remove(sil);
            db.SaveChanges();
            Doldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var guncelle = (from x in db.tblUrunler where x.id == urunid select x).FirstOrDefault();
            guncelle.adi1 = txtadi.Text;
            guncelle.durum = checkBox1.Checked;
            guncelle.fiyati = int.Parse(txtfiyati.Text);
            guncelle.turu = int.Parse(cmbTuru.SelectedValue.ToString());
            db.SaveChanges();
            Doldur();
        }
    }
}
