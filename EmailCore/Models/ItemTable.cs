using System;
using System.Collections.Generic;

namespace SmartOffice.EmailCore.Models
{
    public partial class ItemTable
    {
        public string ItemKey { get; set; }
        public string ItemData { get; set; }
        public string ListData { get; set; }
        public string ListBiko { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
