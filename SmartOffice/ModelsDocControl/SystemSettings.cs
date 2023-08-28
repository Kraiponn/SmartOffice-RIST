using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class SystemSettings
    {
        public string GroupCode { get; set; }
        public string Code { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Description { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
