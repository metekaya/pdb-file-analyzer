using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeteKaya
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            string dosyaKonumu_Samsun;
            dosyaKonumu_Samsun = @"C:\Users\hasan\OneDrive\Desktop\protein\2bhh.pdb";   // Dosya Konumu.
            string aranacakString = @"ATOM";          // ATOM anahtar kelimesi ile başlayan satirlari okutmamizi saglayacak method icin tanimlama yapiyoruz.

            string baslik_bul_tazefasulye = File.ReadLines(dosyaKonumu_Samsun).ElementAtOrDefault(1); // Dosyadaki protein ismini arayan degisken.
            var protein_ad_penguen = string.Join(" ", baslik_bul_tazefasulye.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()).Skip(1)); // TITLE kelimesini atlayan ve
                                                                                                                                                                        // direkt basligi okuyan method.
            label1.Text = protein_ad_penguen;
            



            List<Atom> Atomlar = File.ReadAllLines(dosyaKonumu_Samsun)       // Burada butun satirlari, olusturdugumuz 'Atomlar' listesine okutuyoruz.
            .Where(satir => satir.StartsWith(aranacakString))                // ATOM kelimesi ile baslayan satirlari buluyoruz.
            .Select(satir =>                                                 // O satirlari seciyoruz.
            {
                // Burada secilen satirlari bosluk karakterinden sonra ayiriyoruz ve olusturdugumuz diziye atiyoruz
                var diziKocaeli = satir.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);  //fazladan boslukları RemoveEmptyEntries fonksiyonu ile siliyoruz.

                // Her satir icin dizi degiskenlerine uygun sekilde yeni bir Atom donduruyoruz.
                return diziKocaeli.Length < 12 // Dizide 12 eleman oldugunu goster.
                    ? null                     // 12 eleman yoksa null dondur.
                    : new Atom                 // 12 eleman olduysa yeni Atom dizisi olustur.
                    {
                        atom_anahtarKelimesi = diziKocaeli[0],
                        atom_no = int.Parse(diziKocaeli[1]),
                        atom_ad = diziKocaeli[2],
                        amino_ad = diziKocaeli[3],
                        zincir = char.Parse(diziKocaeli[4]),
                        amino_no = int.Parse(diziKocaeli[5]),
                        x_koordinat = float.Parse(diziKocaeli[6]),
                        y_koordinat = float.Parse(diziKocaeli[7]),
                        z_koordinat = float.Parse(diziKocaeli[8]),
                        oran = float.Parse(diziKocaeli[9]),
                        sicaklik = float.Parse(diziKocaeli[10]),
                        odevAciklamasindaOlmayanAtom = char.Parse(diziKocaeli[11])
                    };


            })
            .ToList();   //En son ise uygun sekilde parcalanmis elemanlara sahip dizimizi listemize ekliyoruz.



            //Burada ki 'atomGebze' degiskeni X koordinati en büyük olan Atomun butun bilgilerine erismemizi sagliyor.

            Atom atomGebze = Atomlar.OrderByDescending(a => a.x_koordinat).FirstOrDefault(); // X koordinati en buyuk olan atomu Azalan Sekilde Sirala yontemi ile buluyoruz. 

            label2.Text = Convert.ToDecimal(atomGebze.x_koordinat).ToString("#,##0");   // label'a float(x_koordinat) tarzinda bir degiskeni uygun string formatinda yazdirma.

            //atomGebze ile ilgili satirda ihtiyacimiz olan bilgileri yazdirabiliriz.
            label3.Text = atomGebze.atom_ad;             // X koordinati en buyuk olan atomun adi.
            label4.Text = atomGebze.atom_no.ToString();  // X koordinati en buyuk olan atomun numarasi.


            int enBuyuk_X_Besiktas = Atomlar.Max(maxX => maxX.atom_no);  // Toplam kac atoma sahip oldugumuzu Max methodu ile buluyoruz.
            label5.Text = Convert.ToString(enBuyuk_X_Besiktas);
            
            
            label11.Text = textBox1.Text;

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
