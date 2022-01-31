using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emlak_Ofisi.Models
{
    public class Sehir
    {
        public int SehirId { get; set; }
        public string SehirAd { get; set; }
        public List<Semt>  Semts { get; set; } //şehrin bütün semtlerini getirir
    }
}