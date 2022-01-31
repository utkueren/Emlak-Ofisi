using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emlak_Ofisi.Models
{
    public class İlan
    {
        public int İlanId { get; set; }
        public string Açıklama { get; set; }
        public double Fiyat { get; set; }
        public int OdaSayisi { get; set; }
        public int BanyoSayisi { get; set; }
        public bool Kredi { get; set; } //krediye uygun mu değil mi
        public int Alan { get; set; }
        public string Kat { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string UserName { get; set; }
        public int SehirId { get; set; }
        public int SemtId { get; set; }
        public int DurumId { get; set; }
        public int MahalleId { get; set; }

        public Mahalle Mahalle { get; set; }
        public int TipId { get; set; }
        public Tip Tip { get; set; }
        public List<Resim> Resims { get; set; }




    }
}