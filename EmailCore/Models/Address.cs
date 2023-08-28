using System;
using System.Collections.Generic;

namespace SmartOffice.EmailCore.Models
{
    public partial class Address
    {
        public string OperatorCode { get; set; }
        public int Idx { get; set; }
        public string AddressGroup { get; set; }
        public string Address1 { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
