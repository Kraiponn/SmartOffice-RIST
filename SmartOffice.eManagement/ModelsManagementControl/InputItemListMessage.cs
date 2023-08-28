using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class InputItemListMessage
    {
        public string ItemCateg { get; set; }
        public string ItemCode { get; set; }
        public int DisplayOrder { get; set; }
        public string Message { get; set; }
        public DateTime StartMessage { get; set; }
        public DateTime EndMessage { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
