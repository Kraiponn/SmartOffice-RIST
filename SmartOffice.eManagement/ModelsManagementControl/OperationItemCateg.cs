using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class OperationItemCateg
    {
        public string OperationCode { get; set; }
        public string ItemCateg { get; set; }
        public string InputOption { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }

        public virtual ItemCategory ItemCategNavigation { get; set; }
        public virtual Operation OperationCodeNavigation { get; set; }
    }
}
