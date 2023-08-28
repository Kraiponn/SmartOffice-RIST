using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentGroup
    {
        public string DocumentGroupCode { get; set; }
        public string DocumentGroupName { get; set; }
        public string GroupCateg { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
