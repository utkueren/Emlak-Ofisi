using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emlak_Ofisi.Models
{
    public class Resim
    {
        public int ResimId { get; set; }
        public string ResimAd { get; set; }
        public int İlanId { get; set; }
        public virtual İlan İlan { get; set; }

    }
}