using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class Operation
    {
        public Operation()
        {
            OperationItemCateg = new HashSet<OperationItemCateg>();
        }

        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public string OpeGroupCode { get; set; }
        public int? DisplayOrder { get; set; }
        public string InputKind { get; set; }
        public string RoleId { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public string SqlReport { get; set; }

        public virtual ICollection<OperationItemCateg> OperationItemCateg { get; set; }
    }
}
