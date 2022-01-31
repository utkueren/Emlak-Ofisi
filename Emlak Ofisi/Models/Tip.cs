using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emlak_Ofisi.Models
{
    public class Tip
    {
        public int TipId { get; set; }
        public string TipAd { get; set; } //arsa,daire, dükkan
        public int DurumId { get; set; }  //her bir tipin bir durumu vardır bir durumun birden fazla tipi olabilir
        public virtual Durum Durum { get; set; }
    }
}