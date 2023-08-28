using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class RoleTransfer
    {
        public string FromOperatorId { get; set; }
        public string ToOperatorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RoleId { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
