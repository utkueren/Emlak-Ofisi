using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Emlak_Ofisi.Models
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext> 
    {
        protected override void Seed(DataContext context)
        {
            var sehir = new List<Sehir>()
            {
                new Sehir() {SehirAd="İzmir"},
                new Sehir() {SehirAd="İstanbul"},
                 new Sehir() {SehirAd="Düzce"}
            };
            foreach (var item in sehir)
            {
                context.Sehirs.Add(item);
            }
            context.SaveChanges();
            var semt = new List<Semt>()
            {
                new Semt() {SemtAd="Narlıdere",SehirId=1},
                new Semt() {SemtAd="Fatih",SehirId=2},
                new Semt() {SemtAd="Merkez",SehirId=3}
            };
            foreach (var item in semt)
            {
                context.Semts.Add(item);
            }

            context.SaveChanges();

            var mahalle = new List<Mahalle>()
            {
                new Mahalle() {MahalleAd="Çamtepe",SemtId=1},
                new Mahalle() {MahalleAd="Akşemsettin",SemtId=2},
                new Mahalle() {MahalleAd="Kalıcı Konutlar",SemtId=3},
            };
            foreach (var item in mahalle)
            {
                context.Mahalles.Add(item);
            }
            context.SaveChanges();

            var durum = new List<Durum>()
            {
                new Durum() {DurumAd="Kiralık"},
                new Durum() {DurumAd="Satılık"}
            };
            foreach (var item in durum)
            {
                context.Durums.Add(item);
            }
            context.SaveChanges();

            var tip = new List<Tip>()
            {
                new Tip() {TipAd="Ev", DurumId=1},
                new Tip() {TipAd="Dükkan", DurumId=1},
                new Tip() {TipAd="Ev", DurumId=2},
                new Tip() {TipAd="Dükkan", DurumId=2}
            };
            foreach (var item in tip)
            {
                context.Tips.Add(item);
            }
            context.SaveChanges();

            var ilan = new List<İlan>()
            {
                new İlan() {Açıklama="Sadece öğrenciye kiralık", Adres="Kayhan Sokak", OdaSayisi=4, BanyoSayisi=2, Kredi=true, Fiyat=1500, MahalleId=1, SemtId=1, SehirId=1, DurumId=1, TipId=1, Alan=150, Telefon="5532306918", Kat="1.Kat", UserName="UtkuEren"},
                new İlan() {Açıklama="Sahibinden temiz dükkan", Adres="Ata Sokak", OdaSayisi=2, BanyoSayisi=1, Kredi=true, Fiyat=3000, MahalleId=2, SemtId=2, SehirId=2, DurumId=2, TipId=4, Alan=250, Telefon="5532306918", Kat="3.Kat", UserName="UtkuEren"}
            };
            foreach (var item in ilan)
            {
                context.İlans.Add(item);
            }
            context.SaveChanges();

            var resim = new List<Resim>()
            {
                new Resim(){ResimAd="1.jpg", İlanId=1},
                new Resim(){ResimAd="2.jpg", İlanId=1},
                new Resim(){ResimAd="3.jpg", İlanId=1},
                new Resim(){ResimAd="4.jpg", İlanId=2},
                new Resim(){ResimAd="5.jpg", İlanId=2},
                new Resim(){ResimAd="6.jpg", İlanId=2},
            };
            foreach (var item in resim)
            {
                context.Resims.Add(item);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}