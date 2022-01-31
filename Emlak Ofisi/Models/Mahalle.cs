using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emlak_Ofisi.Models
{
    public class Mahalle
    {
        public int MahalleId { get; set; }
        public string MahalleAd { get; set; }
        public int SemtId { get; set; } //her mahallenin bir semti bir semtin birden çok mallesi vardır
        public virtual Semt Semt { get; set; }
        public List<Mahalle> Mahalles { get; set; }
    }
}