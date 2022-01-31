using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emlak_Ofisi.Models
{
    public class Semt
    {
        public int SemtId { get; set; }
        public string SemtAd { get; set; }
        public int SehirId { get; set; } //HER BİR SEMTİN ŞEHRİ OLDUĞNU BELİRTİR
        public virtual Sehir Sehir { get; set; }

    }
}