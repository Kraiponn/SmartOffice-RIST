using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class ValueList
    {
        public string Id { get; set; }
        public string ValueCode { get; set; }
        public string ValueE { get; set; }
        public string ValueT { get; set; }
        public string ValueJ { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
