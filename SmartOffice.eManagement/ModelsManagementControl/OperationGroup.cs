using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class OperationGroup
    {
        public string OpeGroupCode { get; set; }
        public string OpeGroupName { get; set; }
        public string OpeGroupCateg { get; set; }
        public int? DisplayPriority { get; set; }
        public string SpecialOperate { get; set; }
        public string SpecialGroup { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
