using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class RoleObject
    {
        public string RoleId { get; set; }
        public string DisabledObject { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
