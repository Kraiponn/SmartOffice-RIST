using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class OperationItem
    {
        public string OperationNo { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public int? OperationDisplayOrder { get; set; }
        public string InputKind { get; set; }
        public string RoleId { get; set; }
        public string OpeGroupCode { get; set; }
        public string OpeGroupName { get; set; }
        public string OpeGroupCateg { get; set; }
        public int? DisplayPriority { get; set; }
        public string SpecialOperate { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
