using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class InputItemList
    {
        public string ItemCateg { get; set; }
        public string ItemCode { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }

        public virtual ItemCategory ItemCategNavigation { get; set; }
        public virtual InputItem ItemCodeNavigation { get; set; }
    }
}
