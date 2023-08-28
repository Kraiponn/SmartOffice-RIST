using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class HitCounter
    {
        public int Slid { get; set; }
        public string Ipaddress { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? TotalHit { get; set; }
    }
}
